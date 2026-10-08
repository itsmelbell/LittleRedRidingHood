using UnityEngine;
using UnityEngine.SceneManagement;

//keep track of what ingredients that player has
//bypass inventory system
public static class GlobalHelper
{
    public static bool hasSugar {get; private set; } = false;   
    public static void getSugar(bool accquired){
        hasSugar = accquired;
    }

    public static bool hasEgg {get; private set; } = false;   
    public static void getEgg(bool accquired){
        hasEgg = accquired;
    }

    public static bool hasApple {get; private set; } = false;   
    public static void getApple(bool accquired){
        hasApple = accquired;
    }

    public static bool hasAll(){
        if(hasSugar && hasEgg && hasApple){
            return true;
        } else {
            return false;
        }
    }
}
