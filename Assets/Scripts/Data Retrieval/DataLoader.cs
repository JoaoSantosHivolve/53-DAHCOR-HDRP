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

    [Header("   COLORS Data")]
    [SerializeField] private List<ColorData> _ColorData;
    [Header("   IMMATERIAL Data")]
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
    [Header("   PRICE Data")]
    [SerializeField] private List<PriceData> _PriceData;
    [SerializeField] private List<SpecificPriceData> _SpecificPriceData;
    [Header("   MODEL Data")]
    [SerializeField] private List<ModelData> _BodyData;
    [SerializeField] private List<ModelData> _HeadData;
    [SerializeField] private List<ModelData> _ButtcapData;
    [SerializeField] private List<ModelData> _GripData;

    private string _ServerName;
    private string _UserName;
    private string _Password;
    private string _ScriptsPath;

    private bool _IsColorDataLoaded;
    private bool _IsWoodsTextureDataLoaded;
    private bool _IsRocksTextureDataLoaded;
    private bool _IsScifiTextureDataLoaded;
    private bool _IsFabricsTextureDataLoaded;
    private bool _IsOrganicTextureDataLoaded;
    private bool _IsMilitaryTextureDataLoaded;
    private bool _IsAnimalsTextureDataLoaded;
    private bool _IsPreciousTextureDataLoaded;
    private bool _IsCountryFlagDataLoaded;
    private bool _IsPriceDataLoaded;
    private bool _IsSpecificPriceDataLoaded;
    private bool _IsBodyDataLoaded;
    private bool _IsHeadDataLoaded;
    private bool _IsButtcapDataLoaded;
    private bool _IsGripDataLoaded;

    private const int TEXTURE_SIZE = 256;

    private const string GET_COLOR_DATA_PHP = "GetColorData.php";
    private const string GET_TEXTURE_DATA_PHP = "GetTextureData.php";
    private const string GET_COUNTRY_FLAGS_DATA_PHP = "GetCountryFlagData.php";
    private const string GET_PRICE_DATA_PHP = "GetPriceData.php";
    private const string GET_SPECIFIC_PRICE_DATA_PHP = "GetSpecificPriceData.php";
    private const string GET_MODEL_DATA_PHP = "GetModelData.php";

    private const string WOOD_CATEGORY = "woods";
    private const string ROCKS_CATEGORY = "rocks";
    private const string SCIFI_CATEGORY = "scifi";
    private const string FABRICS_CATEGORY = "fabrics";
    private const string ORGANIC_CATEGORY = "organic";
    private const string MILITARY_CATEGORY = "military";
    private const string ANIMALS_CATEGORY = "animals";
    private const string PRECIOUS_CATEGORY = "precious";
    private const string BODY_CATEGORY = "body";
    private const string HEAD_CATEGORY = "head";
    private const string BUTTCAP_CATEGORY = "buttcap";
    private const string GRIP_CATEGORY = "grip";

    private DateTime _StartingTime;

    public override void Awake()
    {
        base.Awake();

        _ServerName = _UseLocalhost ? "localhost" : "15.188.166.198";
        _UserName = _UseLocalhost ? "root" : "dahcor";
        _Password = _UseLocalhost ? "" : "dahcor";
        _ScriptsPath = "//" + _ServerName + "/dahcor_backend/";

        // - Color Data
        _ColorData = new List<ColorData>();
        // -- Texture Data
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
        // ---- Price Data
        _PriceData = new List<PriceData>();
        _SpecificPriceData = new List<SpecificPriceData>();
        // ----- Model Data
        _BodyData = new List<ModelData>();
        _HeadData = new List<ModelData>();
        _ButtcapData = new List<ModelData>();
        _GripData = new List<ModelData>();

        _LayoutController = GameObject.Find("Layout Controller").GetComponent<RacketLayoutController>();

        _StartingTime = DateTime.Now;
    }

    private void Start()
    {
        WWWForm form = new WWWForm();
        form.AddField("servername", _ServerName);
        form.AddField("UserNamePost", _UserName);
        form.AddField("PasswordPost", _Password);

        // ----- COLOR DATA
        StartCoroutine(RequestColorData(_ScriptsPath + GET_COLOR_DATA_PHP, form));
        // ----- IMMATERIALS DATA
        StartCoroutine(RequestImmaterialsData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, WOOD_CATEGORY, _WoodsData, verifier => _IsWoodsTextureDataLoaded = verifier));
        StartCoroutine(RequestImmaterialsData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, ROCKS_CATEGORY, _RocksData, verifier => _IsRocksTextureDataLoaded = verifier));
        StartCoroutine(RequestImmaterialsData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, SCIFI_CATEGORY, _ScifiData, verifier => _IsScifiTextureDataLoaded = verifier));
        StartCoroutine(RequestImmaterialsData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, FABRICS_CATEGORY, _FabricsData, verifier => _IsFabricsTextureDataLoaded = verifier));
        StartCoroutine(RequestImmaterialsData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, ORGANIC_CATEGORY, _OrganicData, verifier => _IsOrganicTextureDataLoaded = verifier));
        StartCoroutine(RequestImmaterialsData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, MILITARY_CATEGORY, _MilitaryData, verifier => _IsMilitaryTextureDataLoaded = verifier));
        StartCoroutine(RequestImmaterialsData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, ANIMALS_CATEGORY, _AnimalsData, verifier => _IsAnimalsTextureDataLoaded = verifier));
        StartCoroutine(RequestImmaterialsData(_ScriptsPath + GET_TEXTURE_DATA_PHP, form, PRECIOUS_CATEGORY, _PreciousData, verifier => _IsPreciousTextureDataLoaded = verifier));
        // ----- FLAG DATA
        StartCoroutine(RequestCountryFlagData(_ScriptsPath + GET_COUNTRY_FLAGS_DATA_PHP, form, _CountryFlagsData));
        // ----- PRICE DATA
        StartCoroutine(RequestPriceData(_ScriptsPath + GET_PRICE_DATA_PHP, form));
        StartCoroutine(RequestSpecificPriceData(_ScriptsPath + GET_SPECIFIC_PRICE_DATA_PHP, form));
        // ----- MODEL
        StartCoroutine(RequestModelsData(_ScriptsPath + GET_MODEL_DATA_PHP, form, BODY_CATEGORY, _BodyData, verifier => _IsBodyDataLoaded = verifier));
        StartCoroutine(RequestModelsData(_ScriptsPath + GET_MODEL_DATA_PHP, form, HEAD_CATEGORY, _HeadData, verifier => _IsHeadDataLoaded = verifier));
        StartCoroutine(RequestModelsData(_ScriptsPath + GET_MODEL_DATA_PHP, form, BUTTCAP_CATEGORY, _ButtcapData, verifier => _IsButtcapDataLoaded = verifier));
        StartCoroutine(RequestModelsData(_ScriptsPath + GET_MODEL_DATA_PHP, form, GRIP_CATEGORY, _GripData, verifier => _IsGripDataLoaded = verifier));
    }
    // ----- COLOR Data
    private IEnumerator RequestColorData(string uri, WWWForm form)
    {
        Debug.Log("<color=yellow>Requesting </color><color=white>color </color><color=yellow>data...</color>");

        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:

                string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                for (int i = 0; i < splitScores.Length; i++)
                {
                    string[] temp = splitScores[i].Split(',');

                    var data = new ColorData();
                    data.id = temp[0];
                    data.hex = temp[1];
                    if (ColorUtility.TryParseHtmlString("#" + temp[1], out var convertedColor))
                    {
                        data.color = convertedColor;
                    }

                    _ColorData.Add(data);
                }

                _IsColorDataLoaded = true;
                Debug.Log("<color=green>Successfull data received:</color><color=white> color data</color>");

                if (AllDataIsLoaded())
                    _LayoutController.SetupApp();
                break;

            default:
                string[] pages = uri.Split('/');
                int page = pages.Length - 1;
                Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                _IsColorDataLoaded = false;
                break;
        }
    }
    // ----- IMMATERIALS Data
    private IEnumerator RequestImmaterialsData(string uri, WWWForm form, string category ,List<TextureData> data, System.Action<bool> verifier)
    {
        Debug.Log("<color=yellow>Requesting </color><color=white>" + category + "</color><color=yellow> texture data...</color>");

        form.AddField("Category", category);
        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
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
                        textureData.baseMapDirectory = temp[5];
                        textureData.maskMapDirectory = temp[6];
                        textureData.normalMapDirectory = temp[7];
                        textureData.tiling = new Vector2(float.Parse(temp[8]), float.Parse(temp[9]));

                        data.Add(textureData);
                    }

                    StartCoroutine(DownloadMaterialTextures(data, verifier));
                }
                catch (Exception e)
                {
                    Debug.Log("<color=red>Error adding " + category + " data! " + e.Message + "</color>");
                }
                break;
            default:
                string[] pages = uri.Split('/');
                int page = pages.Length - 1;
                Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                verifier.Invoke(false);
                break;
        }
    }
    private IEnumerator DownloadMaterialTextures(List<TextureData> data, System.Action<bool> dataVerifier)
    {
        for (int i = 0; i < data.Count; i++)
        {
            // Download base map
            var directory = data[i].baseMapDirectory;
            if (directory != "")
            {
                var cwd = new CoroutineWithData(this, DownloadTexture(directory));
                yield return cwd.coroutine;
                data[i].baseMap = (Sprite)cwd.result;
            }

            // Download mask map
            directory = data[i].maskMapDirectory;
            if (directory != "")
            {
                var cwd = new CoroutineWithData(this, DownloadTexture(directory));
                yield return cwd.coroutine;
                data[i].maskMap = (Sprite)cwd.result;
            }

            // Download normal map
            directory = data[i].normalMapDirectory;
            if (directory != "")
            {
                var cwd = new CoroutineWithData(this, DownloadTexture(directory));
                yield return cwd.coroutine;
                data[i].normalMap = (Sprite)cwd.result;
            }
        }

        dataVerifier.Invoke(true);
        Debug.Log("<color=green>Successfull data received:</color><color=white> " + data[0].category + "</color>");

        if (AllDataIsLoaded())
            _LayoutController.SetupApp();
    }
    private IEnumerator DownloadTexture(string directory)
    {
        using UnityWebRequest webRequest = new UnityWebRequest(directory, UnityWebRequest.kHttpVerbGET);
#if UNITY_WEBGL
        webRequest.SetRequestHeader("Access-Control-Allow-Credentials", "true");
        webRequest.SetRequestHeader("Access-Control-Allow-Headers", "x-requested-with, Content-Type, origin, authorization, Accepts, accept, client-security-token, access-control-allow-headers");
        webRequest.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
#endif
        webRequest.downloadHandler = new DownloadHandlerTexture();

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                var texture = DownloadHandlerTexture.GetContent(webRequest);
                yield return Sprite.Create(texture, new Rect(0.0f, 0.0f, TEXTURE_SIZE, TEXTURE_SIZE), new Vector2(0.5f, 0.5f), 100f);
                break;
            default:
                Debug.LogError("<color=red>: Error [Protocol Error] : Downloading " + directory + " data : " + webRequest.error + "</color>");
                break;
        }
    }
    // ----- COUNTRY FLAGS Data
    private IEnumerator RequestCountryFlagData(string uri, WWWForm form, List<CountryFlagsData> data)
    {
        Debug.Log("<color=yellow>Requesting </color><color=white>country flag </color><color=yellow>data...</color>");

        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:

                string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                for (int i = 0; i < splitScores.Length; i++)
                {
                    string[] temp = splitScores[i].Split(',');

                    var countryFlagData = new CountryFlagsData();
                    countryFlagData.name = temp[0];
                    countryFlagData.flag_directory = temp[1];
                    if(countryFlagData.flag_directory != "")
                    {
                        using UnityWebRequest flagRequest = new UnityWebRequest(countryFlagData.flag_directory, UnityWebRequest.kHttpVerbGET);
#if UNITY_WEBGL
        webRequest.SetRequestHeader("Access-Control-Allow-Credentials", "true");
        webRequest.SetRequestHeader("Access-Control-Allow-Headers", "x-requested-with, Content-Type, origin, authorization, Accepts, accept, client-security-token, access-control-allow-headers");
        webRequest.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
#endif
                        flagRequest.downloadHandler = new DownloadHandlerTexture();
                        yield return flagRequest.SendWebRequest();

                        switch (flagRequest.result)
                        {
                            case UnityWebRequest.Result.Success:
                                var texture = DownloadHandlerTexture.GetContent(flagRequest);
                                countryFlagData.flag = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f);
                                break;
                            default:
                                Debug.LogError("<color=red>: Error [Protocol Error] : Downloading " + countryFlagData.flag_directory + " data : " + flagRequest.error + "</color>");
                                break;
                        }
                    }


                    data.Add(countryFlagData);
                }

                _IsCountryFlagDataLoaded = true;
                Debug.Log("<color=green>Successfull data received:</color><color=white> country flag</color>");

                if (AllDataIsLoaded())
                    _LayoutController.SetupApp();

                break;

            default:
                string[] pages = uri.Split('/');
                int page = pages.Length - 1;
                Debug.LogError("<color=red>" + pages[page] + ": Error [Connection Error]: " + webRequest.error + "</color>");
                _IsCountryFlagDataLoaded = false;
                break;
        }
    }
    // ----- PRICE Data
    private IEnumerator RequestPriceData(string uri, WWWForm form)
    {
        Debug.Log("<color=yellow>Requesting </color><color=white>price</color><color=yellow> data...</color>");

        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
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
                    Debug.Log("<color=green>Successfull data received:</color><color=white> price</color>");
                }
                catch (Exception e)
                {
                    Debug.Log("<color=red>ERROR adding price data! " + e.Message + "</color>");
                }

                if (AllDataIsLoaded())
                    _LayoutController.SetupApp();

                break;
            default:
                string[] pages = uri.Split('/');
                int page = pages.Length - 1;
                Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                _IsPriceDataLoaded = false;
                break;
        }
    }
    private IEnumerator RequestSpecificPriceData(string uri, WWWForm form)
    {
        Debug.Log("<color=yellow>Requesting </color><color=white>specific price</color><color=yellow> data...</color>");

        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
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
                    Debug.Log("<color=green>Successfull data received:</color><color=white> specific price</color>");
                }
                catch (Exception e)
                {
                    Debug.Log("<color=red>ERROR adding specific price data! " + e.Message + "</color>");
                }

                if (AllDataIsLoaded())
                    _LayoutController.SetupApp();
                break;

            default:
                string[] pages = uri.Split('/');
                int page = pages.Length - 1;
                Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                _IsSpecificPriceDataLoaded = false;
                break;
        }
    }
    // ------ Model Data
    private IEnumerator RequestModelsData(string uri, WWWForm form, string category, List<ModelData> data, System.Action<bool> verifier)
    {
        Debug.Log("<color=yellow>Requesting </color><color=white>" + category + "</color><color=yellow> model data...</color>");

        form.AddField("Category", category);
        using UnityWebRequest webRequest = UnityWebRequest.Post(uri, form);

        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                try
                {
                    string[] splitScores = webRequest.downloadHandler.text.Trim().Split('\n');
                    for (int i = 0; i < splitScores.Length; i++)
                    {
                        string[] temp = splitScores[i].Split(',');

                        var modelData = new ModelData();
                        modelData.id = temp[0];
                        modelData.name = temp[1];
                        modelData.category = temp[2];
                        modelData.material_count = int.Parse(temp[3]);
                        modelData.bundle_directory = temp[4];

                        data.Add(modelData);
                    }

                    StartCoroutine(DownloadModelMesh(data, verifier));
                }
                catch (Exception e)
                {
                    Debug.Log("<color=red>Error adding " + category + " data! " + e.Message + "</color>");
                }
                break;
            default:
                string[] pages = uri.Split('/');
                int page = pages.Length - 1;
                Debug.LogError("<color=red>" + pages[page] + ": Error [Protocol Error]: " + webRequest.error + "</color>");
                verifier.Invoke(false);
                break;
        }
    }
    private IEnumerator DownloadModelMesh(List<ModelData> data, System.Action<bool> dataVerifier)
    {
        for (int i = 0; i < data.Count; i++)
        {
            // Download base map
            var directory = data[i].bundle_directory;
            if (directory != "")
            {
                var cwd = new CoroutineWithData(this, DownloadMesh(directory));
                yield return cwd.coroutine;
                var bundle = (AssetBundle)cwd.result;
                data[i].mesh = bundle.LoadAsset<GameObject>(data[i].name).GetComponent<MeshFilter>().sharedMesh;

                //var filePath = Application.persistentDataPath + "/" + data[i].name + ".fbx";
                //if (!File.Exists(filePath))
                //{
                //    yield return StartCoroutine(DownloadMesh(directory, filePath));
                //}
                //Instantiate(new OBJLoader().Load(filePath));

                //var newG = cwd.result;
                //var stream = new MemoryStream(Encoding.UTF8.GetBytes(cwd.result.ToString()));
                //var mesh = new OBJLoader().Load(stream);
            }
        }

        dataVerifier.Invoke(true);
        Debug.Log("<color=green>Successfull data received:</color><color=white> " + data[0].category + "</color>");

        if (AllDataIsLoaded())
            _LayoutController.SetupApp();
    }
    private IEnumerator DownloadMesh(string downloadDirectory)
    {
        using UnityWebRequest webRequest = UnityWebRequestAssetBundle.GetAssetBundle(downloadDirectory);
#if UNITY_WEBGL
        webRequest.SetRequestHeader("Access-Control-Allow-Credentials", "true");
        webRequest.SetRequestHeader("Access-Control-Allow-Headers", "Accept, X-Access-Token, X-Application-Name, X-Request-Sent-Time");
        webRequest.SetRequestHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
        webRequest.SetRequestHeader("Access-Control-Allow-Origin", "*");
#endif
        yield return webRequest.SendWebRequest();

        switch (webRequest.result)
        {
            case UnityWebRequest.Result.Success:
                yield return DownloadHandlerAssetBundle.GetContent(webRequest);
                webRequest.Dispose();
                break;
            default:
                Debug.LogError("<color=red>: Error [Protocol Error] : Downloading " + downloadDirectory + " data : " + webRequest.error + "</color>");
                break;
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

    private bool AllDataIsLoaded()
    {
        // - Colors Data
        if (!_IsColorDataLoaded)
            return false;
        // -- Texture Data
        if (!_IsWoodsTextureDataLoaded)
            return false;
        if (!_IsRocksTextureDataLoaded)
            return false;
        if (!_IsScifiTextureDataLoaded)
            return false;
        if (!_IsFabricsTextureDataLoaded)
            return false;
        if (!_IsOrganicTextureDataLoaded)
            return false;
        if (!_IsMilitaryTextureDataLoaded)
            return false;
        if (!_IsAnimalsTextureDataLoaded)
            return false;
        if (!_IsPreciousTextureDataLoaded)
            return false;
        // --- Countries Data
        if (!_IsCountryFlagDataLoaded)
            return false;
        // ---- Price Data
        if (!_IsPriceDataLoaded)
            return false;
        if (!_IsSpecificPriceDataLoaded)
            return false;
        // ---- Price Data
        if (!_IsBodyDataLoaded)
            return false;
        if (!_IsHeadDataLoaded)
            return false;
        if (!_IsGripDataLoaded)
            return false;
        if (!_IsButtcapDataLoaded)
            return false;

        var loadDuration = (DateTime.Now - _StartingTime).Seconds;
        Debug.Log("<color=green>All data loaded sucessfully in </color><color=white>" + loadDuration + "</color><color=green> seconds!</color>");
        return true;
    }
}

[Serializable]
public struct ColorData
{
    public string id;
    public string hex;
    public Color color;
}
[Serializable]
public class TextureData
{
    public string id;
    public string category;
    public string byoName;
    public string cgaixList;
    public string originalName;
    public Sprite baseMap;
    public Sprite maskMap;
    public Sprite normalMap;
    public string baseMapDirectory;
    public string maskMapDirectory;
    public string normalMapDirectory;
    public Vector2 tiling;
}
[Serializable]
public struct CountryFlagsData
{
    public string name;
    public string flag_directory;
    public Sprite flag;
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
[Serializable]
public class ModelData
{
    public string id;
    public string name;
    public string category;
    public int material_count;
    public string bundle_directory;
    public Mesh mesh;
}