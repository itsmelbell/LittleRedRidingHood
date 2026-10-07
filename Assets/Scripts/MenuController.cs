using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    public void onOpenMenu(InputAction.CallbackContext context){
        if (context.performed){
            print("menu up");
            if(!menuCanvas.activeSelf && PauseController.IsGamePaused){
                print("returned");
                return;
            }
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            PauseController.SetPause(menuCanvas.activeSelf);
        }
    }

    public void closeMenu(){
        menuCanvas.SetActive(false);
        PauseController.SetPause(false);
    }
}
