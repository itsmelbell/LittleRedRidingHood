using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//copied from apple picker, but this is how player moves
public class Basket : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private float leftEdge = -8f;
    [SerializeField] private float rightEdge = 8f;
    [SerializeField] private MinigameTimer timer;
    private bool hasWon;
    public GameObject infoPanel;
    public GameObject loseScreen;

    private Rigidbody2D rb;
    private float horizontalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        if (scoreCounter == null)
                {
                    GameObject scoreObject = GameObject.Find("ScoreCounter");
                    if (scoreObject != null)
                        scoreCounter = scoreObject.GetComponent<ScoreCounter>();
                }    
    }

    // Update is called once per frame
    void Update()
    {
        if(PauseController.IsGamePaused){
            horizontalInput = 0f;  
            return; 
        } 
              
        horizontalInput = 0f; 
 
        if(Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed){
            horizontalInput = -1f;
        } if(Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed){
            horizontalInput = 1f;
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = rb.position;
        movement.x += horizontalInput * moveSpeed * Time.fixedDeltaTime;
        movement.x = Mathf.Clamp(movement.x, leftEdge, rightEdge);

        rb.MovePosition(movement);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Apple")) return;
        
        Destroy(collision.gameObject);
        SoundEffectManager.Play("Apple");

        if(scoreCounter == null | hasWon) return;
        
        scoreCounter.score += 1;
        

        if(scoreCounter.score >= 10){
            hasWon = true;
            timer.Stop();
            GlobalHelper.getApple(true);
            SoundEffectManager.Play("Ingredient");
            infoPanel.SetActive(true);
        }
        
    }

    public void endMinigame(){
        infoPanel.SetActive(false);
        FindAnyObjectByType<SceneTransition>().ReturnToOverWorld();
    }

    public void lost(){
        PauseController.SetPause(true);
        loseScreen.SetActive(true);
        SoundEffectManager.Play("Fail");
    }

    public void reset(){
        PauseController.SetPause(false);
        loseScreen.SetActive(false);
        scoreCounter.score = 0;
        hasWon = false;
        timer.ResetTimer();
        rb.transform.position = new Vector3(0.6f, -12.7f, 0f);
    }
}
