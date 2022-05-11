using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : Singleton<DataLoader>
{
    private RacketLayoutController _LayoutController;

    [SerializeField] private List<ColorData> _ColorData;
    [SerializeField] private List<TextureData> _SkinData;
    [SerializeField] private List<TextureData> _ElementData;
    [SerializeField] private List<TextureData> _PreciousData;

    private readonly string _ScriptsPath = "//localhost/dahcor_backend/";
    private readonly string _GetColorDataPhp = "GetColorData.php";
    private readonly string _GetSkinsDataPhp = "GetSkinData.php";
    private readonly string _GetSkinsElementPhp = "GetElementData.php";
    private readonly string _GetSkinsPreciousPhp = "GetPreciousData.php";

    private bool _DataLoaded_Colors;
    private bool _DataLoaded_Skins;
    private bool _DataLoaded_Element;
    private bool _DataLoaded_Precious;

    public override void Awake()
    {
        base.Awake();

        _ColorData = new List<ColorData>();
        _SkinData = new List<TextureData>();
        _ElementData = new List<TextureData>();
        _PreciousData = new List<TextureData>();

        _LayoutController = GameObject.Find("Layout Controller").GetComponent<RacketLayoutController>();
    }

    private void Start()
    {
        StartCoroutine(RequestColorData(_ScriptsPath + _GetColorDataPhp));
        StartCoroutine(RequestTextureData(_ScriptsPath + _GetSkinsDataPhp, _SkinData, verifier => _DataLoaded_Skins = verifier));
        StartCoroutine(RequestTextureData(_ScriptsPath + _GetSkinsElementPhp, _ElementData, verifier => _DataLoaded_Element = verifier));
        StartCoroutine(RequestTextureData(_ScriptsPath + _GetSkinsPreciousPhp, _PreciousData, verifier => _DataLoaded_Precious = verifier));
    }

    IEnumerator RequestColorData(string uri)
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
    IEnumerator RequestTextureData(string uri, List<TextureData> data, System.Action<bool> verifier)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    verifier.Invoke(false);
                    break;
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    verifier.Invoke(false);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    verifier.Invoke(false);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("<color=green>" + pages[page] + " Successfull:</color>" + "\nReceived: " + webRequest.downloadHandler.text);

                    string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                    for (int i = 0; i < splitScores.Length; i++)
                    {
                        string[] temp = splitScores[i].Split('.');

                        var textureData = new TextureData();
                        textureData.name = temp[0];
                        textureData.texture = temp[1];
                        textureData.price = temp[2];

                        data.Add(textureData);
                    }

                    verifier.Invoke(true);

                    CheckIfAllDataIsLoaded();

                    break;
            }
        }
    }
    
    public List<ColorData> GetColorData() => _ColorData;
    public List<TextureData> GetSkinData() => _SkinData;
    public List<TextureData> GetElementData() => _ElementData;
    public List<TextureData> GetPreciousData() => _PreciousData;


    private void CheckIfAllDataIsLoaded()
    {
        if (!_DataLoaded_Colors)
            return;

        if (!_DataLoaded_Skins)
            return;

        if (!_DataLoaded_Element)
            return;

        if (!_DataLoaded_Precious)
            return;

        Debug.Log("<color=green>All Data Loaded Sucessfully! </color>");

        _LayoutController.SetupApp();
    }
}

[Serializable]
public struct ColorData
{
    public string name;
    public string color;
    public string price;
}
[Serializable]
public struct TextureData
{
    public string name;
    public string texture;
    public string price;
}