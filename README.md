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

## ДЗ №4

- Сделал модель дома в блендере, добавил текстуры на нее, добавил модель в проект

  <img width="622" alt="image" src="https://github.com/user-attachments/assets/7ea720bc-64ca-4da4-8473-e1fbd3442ff2">

## ДЗ №5 
- Добавил запрос к облачному хранилищу, где хранится `JSON` файл с содержанием:
```
{
    "header": "Эта информация была получена с облачного хранилища, загружена и вставлена в это текстовое поле",
    "bottomText": "Какой-то случайный текст, тоже загруженный с облачного хранилища",
    "planeColor": "red"
}
```

В скрипте (код скрипта ниже) я получаю этот файл, вытаскиваю из него информацию и значение полей `header` и `bottom` присваиваю присваиваю текстовым полям на сцене (фото ниже). Значение цвета `planeColor` я проверяю на значения `"red"`, `"green"` и `"blue"`, и присваиваю плоскости этот цвет.

<img width="1080" alt="Без названия" src="https://github.com/user-attachments/assets/bd5a63f7-bf8d-475d-8996-03380274ceca" />

Код скрипта: 
```
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class JsonHandler : MonoBehaviour
{
    public Text header;
    public Text bottomText;
    public GameObject coloredPlane;
    public string planeColor;
    public string jsonURL;
    public JsonClass jsonData;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(getData());
    }
    IEnumerator getData() 
    {
        Debug.Log("Загрузка...");

        var uwr = new UnityWebRequest(jsonURL);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.json");
        var dh = new DownloadHandlerFile(resultFile);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success) 
        {
            header.text = "Ошибка получения данных!";
        }
        else 
        {
            Debug.Log("Файл сохранен по пути:" + resultFile);
            jsonData = JsonUtility.FromJson<JsonClass>(File.ReadAllText(Application.persistentDataPath + "/result.json"));
            
            header.text = jsonData.header.ToString();
            bottomText.text = jsonData.bottomText.ToString();
            planeColor = jsonData.planeColor.ToString();
            GetColor(planeColor);

            yield return StartCoroutine(getData());
        }
    }

    void GetColor(string colorName) 
    {
        byte r = 100;
        byte g = 100;
        byte b = 100;
        byte alpha = 255;
        if (colorName == "red")
        {
            r = 255;
        }
        else if (colorName == "green")
        {
            g = 255;
        }
        else if (colorName == "blue") 
        {
            b = 255;
        }

        coloredPlane.GetComponent<Renderer>().material.color = new Color32(r, g, b, alpha);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    [System.Serializable]
    public class JsonClass
    {
        public string header;
        public string bottomText;
        public string planeColor;
    }
}
```


