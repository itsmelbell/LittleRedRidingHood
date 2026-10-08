using UnityEngine;
using UnityEngine.InputSystem;

//menu by clicking tab
public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    //when tab is pressed, open menu if not already opened
    //close menu otherwise
    public void onOpenMenu(InputAction.CallbackContext context){
        if (context.performed){
            //print("menu up");
            if(!menuCanvas.activeSelf && PauseController.IsGamePaused){
                //print("returned");
                return;
            }
            menuCanvas.SetActive(!menuCanvas.activeSelf);
            PauseController.SetPause(menuCanvas.activeSelf);
        }
    }

    //called by button click, when restarting a minigame
    public void closeMenu(){
        menuCanvas.SetActive(false);
        PauseController.SetPause(false);
    }
}
