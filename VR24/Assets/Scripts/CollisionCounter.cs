using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    public Text collisionCounterText;
    static public int collisionCount;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionCounterText != null)
        {
            collisionCounterText.text = $"Count of collisions: {collisionCount}";
        }
    }
}
