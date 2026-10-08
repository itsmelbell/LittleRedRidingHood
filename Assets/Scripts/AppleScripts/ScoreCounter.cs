using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // This line enables use of uGUI classes like Text.        // a
using TMPro;

//from apple picker
public class ScoreCounter : MonoBehaviour {
    [Header("Dynamic")]                                                          // b
    public int score = 0;

    private TMP_Text uiText;                                                        // c

    void Start() {
       uiText = GetComponent<TMP_Text>();                                           // d
    }

    void Update() {
       uiText.text = "x " + score.ToString( "#,0" ); // This 0 is a zero!              // e
    }
}