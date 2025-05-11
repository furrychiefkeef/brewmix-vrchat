using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

// Enhanced serialization manager for IL2CPP objects
public static class EnhancedSerializationManager
{
    // Main method to convert IL2CPP objects to managed objects
    public static object IL2CPPToManaged(object il2cppObject)
    {
        if (il2cppObject == null)
            return null;

        // Get the type of the IL2CPP object
        Type il2cppType = il2cppObject.GetType();
        string typeName = il2cppType.FullName;

        // Early return for primitive types and strings
        if (il2cppType.IsPrimitive || il2cppObject is string)
            return il2cppObject;

        try
        {
            // Handle common IL2CPP collection types
            if (IsIL2CPPDictionary(il2cppObject))
            {
                return ConvertIL2CPPDictionary(il2cppObject);
            }
            else if (IsIL2CPPList(il2cppObject))
            {
                return ConvertIL2CPPList(il2cppObject);
            }
            else if (IsIL2CPPArray(il2cppObject))
            {
                return ConvertIL2CPPArray(il2cppObject);
            }
            // Handle Photon specific types - Parameter Dictionary
            else if (typeName.Contains("ParameterDictionary"))
            {
                return ConvertParameterDictionary(il2cppObject);
            }
            // Handle Photon event data
            else if (typeName.Contains("EventData"))
            {
                return ConvertEventData(il2cppObject);
            }
            // Handle general IL2CPP objects
            else
            {
                return ConvertGenericIL2CPPObject(il2cppObject);
            }
        }
        catch (Exception ex)
        {
            return $"[Conversion Error: {ex.Message}]";
        }
    }

    // Check if object is an IL2CPP dictionary
    private static bool IsIL2CPPDictionary(object obj)
    {
        Type type = obj.GetType();
        string typeName = type.FullName ?? string.Empty;

        return typeName.Contains("Dictionary") ||
               typeName.Contains("IDictionary") ||
               typeName.Contains("Map") ||
               // Try to access common dictionary properties/methods
               HasMethod(type, "get_Item") && HasMethod(type, "ContainsKey");
    }

    // Check if object is an IL2CPP list
    private static bool IsIL2CPPList(object obj)
    {
        Type type = obj.GetType();
        string typeName = type.FullName ?? string.Empty;

        return typeName.Contains("List") ||
               typeName.Contains("IList") ||
               typeName.Contains("Collection") ||
               // Try to access common list properties/methods
               HasMethod(type, "get_Item") && HasMethod(type, "get_Count") &&
               !HasMethod(type, "ContainsKey");
    }

    // Check if object is an IL2CPP array
    private static bool IsIL2CPPArray(object obj)
    {
        Type type = obj.GetType();
        return type.IsArray || type.FullName?.Contains("[]") == true;
    }

    // Helper to check if a type has a method
    private static bool HasMethod(Type type, string methodName)
    {
        return type.GetMethod(methodName) != null;
    }

    // Convert IL2CPP dictionary to managed dictionary
    private static object ConvertIL2CPPDictionary(object il2cppDict)
    {
        Dictionary<object, object> result = new Dictionary<object, object>();

        // Try different ways to get keys and values
        try
        {
            // Method 1: Try to use IDictionary interface
            if (il2cppDict is IDictionary dict)
            {
                foreach (DictionaryEntry entry in dict)
                {
                    object key = IL2CPPToManaged(entry.Key);
                    object value = IL2CPPToManaged(entry.Value);
                    result[key] = value;
                }

                return result;
            }

            // Method 2: Try to use reflection to access keys and values
            Type dictType = il2cppDict.GetType();

            // Try to get Keys property
            PropertyInfo keysProperty = dictType.GetProperty("Keys");
            if (keysProperty != null)
            {
                object keysCollection = keysProperty.GetValue(il2cppDict);
                if (keysCollection is IEnumerable keys)
                {
                    foreach (object key in keys)
                    {
                        // Try to use indexer to get value
                        MethodInfo indexerMethod = dictType.GetMethod("get_Item");
                        if (indexerMethod != null)
                        {
                            object value = indexerMethod.Invoke(il2cppDict, new[] { key });
                            result[IL2CPPToManaged(key)] = IL2CPPToManaged(value);
                        }
                    }

                    return result;
                }
            }

            // Method 3: Try to use non-generic enumerator
            MethodInfo getEnumeratorMethod = dictType.GetMethod("GetEnumerator");
            if (getEnumeratorMethod != null)
            {
                object enumerator = getEnumeratorMethod.Invoke(il2cppDict, null);
                Type enumeratorType = enumerator.GetType();
                MethodInfo moveNextMethod = enumeratorType.GetMethod("MoveNext");
                PropertyInfo currentProperty = enumeratorType.GetProperty("Current");

                if (moveNextMethod != null && currentProperty != null)
                {
                    while ((bool)moveNextMethod.Invoke(enumerator, null))
                    {
                        object current = currentProperty.GetValue(enumerator);

                        // Try to extract key and value from the current entry
                        Type entryType = current.GetType();
                        PropertyInfo keyProperty = entryType.GetProperty("Key");
                        PropertyInfo valueProperty = entryType.GetProperty("Value");

                        if (keyProperty != null && valueProperty != null)
                        {
                            object key = keyProperty.GetValue(current);
                            object value = valueProperty.GetValue(current);
                            result[IL2CPPToManaged(key)] = IL2CPPToManaged(value);
                        }
                    }

                    return result;
                }
            }

            // If we get here, none of the methods worked
            throw new InvalidOperationException("Could not extract dictionary entries");
        }
        catch (Exception ex)
        {
            result["[ERROR]"] = $"Dictionary conversion error: {ex.Message}";
            return result;
        }
    }

