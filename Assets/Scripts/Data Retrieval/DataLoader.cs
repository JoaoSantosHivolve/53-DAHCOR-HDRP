using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : Singleton<DataLoader>
{
    [SerializeField] private bool _UseLocalhost;
    private RacketLayoutController _LayoutController;

    [Header("   TEXTURES Data")]
    [SerializeField] private List<ColorData> _ColorData;
    [SerializeField] private List<TextureData> _WoodData;
    [Header("   DROPDOWN Data")]
    [SerializeField] private List<CountryFlagsData> _CountryFlagsData;

    private readonly string _GetColorDataPhp = "GetColorData.php";
    private readonly string _GetTextureDataPhp = "GetTextureData.php";
    private readonly string _GetCountryFlagDataPhp = "GetCountryFlagData.php"; 

    private bool _IsColorDataLoaded;
    private bool _IsWoodsDataLoaded;
    private bool _IsCountryFlagDataLoaded;

    private string _ServerName;
    private string _UserName;
    private string _Password;
    private string _ScriptsPath;

    private const int TEXTURE_SIZE = 256;
    private const string WOOD_DB = "Woods";

    public override void Awake()
    {
        base.Awake();

        _ServerName = _UseLocalhost ? "localhost" : "13.38.61.207";
        _UserName = _UseLocalhost ? "root" : "dahcor";
        _Password = _UseLocalhost ? "" : "dahcor";
        _ScriptsPath = "//" + _ServerName + "/dahcor_backend/";

        _ColorData = new List<ColorData>();
        _WoodData = new List<TextureData>();

        _LayoutController = GameObject.Find("Layout Controller").GetComponent<RacketLayoutController>();
    }

    private void Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("servername", _ServerName);
        form.AddField("UserNamePost", _UserName);
        form.AddField("PasswordPost", _Password);

        // Color data
        StartCoroutine(RequestColorData(_ScriptsPath + _GetColorDataPhp, form));
        // Immaterials
        StartCoroutine(RequestTextureData(_ScriptsPath + _GetTextureDataPhp, form, WOOD_DB, _WoodData, verifier => _IsWoodsDataLoaded = verifier));
        // Flags Data
        StartCoroutine(RequestCountryFlagData(_ScriptsPath + _GetCountryFlagDataPhp, form, _CountryFlagsData, verifier => _IsCountryFlagDataLoaded = verifier));
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
                        data.id = temp[0];
                        if (ColorUtility.TryParseHtmlString("#" + temp[1], out var convertedColor))
                        {
                            data.color = convertedColor;
                        }

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
    private IEnumerator RequestTextureData(string uri, WWWForm form, string database, List<TextureData> data, System.Action<bool> verifier)
    {
        // Add select database to get data
        form.AddField("DataBase", database);

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
                        textureData.id = temp[0];
                        textureData.byoName = temp[1];
                        textureData.cgaixList = temp[2];
                        textureData.originalName = temp[3];

                        var textureString = temp[4];
                        var convertedTextureData = System.Convert.FromBase64String(textureString);
                        var texture = new Texture2D(TEXTURE_SIZE, TEXTURE_SIZE);
                        texture.LoadImage(convertedTextureData);
                        var textureSprite = Sprite.Create(texture,
                                new Rect(0.0f, 0.0f, texture.width, texture.height),
                                new Vector2(0.5f, 0.5f), 100.0f);
                        textureData.baseMap = textureSprite;

                        data.Add(textureData);
                    }

                    if (AllDataIsLoaded())
                        _LayoutController.SetupApp();

                    break;
            }
        }
    }
    private IEnumerator RequestCountryFlagData(string uri, WWWForm form, List<CountryFlagsData> data, System.Action<bool> verifier)
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

                        var countryFlagData = new CountryFlagsData();
                        countryFlagData.name = temp[0];
                        countryFlagData.image = temp[1];

                        data.Add(countryFlagData);
                    }

                    if (AllDataIsLoaded())
                        _LayoutController.SetupApp();

                    break;
            }
        }
    }
    public List<ColorData> GetColorData() => _ColorData;
    public List<TextureData> GetWoodData() => _WoodData;
    public List<CountryFlagsData> GetCountryFlagData() => _CountryFlagsData;

    private bool AllDataIsLoaded()
    {
        if (!_IsColorDataLoaded)
            return false;

        if (!_IsWoodsDataLoaded)
            return false;

        //if (!_IsCountryFlagDataLoaded)
        //    return false;

        Debug.Log("<color=green> All Data Loaded Sucessfully! </color>");
        return true;

    }
}

[Serializable]
public struct ColorData
{
    public string id;
    public Color color;
}
[Serializable]
public struct TextureData
{
    public string id;
    public string byoName;
    public string cgaixList;
    public string originalName;
    public Sprite baseMap;
}
[Serializable]
public struct CountryFlagsData
{
    public string name;
    public string image;
}