using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes;

namespace Brewview;


/// <summary>
/// Faster, and Enjoyable to Write (Thanks AI for helping me a lil bit)
/// </summary>
public static class SerializationManager
{
    public static T FromManagedToIL2CPP<T>(object obj)
    {
        if (obj == null)
            return default;

        // If already the correct type, return directly
        if (obj is T il2cppObj)
            return il2cppObj;

        // String conversion
        if (obj is string stringValue)
        {
            // Use the proper string conversion method
            IntPtr il2cppString = IL2CPP.ManagedStringToIl2Cpp(stringValue);
            return (T)(object)new Il2CppSystem.String(il2cppString);
        }

        // Primitive type conversions using proper boxing methods
        if (obj is bool boolValue)
        {
            // Use the Il2CppSystem.Convert methods which handle the conversions correctly
            return (T)(object)Il2CppSystem.Convert.ToBoolean(boolValue);
        }

        if (obj is int intValue)
        {
            return (T)(object)Il2CppSystem.Convert.ToInt32(intValue);
        }

        if (obj is float floatValue)
        {
            return (T)(object)Il2CppSystem.Convert.ToSingle(floatValue);
        }

        if (obj is double doubleValue)
        {
            return (T)(object)Il2CppSystem.Convert.ToDouble(doubleValue);
        }

        if (obj is long longValue)
        {
            return (T)(object)Il2CppSystem.Convert.ToInt64(longValue);
        }

        // Handle System.Array (not Il2CppSystem.Array)
        if (obj is System.Array managedArray)
        {
            var il2cppList = new Il2CppSystem.Collections.Generic.List<Il2CppSystem.Object>();
            foreach (var item in managedArray)
            {
                var convertedItem = FromManagedToIL2CPP<Il2CppSystem.Object>(item);
                if (convertedItem != null)
                    il2cppList.Add(convertedItem);
            }
            return (T)(object)il2cppList;
        }

        // Handle managed dictionaries
        if (obj is System.Collections.IDictionary dict)
        {
            var il2cppDict = new Il2CppSystem.Collections.Generic.Dictionary<Il2CppSystem.Object, Il2CppSystem.Object>();
            foreach (System.Collections.DictionaryEntry entry in dict)
            {
                var key = FromManagedToIL2CPP<Il2CppSystem.Object>(entry.Key);
                var value = FromManagedToIL2CPP<Il2CppSystem.Object>(entry.Value);
                
                if (key != null && value != null)
                    il2cppDict.Add(key, value);
            }
            return (T)(object)il2cppDict;
        }

        // Handle managed lists
        if (obj is System.Collections.IList list)
        {
            var il2cppList = new Il2CppSystem.Collections.Generic.List<Il2CppSystem.Object>();
            foreach (var item in list)
            {
                var il2cppItem = FromManagedToIL2CPP<Il2CppSystem.Object>(item);
                if (il2cppItem != null)
                    il2cppList.Add(il2cppItem);
            }
            return (T)(object)il2cppList;
        }

        // Handle HashSet - need to convert directly to a List for IL2CPP
        if (obj is System.Collections.IEnumerable enumerable && !(obj is string))
        {
            var il2cppList = new Il2CppSystem.Collections.Generic.List<Il2CppSystem.Object>();
            foreach (var item in enumerable)
            {
                var il2cppItem = FromManagedToIL2CPP<Il2CppSystem.Object>(item);
                if (il2cppItem != null)
                    il2cppList.Add(il2cppItem);
            }
            return (T)(object)il2cppList;
        }

        // If we can't convert, return default value
        return default;
    }
    
    // Optional: IL2CPP to managed converter
    public static T FromIL2CPPToManaged<T>(object? obj)
    {
        if (obj == null)
            return default;
        
        // Handle primitive types and strings
        if (obj is Il2CppSystem.String il2cppString)
        {
            string managedString = il2cppString.ToString();
            return (T)(object)managedString;
        }
        
        /*if (obj is Il2CppSystem.Boolean boolVal)
        {
            return (T)(object)(bool)boolVal.m_value;
        }
        
        if (obj is Il2CppSystem.Int32 intVal)
        {
            return (T)(object)(int)intVal.m_value;
        }
        
        if (obj is Il2CppSystem.Single floatVal)
        {
            return (T)(object)(float)floatVal.m_value;
        }
        
        if (obj is Il2CppSystem.Double doubleVal)
        {
            return (T)(object)(double)doubleVal.m_value;
        }
        
        if (obj is Il2CppSystem.Int64 longVal)
        {
            return (T)(object)(long)longVal.m_value;
        }*/
        
        // Add more conversions as needed
        
        return default;
    }
}