using UnityEngine;
using UnityEngine.SceneManagement;

//egg collected in platfomer level
public class Egg : MonoBehaviour, IInteractable
{
    public GameObject infoPanel;

    public void Interact(){
        if(!CanInteract()) return;
        collectItem();
    }

    public bool CanInteract(){
        return true;
    }

    public void collectItem(){
        GlobalHelper.getEgg(true);
        SoundEffectManager.Play("Ingredient");
        infoPanel.SetActive(true);     
    }

    public void endMinigame(){
        infoPanel.SetActive(false);
        FindAnyObjectByType<SceneTransition>().ReturnToOverWorld();
    }
}
