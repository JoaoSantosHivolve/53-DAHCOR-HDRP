using Dummiesman;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : Singleton<DataLoader>
{
    [SerializeField] private bool _UseLocalhost;
    private RacketLayoutController _LayoutController;

    [SerializeField] private List<ColorData> _ColorData;
    [SerializeField] private List<TextureData> _SkinData;
    [SerializeField] private List<TextureData> _ElementData;
    [SerializeField] private List<TextureData> _PreciousData;
    [SerializeField] private List<ModelData> _ModelData;

    private readonly string _GetColorDataPhp = "GetColorData.php";
    private readonly string _GetSkinsDataPhp = "GetSkinData.php";
    private readonly string _GetElementDataPhp = "GetElementData.php";
    private readonly string _GetPreciousDataPhp = "GetPreciousData.php";

    private bool _IsColorDataLoaded;
    private bool _IsSkinsDataLoaded;
    private bool _IsElementDataLoaded;
    private bool _IsPreciousDataLoaded;

    private string _ServerName;
    private string _UserName;
    private string _Password;
    private string _ScriptsPath;

    public override void Awake()
    {
        base.Awake();

        _ServerName = _UseLocalhost ? "localhost" : "13.38.61.207";
        _UserName = _UseLocalhost ? "root" : "dahcor";
        _Password = _UseLocalhost ? "" : "dahcor";
        _ScriptsPath = "//" + _ServerName + "/dahcor_backend/";

        _ColorData = new List<ColorData>();
        _SkinData = new List<TextureData>();
        _ElementData = new List<TextureData>();
        _PreciousData = new List<TextureData>();
        _ModelData = new List<ModelData>();

        _LayoutController = GameObject.Find("Layout Controller").GetComponent<RacketLayoutController>();
    }

    private void Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("servername", _ServerName);
        form.AddField("UserNamePost", _UserName);
        form.AddField("PasswordPost", _Password);

        StartCoroutine(RequestColorData(_ScriptsPath + _GetColorDataPhp, form));
        StartCoroutine(RequestTextureData(_ScriptsPath + _GetSkinsDataPhp, form, _SkinData, verifier => _IsSkinsDataLoaded = verifier));
        StartCoroutine(RequestTextureData(_ScriptsPath + _GetElementDataPhp, form, _ElementData, verifier => _IsElementDataLoaded = verifier));
        StartCoroutine(RequestTextureData(_ScriptsPath + _GetPreciousDataPhp, form, _PreciousData, verifier => _IsPreciousDataLoaded = verifier));
    }

    private IEnumerator RequestColorData(string uri, WWWForm form)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Connection Error]: " + webRequest.error + "</color>");
                    _IsColorDataLoaded = false;
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Data Processing Error]: " + webRequest.error + "</color>");
                    _IsColorDataLoaded = false;
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                    _IsColorDataLoaded = false;
                    break;

                case UnityWebRequest.Result.Success:
                    Debug.Log("<color=green>" + pages[page] + " Successfull:</color>" + "\nReceived: " + webRequest.downloadHandler.text);
                    _IsColorDataLoaded = true;

                    string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                    for (int i = 0; i < splitScores.Length; i++)
                    {
                        string[] temp = splitScores[i].Split('.');

                        var data = new ColorData();
                        data.name = temp[0];
                        data.color = temp[1];
                        data.price = temp[2];

                        _ColorData.Add(data);
                    }

                    if (AllDataIsLoaded())
                        _LayoutController.SetupApp();

                    break;

                 default:
                    Debug.Log("None Of the above");
                    break;
            }
        }
    }
    private IEnumerator RequestTextureData(string uri, WWWForm form, List<TextureData> data, System.Action<bool> verifier)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Connection Error]: " + webRequest.error + "</color>");
                    _IsColorDataLoaded = false;
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Data Processing Error]: " + webRequest.error + "</color>");
                    _IsColorDataLoaded = false;
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                    _IsColorDataLoaded = false;
                    break;

                case UnityWebRequest.Result.Success:
                    Debug.Log("<color=green>" + pages[page] + " Successfull:</color>" + "\nReceived: " + webRequest.downloadHandler.text);
                    verifier.Invoke(true);

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

                    if (AllDataIsLoaded())
                        _LayoutController.SetupApp();

                    break;
            }
        }
    }

    public List<ColorData> GetColorData() => _ColorData;
    public List<TextureData> GetSkinData() => _SkinData;
    public List<TextureData> GetElementData() => _ElementData;
    public List<TextureData> GetPreciousData() => _PreciousData;

    private bool AllDataIsLoaded()
    {
        if (!_IsColorDataLoaded)
            return false;

        if (!_IsSkinsDataLoaded)
            return false;

        if (!_IsElementDataLoaded)
            return false;

        if (!_IsPreciousDataLoaded)
            return false;
        
        Debug.Log("<color=green> All Data Loaded Sucessfully! </color>");
        return true;

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
[Serializable]
public struct ModelData
{
    public string name;
    public string model;
    public string price;
}