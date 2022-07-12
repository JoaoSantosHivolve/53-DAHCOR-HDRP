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
    [SerializeField] private List<TextureData> _WoodsData;
    [SerializeField] private List<TextureData> _RocksData;
    [SerializeField] private List<TextureData> _ScifiData;
    [SerializeField] private List<TextureData> _FabricsData;
    [SerializeField] private List<TextureData> _OrganicData;
    [SerializeField] private List<TextureData> _MilitaryData;
    [SerializeField] private List<TextureData> _AnimalsData;
    [SerializeField] private List<TextureData> _PreciousData;
    [SerializeField] private List<TextureData> _AllTexturesData;
    [Header("   DROPDOWN Data")]
    [SerializeField] private List<CountryFlagsData> _CountryFlagsData;
    [Header("   PRICE Data")]
    [SerializeField] private List<PriceData> _PriceData;
    [SerializeField] private List<SpecificPriceData> _SpecificPriceData;
    [Header("   MODEL Data")]
    [SerializeField] private List<ModelData> _BodyData;
    [SerializeField] private List<ModelData> _HeadData;
    [SerializeField] private List<ModelData> _GripData;
    [SerializeField] private List<ModelData> _ButtcapData;

    private string _ServerName;
    private string _UserName;
    private string _Password;
    private string _ScriptsPath;

    private bool _IsColorDataLoaded;
    private bool _IsAllTextureDataLoaded;
    private bool _IsCountryFlagDataLoaded;
    private bool _IsPriceDataLoaded;
    private bool _IsSpecificPriceDataLoaded;

    //private bool _IsWoodsTextureDataLoaded;
    //private bool _IsRocksTextureDataLoaded;
    //private bool _IsScifiTextureDataLoaded;
    //private bool _IsFabricsTextureDataLoaded;
    //private bool _IsOrganicTextureDataLoaded;
    //private bool _IsMilitaryTextureDataLoaded;
    //private bool _IsAnimalsTextureDataLoaded;
    //private bool _IsPreciousTextureDataLoaded;

    private const int TEXTURE_SIZE = 256;

    private const string GET_COLOR_DATA_PHP = "GetColorData.php";
    private const string GET_TEXTURE_DATA_PHP = "GetTextureData.php";
    private const string GET_TEXTURE_DATA_COUNT_PHP = "GetTextureDataCount.php";
    private const string GET_TEXTURE_DATA_INDEX_PHP = "GetIndexTextureData.php";
    private const string GET_COUNTRY_FLAGS_DATA_PHP = "GetCountryFlagData.php";
    private const string GET_PRICE_DATA_PHP = "GetPriceData.php";
    private const string GET_SPECIFIC_PRICE_DATA_PHP = "GetSpecificPriceData.php";

    private const string WOOD_CATEGORY = "woods";
    private const string ROCKS_CATEGORY = "rocks";
    private const string SCIFI_CATEGORY = "scifi";
    private const string FABRICS_CATEGORY = "fabrics";
    private const string ORGANIC_CATEGORY = "organic";
    private const string MILITARY_CATEGORY = "military";
    private const string ANIMALS_CATEGORY = "animals";
    private const string PRECIOUS_CATEGORY = "precious";

    public override void Awake()
    {
        base.Awake();
        //13.38.129.52
        _ServerName = _UseLocalhost ? "localhost" : "13.38.129.52";
        //_ServerName = _UseLocalhost ? "localhost" : "13.38.61.207";
        _UserName = _UseLocalhost ? "root" : "dahcor";
        _Password = _UseLocalhost ? "" : "dahcor";
        _ScriptsPath = "//" + _ServerName + "/dahcor_backend/";

        // - Color Data
        _ColorData = new List<ColorData>();
        // -- Texture Data
        _AllTexturesData = new List<TextureData>();
        _WoodsData = new List<TextureData>();
        _RocksData = new List<TextureData>();
        _ScifiData = new List<TextureData>();
        _FabricsData = new List<TextureData>();
        _OrganicData = new List<TextureData>();
        _MilitaryData = new List<TextureData>();
        _AnimalsData = new List<TextureData>();
        _PreciousData = new List<TextureData>();
        // --- Country Data
        _CountryFlagsData = new List<CountryFlagsData>();
        // ---- Price
        _PriceData = new List<PriceData>();
        _SpecificPriceData = new List<SpecificPriceData>();
        //_BodyData = new List<ModelData>();
        //_HeadData = new List<ModelData>();
        //_ButtcapData = new List<ModelData>();

        _LayoutController = GameObject.Find("Layout Controller").GetComponent<RacketLayoutController>();
    }
    private void Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("servername", _ServerName);
        form.AddField("UserNamePost", _UserName);
        form.AddField("PasswordPost", _Password);

        // ----- COLOR DATA
        StartCoroutine(RequestColorData(_ScriptsPath + GET_COLOR_DATA_PHP, form));
        // ----- GET ALL TEXTURE DATA
        StartCoroutine(RequestAllTextureData(_ScriptsPath + GET_TEXTURE_DATA_COUNT_PHP, form, _ScriptsPath + GET_TEXTURE_DATA_INDEX_PHP));

        // ----- IMMATERIALS
        //StartCoroutine(RequestTextureData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, WOOD_CATEGORY, _WoodsData, verifier => _IsWoodsTextureDataLoaded = verifier));
        //StartCoroutine(RequestTextureData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, ROCKS_CATEGORY, _RocksData, verifier => _IsRocksTextureDataLoaded = verifier));
        //StartCoroutine(RequestTextureData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, SCIFI_CATEGORY, _ScifiData, verifier => _IsScifiTextureDataLoaded = verifier));
        //StartCoroutine(RequestTextureData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, FABRICS_CATEGORY, _FabricsData, verifier => _IsFabricsTextureDataLoaded = verifier));
        //StartCoroutine(RequestTextureData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, ORGANIC_CATEGORY, _OrganicData, verifier => _IsOrganicTextureDataLoaded = verifier));
        //StartCoroutine(RequestTextureData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, MILITARY_CATEGORY, _MilitaryData, verifier => _IsMilitaryTextureDataLoaded = verifier));
        //StartCoroutine(RequestTextureData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, ANIMALS_CATEGORY, _AnimalsData, verifier => _IsAnimalsTextureDataLoaded = verifier));
        //StartCoroutine(RequestTextureData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, PRECIOUS_CATEGORY, _PreciousData, verifier => _IsPreciousTextureDataLoaded = verifier));
        // ----- FLAG DATA
        StartCoroutine(RequestCountryFlagData(_ScriptsPath + GET_COUNTRY_FLAGS_DATA_PHP, form, _CountryFlagsData, verifier => _IsCountryFlagDataLoaded = verifier));
        // ----- PRICE DATA
        StartCoroutine(RequestPriceData(_ScriptsPath + GET_PRICE_DATA_PHP, form));
        StartCoroutine(RequestSpecificPriceData(_ScriptsPath + GET_SPECIFIC_PRICE_DATA_PHP, form));
    }

    private IEnumerator RequestColorData(string uri, WWWForm form)
    {
        Debug.Log("<color=yellow> Requesting Color Data...</color>");

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
                        string[] temp = splitScores[i].Split(',');

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
    private IEnumerator RequestAllTextureData(string uri, WWWForm form, string getTexturaDataViaIdUri)
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
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Data Processing Error]: " + webRequest.error + "</color>");
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(webRequest.downloadHandler.text);
                    int count = int.Parse(webRequest.downloadHandler.text);
                    int currentIndex = 1;

                    // ----- REQUESTING DATA
                    while (currentIndex <= count)
                    {
                        Debug.Log("<color=yellow> Requesting Texture Data At: " + currentIndex + " ...</color>");
                        var newForm = form;
                        newForm.AddField("Id", currentIndex);

                        using (UnityWebRequest textureWebRequest = UnityWebRequest.Post(getTexturaDataViaIdUri, newForm))
                        {
                            yield return textureWebRequest.SendWebRequest();

                            pages = getTexturaDataViaIdUri.Split('/');
                            page = pages.Length - 1;

                            switch (textureWebRequest.result)
                            {
                                case UnityWebRequest.Result.ConnectionError:
                                    Debug.LogError("<color=red>" + pages[page] + ": Error [Connection Error]: " + textureWebRequest.error + "</color>");
                                    break;

                                case UnityWebRequest.Result.DataProcessingError:
                                    Debug.LogError("<color=red>" + pages[page] + ": Error [Data Processing Error]: " + textureWebRequest.error + "</color>");
                                    break;

                                case UnityWebRequest.Result.ProtocolError:
                                    Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + textureWebRequest.error + "</color>");
                                    break;
                                case UnityWebRequest.Result.Success:
                                    string[] splitScores = textureWebRequest.downloadHandler.text.Trim().Split('\n');
                                    for (int i = 0; i < splitScores.Length; i++)
                                    {
                                        string[] temp = splitScores[i].Split(',');

                                        var textureData = new TextureData();
                                        textureData.id = temp[0];
                                        textureData.category = temp[1];
                                        textureData.byoName = temp[2];
                                        textureData.cgaixList = temp[3];
                                        textureData.originalName = temp[4];
                                        textureData.baseMap = GetSpriteFromString(temp[5]);
                                        textureData.maskMap = GetSpriteFromString(temp[6]);
                                        textureData.normalMap = GetSpriteFromString(temp[7]);
                                        textureData.tiling = new Vector2(float.Parse(temp[8]), float.Parse(temp[9]));

                                        _AllTexturesData.Add(textureData);
                                    }

                                    break;
                            }
                        }

                        currentIndex++;

                        yield return null;
                    }

                    OrganizeTextureData();

                    _IsAllTextureDataLoaded = true;

                    if (AllDataIsLoaded())
                        _LayoutController.SetupApp();

                    break;

            }
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                
            }
        }
    }
    private IEnumerator RequestTextureData(string uri, WWWForm form, string category ,List<TextureData> data, System.Action<bool> verifier)
    {
        Debug.Log("<color=yellow> Requesting " + category + " Texture Data...</color>");
        form.AddField("Category", category);

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            webRequest.SendWebRequest();

            while (!webRequest.isDone)
            {
                yield return new WaitForSeconds(2);
                Debug.Log(category + " download Progress: " + ((webRequest.downloadedBytes / 1024) / 1024) + "MB");
            }

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Connection Error]: " + webRequest.error + "</color>");
                    verifier.Invoke(false);
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Data Processing Error]: " + webRequest.error + "</color>");
                    verifier.Invoke(false);
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                    verifier.Invoke(false);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("<color=green>SUCCESSFULL: </color>" + category + "\n Data Received");

                    try
                    {
                        string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                        for (int i = 0; i < splitScores.Length; i++)
                        {
                            string[] temp = splitScores[i].Split(',');

                            var textureData = new TextureData();
                            textureData.id = temp[0];
                            textureData.category = temp[1];
                            textureData.byoName = temp[2];
                            textureData.cgaixList = temp[3];
                            textureData.originalName = temp[4];
                            textureData.baseMap = GetSpriteFromString(temp[5]);
                            textureData.maskMap = GetSpriteFromString(temp[6]);
                            textureData.normalMap = GetSpriteFromString(temp[7]);
                            textureData.tiling = new Vector2(float.Parse(temp[8]), float.Parse(temp[9]));

                            data.Add(textureData);
                        }

                        verifier.Invoke(true);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Error adding " + category + " data! " + e.Message);
                    }

                    if (AllDataIsLoaded())
                        _LayoutController.SetupApp();

                    //OrganizeTextureData();

                    break;
            }
        }
    }
    private IEnumerator RequestCountryFlagData(string uri, WWWForm form, List<CountryFlagsData> data, System.Action<bool> verifier)
    {
        Debug.Log("<color=yellow> Requesting Country Flag Data...</color>");

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
                        string[] temp = splitScores[i].Split(',');

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
    private IEnumerator RequestPriceData(string uri, WWWForm form)
    {
        Debug.Log("<color=yellow> Requesting Price Data...</color>");

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Connection Error]: " + webRequest.error + "</color>");
                    _IsPriceDataLoaded = false;
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Data Processing Error]: " + webRequest.error + "</color>");
                    _IsPriceDataLoaded = false;
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                    _IsPriceDataLoaded = false;
                    break;
                case UnityWebRequest.Result.Success:
                    try
                    {
                        string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                        for (int i = 0; i < splitScores.Length; i++)
                        {
                            string[] temp = splitScores[i].Split(',');

                            var data = new PriceData();
                            data.category = PriceManager.Instance.GetPriceCategory(int.Parse(temp[0]));
                            data.body_minimal = int.Parse(temp[1]);
                            data.body_outline_offbeat = int.Parse(temp[2]);
                            data.head_mono_break = int.Parse(temp[3]);
                            data.head_deuce = int.Parse(temp[4]);
                            data.bumper = int.Parse(temp[5]);
                            data.handle = int.Parse(temp[6]);
                            data.name = data.category.ToString();

                            _PriceData.Add(data);
                        }

                        _IsPriceDataLoaded = true;

                        Debug.Log("<color=green>SUCCESSFULL: Price Data Received from " + pages[page] + "!</color>");
                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=red>ERROR adding price data! " + e.Message + "</color>");
                    }

                    if (AllDataIsLoaded())
                        _LayoutController.SetupApp();

                    break;
            }
        }
    }
    private IEnumerator RequestSpecificPriceData(string uri, WWWForm form)
    {
        Debug.Log("<color=yellow> Requesting Specific Price Data...</color>");

        using (UnityWebRequest webRequest = UnityWebRequest.Post(uri, form))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Connection Error]: " + webRequest.error + "</color>");
                    _IsSpecificPriceDataLoaded = false;
                    break;

                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Data Processing Error]: " + webRequest.error + "</color>");
                    _IsSpecificPriceDataLoaded = false;
                    break;

                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                    _IsSpecificPriceDataLoaded = false;
                    break;
                case UnityWebRequest.Result.Success:
                    try
                    {
                        string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                        for (int i = 0; i < splitScores.Length; i++)
                        {
                            string[] temp = splitScores[i].Split(',');

                            var data = new SpecificPriceData();
                            data.body_minimal = int.Parse(temp[1]);
                            data.body_outline = int.Parse(temp[2]);
                            data.body_offbeat = int.Parse(temp[3]);
                            data.head_no_costumization = int.Parse(temp[4]);
                            data.head_mono = int.Parse(temp[5]);
                            data.head_break = int.Parse(temp[6]);
                            data.head_deuce = int.Parse(temp[7]);
                            data.lettering_gloss = int.Parse(temp[8]);
                            data.lettering_matte = int.Parse(temp[9]);
                            data.lettering_metal = int.Parse(temp[10]);
                            data.lettering_chrome = int.Parse(temp[11]);
                            data.buttcap_standart = int.Parse(temp[12]);
                            data.buttcap_pro = int.Parse(temp[13]);
                            data.strings_no_costumization = int.Parse(temp[14]);
                            data.strings_colormap = int.Parse(temp[15]);
                            data.autograph_yes = int.Parse(temp[16]);


                            _SpecificPriceData.Add(data);
                        }

                        _IsSpecificPriceDataLoaded = true;

                        Debug.Log("<color=green>SUCCESSFULL: Specific Price Data Received from " + pages[page] + "!</color>");
                    }
                    catch (Exception e)
                    {
                        Debug.Log("<color=red>ERROR adding specific price data! " + e.Message + "</color>");
                    }

                    if (AllDataIsLoaded())
                        _LayoutController.SetupApp();

                    break;
            }
        }
    }

    public List<ColorData> GetColorData() => _ColorData;
    public List<TextureData> GetWoodData() => _WoodsData;
    public List<TextureData> GetRocksData() => _RocksData;
    public List<TextureData> GetScifiData() => _ScifiData;
    public List<TextureData> GetFabricsData() => _FabricsData;
    public List<TextureData> GetOrganicData() => _OrganicData;
    public List<TextureData> GetMilitaryData() => _MilitaryData;
    public List<TextureData> GetAnimalsData() => _AnimalsData;
    public List<TextureData> GetPreciousData() => _PreciousData;
    public List<CountryFlagsData> GetCountryFlagData() => _CountryFlagsData;
    public List<PriceData> GetPriceData() => _PriceData;
    public List<SpecificPriceData> GetSpecificPriceData() => _SpecificPriceData;
    public List<ModelData> GetBodyData() => _BodyData;
    public List<ModelData> GetHeadData() => _HeadData;
    public List<ModelData> GetGripData() => _GripData;
    public List<ModelData> GetButtcapData() => _ButtcapData;

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
        // - Colors Data
        if (!_IsColorDataLoaded)
            return false;
        // -- Texture Data
        if (!_IsAllTextureDataLoaded)
            return false;
        //if (!_IsWoodsTextureDataLoaded)
        //    return false;
        //if (!_IsRocksTextureDataLoaded)
        //    return false;
        //if (!_IsScifiTextureDataLoaded)
        //    return false;
        //if (!_IsFabricsTextureDataLoaded)
        //    return false;
        //if (!_IsOrganicTextureDataLoaded)
        //    return false;
        //if (!_IsMilitaryTextureDataLoaded)
        //    return false;
        //if (!_IsAnimalsTextureDataLoaded)
        //    return false;
        //if (!_IsPreciousTextureDataLoaded)
        //    return false;

        // --- Countries Data
        if (!_IsCountryFlagDataLoaded)
            return false;

        // ---- Price Data
        if (!_IsPriceDataLoaded)
            return false;
        if (!_IsSpecificPriceDataLoaded)
            return false;

        Debug.Log("<color=green> All Data Loaded Sucessfully! </color>");
        return true;
    }
    private Sprite GetSpriteFromString(string textureString)
    {
        var convertedTextureData = System.Convert.FromBase64String(textureString);
        var texture = new Texture2D(TEXTURE_SIZE, TEXTURE_SIZE);
        texture.LoadImage(convertedTextureData);
        var textureSprite = Sprite.Create(texture,
                new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 100.0f);

        return textureSprite;
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
    public Sprite maskMap;
    public Sprite normalMap;
    public Vector2 tiling;
}
[Serializable]
public struct CountryFlagsData
{
    public string name;
    public string image;
}
[Serializable]
public struct ModelData
{
    public string id;
    public int materialCount;
    public Mesh mesh;
}
[Serializable]
public struct PriceData
{
    public string name;
    public RacketPriceCategory category;
    public int body_minimal;
    public int body_outline_offbeat;
    public int head_mono_break;
    public int head_deuce;
    public int bumper;
    public int handle;
}
[Serializable]
public struct SpecificPriceData
{
    public int body_minimal;
    public int body_outline;
    public int body_offbeat;
    public int head_no_costumization;
    public int head_mono;
    public int head_break;
    public int head_deuce;
    public int lettering_gloss;
    public int lettering_matte;
    public int lettering_metal;
    public int lettering_chrome;
    public int buttcap_standart;
    public int buttcap_pro;
    public int strings_no_costumization;
    public int strings_colormap;
    public int autograph_yes;
}