    // Convert IL2CPP list to managed list
    private static object ConvertIL2CPPList(object il2cppList)
    {
        List<object> result = new List<object>();

        try
        {
            // Method 1: Try to use ICollection or IEnumerable interface
            if (il2cppList is IEnumerable enumerable)
            {
                foreach (object item in enumerable)
                {
                    result.Add(IL2CPPToManaged(item));
                }

                return result;
            }

            // Method 2: Try to use Count property and indexer
            Type listType = il2cppList.GetType();
            PropertyInfo countProperty = listType.GetProperty("Count");
            MethodInfo indexerMethod = listType.GetMethod("get_Item");

            if (countProperty != null && indexerMethod != null)
            {
                int count = (int)countProperty.GetValue(il2cppList);
                for (int i = 0; i < count; i++)
                {
                    object item = indexerMethod.Invoke(il2cppList, new object[] { i });
                    result.Add(IL2CPPToManaged(item));
                }

                return result;
            }

            // Method 3: Try to use GetEnumerator
            MethodInfo getEnumeratorMethod = listType.GetMethod("GetEnumerator");
            if (getEnumeratorMethod != null)
            {
                object enumerator = getEnumeratorMethod.Invoke(il2cppList, null);
                Type enumeratorType = enumerator.GetType();
                MethodInfo moveNextMethod = enumeratorType.GetMethod("MoveNext");
                PropertyInfo currentProperty = enumeratorType.GetProperty("Current");

                if (moveNextMethod != null && currentProperty != null)
                {
                    while ((bool)moveNextMethod.Invoke(enumerator, null))
                    {
                        object current = currentProperty.GetValue(enumerator);
                        result.Add(IL2CPPToManaged(current));
                    }

                    return result;
                }
            }

            throw new InvalidOperationException("Could not extract list items");
        }
        catch (Exception ex)
        {
            result.Add($"[List conversion error: {ex.Message}]");
            return result;
        }
    }

    // Convert IL2CPP array to managed array
    private static object ConvertIL2CPPArray(object il2cppArray)
    {
        try
        {
            if (il2cppArray is Array array)
            {
                List<object> result = new List<object>();
                foreach (object item in array)
                {
                    result.Add(IL2CPPToManaged(item));
                }

                return result.ToArray();
            }

            throw new InvalidOperationException("Not a valid array");
        }
        catch (Exception ex)
        {
            return $"[Array conversion error: {ex.Message}]";
        }
    }

    // Special handler for Photon ParameterDictionary
    private static object ConvertParameterDictionary(object paramDictObj)
    {
        Dictionary<object, object> result = new Dictionary<object, object>();

        try
        {
            Type paramDictType = paramDictObj.GetType();

            // Try to access the inner dictionary field/property called "paramDict"
            object innerDict = null;

            // Try property first
            PropertyInfo paramDictProperty = paramDictType.GetProperty("paramDict");
            if (paramDictProperty != null)
            {
                innerDict = paramDictProperty.GetValue(paramDictObj);
            }
            else
            {
                // Try field if property doesn't exist
                FieldInfo paramDictField = paramDictType.GetField("paramDict",
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (paramDictField != null)
                {
                    innerDict = paramDictField.GetValue(paramDictObj);
                }
            }

            // If we found the inner dictionary, convert it
            if (innerDict != null)
            {
                return ConvertIL2CPPDictionary(innerDict);
            }

            // If we couldn't find the inner dictionary, try to use the parameter dictionary directly
            if (paramDictObj is IDictionary || HasMethod(paramDictType, "ContainsKey"))
            {
                return ConvertIL2CPPDictionary(paramDictObj);
            }

            // Try to use the indexer method
            MethodInfo tryGetValueMethod = paramDictType.GetMethod("TryGetValue");
            PropertyInfo keysProperty = paramDictType.GetProperty("Keys");

            if (tryGetValueMethod != null && keysProperty != null)
            {
                object keysCollection = keysProperty.GetValue(paramDictObj);
                if (keysCollection is IEnumerable keys)
                {
                    foreach (object key in keys)
                    {
                        object[] parameters = { key, null };
                        bool success = (bool)tryGetValueMethod.Invoke(paramDictObj, parameters);
                        if (success)
                        {
                            result[IL2CPPToManaged(key)] = IL2CPPToManaged(parameters[1]);
                        }
                    }

                    return result;
                }
            }

            result["[WARNING]"] = "Could not extract ParameterDictionary values through known methods";
            return result;
        }
        catch (Exception ex)
        {
            result["[ERROR]"] = $"ParameterDictionary conversion error: {ex.Message}";
            return result;
        }
    }

