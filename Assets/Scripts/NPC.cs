using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

//NPC dialouge
public enum RequiredItem {None, Sugar, Apple, Egg, All}

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialouge dialougeData;

    //dialouge can change depending what ingredients player possess
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

    //check for what ingredent player has, returns the appropriate dialouge
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
            return;
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
        int i = 0;
        //letter by letter typing
        foreach(char letter in activeData.dialougeLines[dialougeIndex]){
            dialougeUI.SetDialougeText(dialougeUI.dialougeText.text += letter);
            //so the sound effects dont sound like a machine gun
            if(letter != ' ' && i%2 == 0){
                SoundEffectManager.PlayVoice(activeData.voiceSound, activeData.voicePitch);
            }
            i++;
            yield return new WaitForSeconds(activeData.typingSpeed);
        }

        isTyping = false;

        if(activeData.autoProgressLines.Length > dialougeIndex && activeData.autoProgressLines[dialougeIndex]){
            yield return new WaitForSeconds(activeData.autoProgressDelay);
            NextLine();
        }
    }

    //choice buttons
    void DisplayChoices(DialougeChoice choice){
        for(int i = 0; i < choice.choices.Length; i++){
            int nextIndex = choice.nextDialougeIndexes[i];
            dialougeUI.CreateChoiceButton(choice.choices[i], () => ChooseOption(nextIndex));
        }
    }

    //option is picked, set the dialouge index to the appropriate response
    void ChooseOption(int nextIndex){
        dialougeIndex = nextIndex;
        dialougeUI.ClearChoices();
        DisplayCurrentLine();
    }

    void DisplayCurrentLine(){
        StopAllCoroutines();
        StartCoroutine(TypeLine());
    }

    //stops dialouge, closes all windows
    public void EndDialouge(){
        bool wasitemDialouge = itemDialouge != null && activeData == itemDialouge;
        StopAllCoroutines();
        isDialougeActive = false;
        dialougeUI.SetDialougeText("");
        dialougeUI.ShowDialouge(false);
        PauseController.SetPause(false);

        //for game end instance
        if(wasitemDialouge){
            dialougeFinish?.Invoke();
        }
    }
}
