using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//Text to display player lives
public class LiveCounter : MonoBehaviour {
    [SerializeField] private TMP_Text livesText;                                                        // c

    private void Reset()
    {
        // auto-fills the slot when you add the component
        livesText = GetComponent<TMP_Text>();
    }

    public void SetLives(int lives)
    {
        livesText.text = " " + Mathf.Max(lives, 0);
    }
}