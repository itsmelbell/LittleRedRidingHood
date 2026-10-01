using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple: MonoBehaviour
{
    public static float bottomY = -15f;

    void Update(){
        if(transform.position.y < bottomY) {
            Destroy(this.gameObject);
        }
    }
}