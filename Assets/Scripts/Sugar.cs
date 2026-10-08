using UnityEngine;
using UnityEngine.SceneManagement;

//sugar collected in bee level
public class Sugar : MonoBehaviour, IInteractable
{
    public GameObject infoPanel;
    [SerializeField] private MinigameTimer timer;
    private bool collected;

    public void Interact(){
        if(!CanInteract()) return;
        collectItem();
    }

    public bool CanInteract(){
        return true;
    }

    public void collectItem(){
        collected = true;
        GlobalHelper.getSugar(true);
        SoundEffectManager.Play("Ingredient");
        if(timer != null){
            timer.Stop();
        }
        infoPanel.SetActive(true);     
    }

    public void endMinigame(){
        infoPanel.SetActive(false);
        FindAnyObjectByType<SceneTransition>().ReturnToOverWorld();
    }
}
