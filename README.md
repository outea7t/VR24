# VR24
- Репозиторий с кодом после лекций по предмету "Основы виртуальной реальности в машиностроении" и выполненными домашними заданиями.

## ДЗ №1

```
(создать проект)
```

## ДЗ №2

- Движение куба по гантелеобразной траектории

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

## ДЗ №3

- Добавил наклоненную плоскость, по которой скатывается модель геймпада, также считаются столковения геймпада с плоскостями, и их количество выводится на экран. Если количество столкновений больше или равно 5, то сердцевина геймпада меняет цвет с синего на зеленый

GamepadCollision.cs:
```cs
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
```

CollisionCounter.cs:
```cs
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

```



