using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : Singleton<DataLoader>
{
    private Animator _Ui;
    private RacketLayoutController _LayoutController;
    private List<ColorData> _ColorData;

    private bool _DataLoaded_Colors;

    public override void Awake()
    {
        base.Awake();

        _ColorData = new List<ColorData>();
        _Ui = GameObject.Find("Canvas - UI").GetComponent<Animator>();
        _LayoutController = GameObject.Find("Layout Controller").GetComponent<RacketLayoutController>();
    }

    private void Start()
    {
        StartCoroutine(GetRequest("//localhost/dahcor_backend/GetColorData.php"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    _DataLoaded_Colors = false;
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    _DataLoaded_Colors = false;
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    _DataLoaded_Colors = false;
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("<color=green>" + pages[page] + " Successfull:</color>" + "\nReceived: " + webRequest.downloadHandler.text);

                    string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                    for (int i = 0; i < splitScores.Length; i++)
                    {
                        //throw an error if the string is not properly formatted
                        //if (!splitScores[i].Contains("."))
                        //    throw new Exception("Improperly formatted data from database " + splitScores[i]);

                        string[] temp = splitScores[i].Split('.');

                        var data = new ColorData();
                        data.name = temp[0];
                        data.color = temp[1];
                        data.price = temp[2];

                        _ColorData.Add(data);
                    }

                    _DataLoaded_Colors = true;

                    CheckIfAllDataIsLoaded();

                    break;
            }
        }
    }

    public List<ColorData> GetColorData() => _ColorData;

    private void CheckIfAllDataIsLoaded()
    {
        if (!_DataLoaded_Colors)
            return;


        // All data is Loaded
        _Ui.SetTrigger("StartFadeIn");

        _LayoutController.UpdateQuestionsWithNewData();
    }
}

public struct ColorData
{
    public string name;
    public string color;
    public string price;
}