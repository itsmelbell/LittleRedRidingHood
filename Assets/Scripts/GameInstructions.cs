using UnityEngine;

public class GameInstructions : MonoBehaviour
{
    public GameObject infoPanel;
    private bool isUp;

    void Start(){
        isUp = true;
        PauseController.SetPause(true);
    }
    
    public void infoScreenDown()
    {
        infoPanel.SetActive(false);
        PauseController.SetPause(false);
        isUp = false;
    }

    public bool screenUp(){
        return isUp;
    }
}
