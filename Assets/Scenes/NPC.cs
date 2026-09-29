using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour, IInteractable
{
    public NPCDialouge dialougeData;
    public GameObject dialougePanel;
    public TMP_Text dialougeText, nameText;
    public Image portraitImage;

    private int dialougeIndex;
    private bool isTyping, isDialougeActive;

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

        nameText.SetText(dialougeData.npcName);
        portraitImage.sprite = dialougeData.npcPortrait;

        dialougePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());

    }

    void NextLine(){
        if(isTyping){
            StopAllCoroutines();
            dialougeText.SetText(dialougeData.dialougeLines[dialougeIndex]);
            isTyping = false;
        } else if(++dialougeIndex < dialougeData.dialougeLines.Length){
            StartCoroutine(TypeLine());
        } else {
            EndDialouge();
        }
    }

    IEnumerator TypeLine(){
        isTyping = true;
        dialougeText.SetText("");
        foreach(char letter in dialougeData.dialougeLines[dialougeIndex]){
            dialougeText.text += letter;
            yield return new WaitForSeconds(dialougeData.typingSpeed);
        }

        isTyping = false;

        if(dialougeData.autoProgressLines.Length > dialougeIndex && dialougeData.autoProgressLines[dialougeIndex]){
            yield return new WaitForSeconds(dialougeData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialouge(){
        StopAllCoroutines();
        isDialougeActive = false;
        dialougeText.SetText("");
        dialougePanel.SetActive(false);
        PauseController.SetPause(false);
    }
}
