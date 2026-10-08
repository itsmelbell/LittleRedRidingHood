using UnityEngine;

//pause and bring up menu 
public class PauseController : MonoBehaviour{
    public static bool IsGamePaused {get; private set; } = false;
    public static void SetPause(bool pause){
        IsGamePaused = pause;
    }
}
