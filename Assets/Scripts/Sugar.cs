using UnityEngine;
using UnityEngine.SceneManagement;

public class Sugar : MonoBehaviour, IInteractable
{
    public void Interact(){
        if(!CanInteract()) return;
        collectItem();
    }

    public bool CanInteract(){
        return true;
    }

    public void collectItem(){
        GlobalHelper.getSugar(true);
        FindAnyObjectByType<SceneTransition>().ReturnToOverWorld();   
    }
}
