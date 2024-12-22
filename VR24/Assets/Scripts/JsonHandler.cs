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
