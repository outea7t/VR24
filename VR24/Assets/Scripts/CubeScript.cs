using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class CubeScript : MonoBehaviour
{

    public byte r = 242;
    public byte g = 100;
    public byte b = 100;
    public byte alpha = 255;

    public bool isColorToggled = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickButton() 
    {   
        if (!isColorToggled)
        {
            this.GetComponent<Renderer>().material.color = new Color32(r, g, b, alpha);
        }
        else
        {
            this.GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 255);
        }

        isColorToggled = !isColorToggled;
    }
}
