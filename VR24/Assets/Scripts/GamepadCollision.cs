using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadCollision : MonoBehaviour
{
    public byte r = 100;
    public byte g = 255;
    public byte b = 150;
    public byte alpha = 255;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        CollisionCounter.collisionCount += 1;
        if (CollisionCounter.collisionCount >= 5)
        {
            this.GetComponent<Renderer>().material.color = new Color32(r, g, b, alpha);
        }
    }
}
