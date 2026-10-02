using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public enum RequiredItem {None, Sugar, Apple, Egg, All}

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialouge dialougeData;
    public NPCDialouge itemDialouge;
    [SerializeField] private RequiredItem requiredItem;


    private DialougeController dialougeUI;

    [SerializeField] private UnityEvent dialougeFinish;
    private NPCDialouge activeData;


    private int dialougeIndex;
    private bool isTyping, isDialougeActive;

    private void Start(){
        dialougeUI = DialougeController.Instance;
    }

    public bool CanInteract(){
        return !isDialougeActive;
    }

    public void Interact(){
        if(dialougeData == null || (PauseController.IsGamePaused && !isDialougeActive)){
            return;
        } if(isDialougeActive){
            NextLine();
        } else {
            StartDialouge();
        }
    }

    private bool HasRequiredItem(){
        switch(requiredItem){
            case RequiredItem.Sugar: return GlobalHelper.hasSugar;
            case RequiredItem.Apple: return GlobalHelper.hasApple;
            case RequiredItem.Egg: return GlobalHelper.hasEgg;
            case RequiredItem.All: return GlobalHelper.hasAll();
            default: return false;
        }
    }

    private NPCDialouge GetCurrentDialouge(){
        if(HasRequiredItem() && itemDialouge != null){
            return itemDialouge;
        } 
        return dialougeData;
    }

    void StartDialouge(){
        activeData = GetCurrentDialouge();

        if(activeData == null){
            return;
        }

        isDialougeActive = true;
        dialougeIndex = 0;

        dialougeUI.SetNPCInfo(activeData.npcName);
        dialougeUI.ShowDialouge(true);

        PauseController.SetPause(true);

        DisplayCurrentLine();
    }

    void NextLine(){
        if(isTyping){
            // show full line
            StopAllCoroutines();
            dialougeUI.SetDialougeText(activeData.dialougeLines[dialougeIndex]);
            isTyping = false;
        } 

        dialougeUI.ClearChoices();

        //is end dialouge lines checked
        if(activeData.endDialougeLines.Length > dialougeIndex && activeData.endDialougeLines[dialougeIndex]){
            EndDialouge();
            return;
        }

        //check if there are choices set
        foreach(DialougeChoice dialougeChoice in activeData.choices){
            if(dialougeChoice.dialougeIndex == dialougeIndex){
                DisplayChoices(dialougeChoice);
                return;
            }
        }

        if(++dialougeIndex < activeData.dialougeLines.Length){
            DisplayCurrentLine();
        } else {
            EndDialouge();
        }
    }

    IEnumerator TypeLine(){
        isTyping = true;
        dialougeUI.SetDialougeText("");
        foreach(char letter in activeData.dialougeLines[dialougeIndex]){
            dialougeUI.SetDialougeText(dialougeUI.dialougeText.text += letter);
            yield return new WaitForSeconds(activeData.typingSpeed);
        }

        isTyping = false;

        if(activeData.autoProgressLines.Length > dialougeIndex && activeData.autoProgressLines[dialougeIndex]){
            yield return new WaitForSeconds(activeData.autoProgressDelay);
            NextLine();
        }
    }

    void DisplayChoices(DialougeChoice choice){
        for(int i = 0; i < choice.choices.Length; i++){
            int nextIndex = choice.nextDialougeIndexes[i];
            dialougeUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }
    }

    void ChooseOption(int nextIndex){
        dialougeIndex = nextIndex;
        dialougeUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine(){
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    public void EndDialouge(){
        bool wasitemDialouge = itemDialouge != null && activeData == itemDialouge;
        StopAllCoroutines();
        isDialougeActive = false;
        dialougeUI.SetDialougeText("");
        dialougeUI.ShowDialouge(false);
        PauseController.SetPause(false);

        if(wasitemDialouge){
            dialougeFinish?.Invoke();
        }
    }
}
