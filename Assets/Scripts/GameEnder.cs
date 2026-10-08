using UnityEngine;

//Displays the end screen
public class GameEnder : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;

    //method called by invoke
    public void EndGame()
    {
        endScreen.SetActive(true);
        PauseController.SetPause(true);
    }

}