    // Special handler for Photon EventData
    private static object ConvertEventData(object eventDataObj)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        try
        {
            Type eventDataType = eventDataObj.GetType();

            // Extract common EventData properties
            PropertyInfo codeProperty = eventDataType.GetProperty("Code");
            PropertyInfo senderProperty = eventDataType.GetProperty("Sender");
            PropertyInfo customDataProperty = eventDataType.GetProperty("CustomData");
            PropertyInfo parametersProperty = eventDataType.GetProperty("Parameters");

            if (codeProperty != null)
                result["Code"] = codeProperty.GetValue(eventDataObj);

            if (senderProperty != null)
                result["Sender"] = senderProperty.GetValue(eventDataObj);

            if (customDataProperty != null)
            {
                object customData = customDataProperty.GetValue(eventDataObj);
                result["CustomData"] = IL2CPPToManaged(customData);
            }

            if (parametersProperty != null)
            {
                object parameters = parametersProperty.GetValue(eventDataObj);
                result["Parameters"] = IL2CPPToManaged(parameters);
            }

            return result;
        }
        catch (Exception ex)
        {
            result["[ERROR]"] = $"EventData conversion error: {ex.Message}";
            return result;
        }
    }

    // Convert any generic IL2CPP object to a managed representation
    private static object ConvertGenericIL2CPPObject(object il2cppObject)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        try
        {
            Type type = il2cppObject.GetType();
            result["[Type]"] = type.FullName;

            // Get all public properties
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in properties)
            {
                try
                {
                    if (prop.GetIndexParameters().Length == 0) // Skip indexers
                    {
                        object value = prop.GetValue(il2cppObject);
                        result[prop.Name] = IL2CPPToManaged(value);
                    }
                }
                catch
                {
                    result[prop.Name] = "[Property access error]";
                }
            }

            // Get all public fields
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                try
                {
                    object value = field.GetValue(il2cppObject);
                    result[field.Name] = IL2CPPToManaged(value);
                }
                catch
                {
                    result[field.Name] = "[Field access error]";
                }
            }

            return result;
        }
        catch (Exception ex)
        {
            result["[ERROR]"] = $"Object conversion error: {ex.Message}";
            return result;
        }
    }

    public static string SerializeObject(object obj, int depth = 0, HashSet<object> visited = null)
    {
        if (visited == null)
            visited = new HashSet<object>(new ReferenceEqualityComparer());

        if (obj == null)
            return "null";

        // Prevent infinite recursion
        if (depth > 5)
            return "[Max depth reached]";

        // Check for circular references
        if (!obj.GetType().IsValueType && !obj.GetType().IsPrimitive && !(obj is string))
        {
            if (visited.Contains(obj))
                return "[Circular reference]";

            visited.Add(obj);
        }

        // Handle simple types
        if (obj is string s)
            return $"\"{s}\"";
        if (obj is bool b)
            return b.ToString().ToLower();
        if (obj is byte || obj is short || obj is int || obj is long ||
            obj is float || obj is double || obj is decimal)
            return obj.ToString();

        // Handle collections
        if (obj is IDictionary dict)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{ ");

            bool first = true;
            foreach (DictionaryEntry entry in dict)
            {
                if (!first) sb.Append(", ");
                first = false;

                string keyStr = SerializeObject(entry.Key, depth + 1, visited);
                string valueStr = SerializeObject(entry.Value, depth + 1, visited);

                sb.Append($"{keyStr}: {valueStr}");
            }

            sb.Append(" }");
            return sb.ToString();
        }

        if (obj is IEnumerable enumerable && !(obj is string))
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");

            bool first = true;
            foreach (object item in enumerable)
            {
                if (!first) sb.Append(", ");
                first = false;

                sb.Append(SerializeObject(item, depth + 1, visited));
            }

            sb.Append(" ]");
            return sb.ToString();
        }

        // Handle other objects
        return obj.ToString();
    }

// Helper class for reference equality comparison
    public class ReferenceEqualityComparer : EqualityComparer<object>
    {
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }

        public override int GetHashCode(object obj)
        {
            return RuntimeHelpers.GetHashCode(obj);
        }
    }
}



// Helper method to serialize any object to string
