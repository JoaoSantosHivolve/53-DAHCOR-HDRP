using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DataLoader : Singleton<DataLoader>
{
    [SerializeField] private bool _UseLocalhost;
    private RacketLayoutController _LayoutController;

    [Header("   COLORS Data")]
    [SerializeField] private List<ColorData> _ColorData;
    [Header("   TEXTURES Data")]
    private List<TextureData> _AllTexturesData;
    [SerializeField] private List<TextureData> _WoodsData;
    [SerializeField] private List<TextureData> _RocksData;
    [SerializeField] private List<TextureData> _ScifiData;
    [SerializeField] private List<TextureData> _FabricsData;
    [SerializeField] private List<TextureData> _OrganicData;
    [SerializeField] private List<TextureData> _MilitaryData;
    [SerializeField] private List<TextureData> _AnimalsData;
    [SerializeField] private List<TextureData> _PreciousData;
    [Header("   DROPDOWN Data")]
    [SerializeField] private List<CountryFlagsData> _CountryFlagsData;
    [Header("   MODEL Data")]
    [SerializeField] private List<GameObject> _ButtcapData;

    private readonly string _GetColorDataPhp = "GetColorData.php";
    private readonly string _GetTextureDataPhp = "GetTextureData.php";
    private readonly string _GetCountryFlagDataPhp = "GetCountryFlagData.php"; 

    private bool _IsColorDataLoaded;
    private bool _IsTextureDataLoaded;
    private bool _IsCountryFlagDataLoaded;

    private string _ServerName;
    private string _UserName;
    private string _Password;
    private string _ScriptsPath;

    private const int TEXTURE_SIZE = 256;
    private const string WOOD_CATEGORY = "woods";
    private const string ROCKS_CATEGORY = "rocks";
    private const string SCIFI_CATEGORY = "sci-fi";
    private const string FABRICS_CATEGORY = "fabrics";
    private const string ORGANIC_CATEGORY = "organic";
    private const string MILITARY_CATEGORY = "military";
    private const string ANIMALS_CATEGORY = "animals";
    private const string PRECIOUS_CATEGORY = "precious";

    public override void Awake()
    {
        base.Awake();

        _ServerName = _UseLocalhost ? "localhost" : "13.38.61.207";
        _UserName = _UseLocalhost ? "root" : "dahcor";
        _Password = _UseLocalhost ? "" : "dahcor";
        _ScriptsPath = "//" + _ServerName + "/dahcor_backend/";

        _ColorData = new List<ColorData>();
        _AllTexturesData = new List<TextureData>();
        _WoodsData = new List<TextureData>();
        _RocksData = new List<TextureData>();
        _ScifiData = new List<TextureData>();
        _FabricsData = new List<TextureData>();
        _OrganicData = new List<TextureData>();
        _MilitaryData = new List<TextureData>();
        _AnimalsData = new List<TextureData>();
        _PreciousData = new List<TextureData>();
        _CountryFlagsData = new List<CountryFlagsData>();

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
        StartCoroutine(RequestTextureData(_ScriptsPath + _GetTextureDataPhp, form, _AllTexturesData, verifier => _IsTextureDataLoaded = verifier));
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
                        textureData.id = temp[0];
                        textureData.category = temp[1];
                        textureData.byoName = temp[2];
                        textureData.cgaixList = temp[3];
                        textureData.originalName = temp[4];

                        var textureString = temp[5];
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

                    OrganizeTextureData();

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
    public List<TextureData> GetWoodData() => _WoodsData;
    public List<CountryFlagsData> GetCountryFlagData() => _CountryFlagsData;
    public List<GameObject> GetButtcapData() => _ButtcapData;

    private void OrganizeTextureData()
    {
        foreach (var item in _AllTexturesData)
        {
            switch (item.category)
            {
                case WOOD_CATEGORY:
                    _WoodsData.Add(item);
                    break;
                case ROCKS_CATEGORY:
                    _RocksData.Add(item);
                    break;
                case SCIFI_CATEGORY:
                    _ScifiData.Add(item);
                    break;
                case FABRICS_CATEGORY:
                    _FabricsData.Add(item);
                    break;
                case ORGANIC_CATEGORY:
                    _OrganicData.Add(item);
                    break;
                case MILITARY_CATEGORY:
                    _MilitaryData.Add(item);
                    break;
                case ANIMALS_CATEGORY:
                    _AnimalsData.Add(item);
                    break;
                case PRECIOUS_CATEGORY:
                    _PreciousData.Add(item);
                    break;
                default:
                    Debug.Log("File with invalid category: " + item.category);
                    break;
            }
        }
    }
    private bool AllDataIsLoaded()
    {
        if (!_IsColorDataLoaded)
            return false;

        if (!_IsTextureDataLoaded)
            return false;

        if (!_IsCountryFlagDataLoaded)
            return false;

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
    public string category;
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