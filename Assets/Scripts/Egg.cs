using UnityEngine;
using UnityEngine.SceneManagement;

public class Egg : MonoBehaviour, IInteractable
{
    public void Interact(){
        if(!CanInteract()) return;
        collectItem();
    }

    public bool CanInteract(){
        return true;
    }

    public void collectItem(){
        GlobalHelper.getEgg(true);
        FindAnyObjectByType<SceneTransition>().ReturnToOverWorld();   
    }
}
