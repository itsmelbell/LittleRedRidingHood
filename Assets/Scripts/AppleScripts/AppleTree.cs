using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    [Header("Inscribed")]

    // Prefab for instantiating apples
    public GameObject applePrefab;

    // Speed at which the AppleTree moves
    public float speed = 5f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 6.5f;

    // Chance that the AppleTree will change direction
    public float changeDirChance = 0.002f;

    // Seconds between Apples instantiations
    public float appleDropDelay = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //start dropping apples
        Invoke("DropApple", 2f);
    }

    void DropApple(){
        if (!PauseController.IsGamePaused){
            GameObject apple = Instantiate<GameObject>(applePrefab);
            apple.transform.position = transform.position;
        }
        Invoke("DropApple", appleDropDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseController.IsGamePaused) return;
        //basic movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        //changing direction
        if(pos.x < -leftAndRightEdge){
            speed = Mathf.Abs(speed); //move right
        } else if (pos.x > leftAndRightEdge){
            speed = -Mathf.Abs(speed); //move left
        //} else if (Random.value < changeDirChance){
        //    speed *= -1; // change direction
        }
    }

    void FixedUpdate(){
        //Ranodm directio nchanges are now time-based
        if( Random.value < changeDirChance){
            speed *= -1; //change direction
        }
    }
}
