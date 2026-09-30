using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialougeController : MonoBehaviour
{
    public static DialougeController Instance {get; private set;}
    public GameObject dialougePanel;
    public TMP_Text dialougeText, nameText;
    public Transform choiceContainer;
    public GameObject choiceButtonPrefab;
    //public Button endButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowDialouge(bool show){
        dialougePanel.SetActive(show);
    }

    public void SetNPCInfo(string npcName){
        nameText.text = npcName;
    }

    public void SetDialougeText(string text){
        dialougeText.text = text;
    }

    public void ClearChoices(){
        foreach(Transform child in choiceContainer) Destroy(child.gameObject);
    }

    public void CreateChoiceButton(string choiceText, UnityEngine.Events.UnityAction onClick){
        GameObject choiceButton = Instantiate(choiceButtonPrefab, choiceContainer);
        choiceButton.GetComponentInChildren<TMP_Text>().text = choiceText;
        choiceButton.GetComponent<Button>().onClick.AddListener(onClick);
    }
}
