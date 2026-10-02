using UnityEngine;

public class GameEnder : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;

    public void EndGame()
    {
        endScreen.SetActive(true);
        PauseController.SetPause(true);
    }

    public void StartGame(){
        endScreen.SetActive(false);
        PauseController.SetPause(false);
    }
}