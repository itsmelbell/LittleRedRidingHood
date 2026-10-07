using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    public GameObject loseScreen;
    [SerializeField] private MinigameTimer timer; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //print(GlobalHelper.hasApple);
        //print(GlobalHelper.hasSugar);
        //print(GlobalHelper.hasEgg);
    }

    // Update is called once per frame
    void Update()
    {
        //dont move if paused
        if(PauseController.IsGamePaused){
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isWalking", false);
            return;
        }
        rb.linearVelocity = moveInput * moveSpeed;
        animator.SetBool("isWalking", rb.linearVelocity.magnitude > 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(context.canceled){
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        moveInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }

    //for bee level, when timer runs out       
    public void lost(){
        PauseController.SetPause(true);
        loseScreen.SetActive(true);
    }

    //for bee level, called by button click
    public void reset(){
        PauseController.SetPause(false);
        loseScreen.SetActive(false);
        timer.ResetTimer();
        rb.transform.position = new Vector3(0f, 0f, 0f);
    }
}
