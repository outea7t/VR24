# VR24
- Репозиторий с кодом после лекций по предмету "Основы виртуальной реальности в машиностроении" и выполненными домашними заданиями.

## ДЗ №1

```
(создать проект)
```

## ДЗ №2

Движение куба по гантелеобразной траектории

```cs
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{   
    public Text Mytxt;
    public GameObject cube;
    public float radius = 0.01f;
    public float angle = 0.0f;
    public Vector3 positionOfCenter;

    // Start is called before the first frame update
    void Start()
    {
      if (cube != null)
      {
        positionOfCenter = cube.transform.position;
      }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cube == null) 
        {
            Debug.Log("cube is not initialized");
            return;
        }
        if (Mytxt == null)
        {
            Debug.Log("text is not initialized");
            return;
        }
        angle += Time.deltaTime;

        // куб будет двигаться по гантелеобразной траектории
        float newXPos = (positionOfCenter.x + MathF.Cos(angle) * 2) * radius;
        float newZPos = (positionOfCenter.y + MathF.Sin(angle * 2)) * radius;

        cube.transform.position = new Vector3(newXPos, cube.transform.position.y, newZPos);

        Mytxt.text = $"Угол: {(int)(angle * 180 / MathF.PI) % 360}";
    }
}

```

