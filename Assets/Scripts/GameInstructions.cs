using UnityEngine;

public class GameInstructions : MonoBehaviour
{
    public GameObject infoPanel;
    [SerializeField] private MinigameTimer timer;
    private bool isUp;

    //start of each minigame, should should
    void Start(){
        isUp = true;
        PauseController.SetPause(true);
    }
    
    //with button click
    public void infoScreenDown()
    {
        infoPanel.SetActive(false);
        PauseController.SetPause(false);
        isUp = false;
        if(timer != null)
            timer.StartTimer();
    }

    //bool to check if screen is up
    public bool screenUp(){
        return isUp;
    }
}
