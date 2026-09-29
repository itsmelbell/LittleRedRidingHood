using UnityEngine;

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
        print("sugar accquired");
    }
}
