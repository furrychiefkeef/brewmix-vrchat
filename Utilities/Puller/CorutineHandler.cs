using UNIXEngine.IL2CPPModding.Utils.Collections;

namespace Brewview;

public static class CorutineHandler
{
    public static System.Collections.IEnumerator Start(System.Collections.IEnumerator routine)
    {
        VRCUiManager.field_Private_Static_VRCUiManager_0.StartCoroutine(routine.WrapToIl2Cpp());
        return routine;
    }
    /*public static System.Collections.IEnumerator Stop(System.Collections.IEnumerator routine)
    {
      
    }*/

  
}