using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialouge dialougeData;
    private DialougeController dialougeUI;

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

    void StartDialouge(){
        isDialougeActive = true;
        dialougeIndex = 0;

        dialougeUI.SetNPCInfo(dialougeData.npcName);
        dialougeUI.ShowDialouge(true);

        PauseController.SetPause(true);

        DisplayCurrentLine();
    }

    void NextLine(){
        if(isTyping){
            // show full line
            StopAllCoroutines();
            dialougeUI.SetDialougeText(dialougeData.dialougeLines[dialougeIndex]);
            isTyping = false;
        } 

        dialougeUI.ClearChoices();

        //is end dialouge lines checked
        if(dialougeData.endDialougeLines.Length > dialougeIndex && dialougeData.endDialougeLines[dialougeIndex]){
            EndDialouge();
            return;
        }

        //check if there are choices set
        foreach(DialougeChoice dialougeChoice in dialougeData.choices){
            if(dialougeChoice.dialougeIndex == dialougeIndex){
                DisplayChoices(dialougeChoice);
                return;
            }
        }

        if(++dialougeIndex < dialougeData.dialougeLines.Length){
            DisplayCurrentLine();
        } else {
            EndDialouge();
        }
    }

    IEnumerator TypeLine(){
        isTyping = true;
        dialougeUI.SetDialougeText("");
        foreach(char letter in dialougeData.dialougeLines[dialougeIndex]){
            dialougeUI.SetDialougeText(dialougeUI.dialougeText.text += letter);
            yield return new WaitForSeconds(dialougeData.typingSpeed);
        }

        isTyping = false;

        if(dialougeData.autoProgressLines.Length > dialougeIndex && dialougeData.autoProgressLines[dialougeIndex]){
            yield return new WaitForSeconds(dialougeData.autoProgressDelay);
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
        StopAllCoroutines();
        isDialougeActive = false;
        dialougeUI.SetDialougeText("");
        dialougeUI.ShowDialouge(false);
        PauseController.SetPause(false);
    }
}
