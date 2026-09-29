using UnityEngine;

public static class GlobalHelper
{
    public static bool hasFlour {get; private set; } = false;   
    public static void getFlour(bool accquired){
        hasFlour = accquired;
    }

    public static bool hasEgg {get; private set; } = false;   
    public static void getEgg(bool accquired){
        hasEgg = accquired;
    }

    public static bool hasApple {get; private set; } = false;   
    public static void getApple(bool accquired){
        hasApple = accquired;
    }

    public static string GenerateUniqueID(GameObject obj){
        return $"{obj.scene.name}_{obj.transform.position.x}_{obj.transform.position.y}";
    }
}
