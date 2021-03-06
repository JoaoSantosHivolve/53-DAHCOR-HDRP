using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;
using UnityEngine.UI;

public enum TextPlacement
{
    NotSetYet,
    Inscribe,
    OutsideLogo,
    PhygitallyMade,
    Autograph
}


[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class TextOnObjectManager : MonoBehaviour
{
    // reference via Inspector if possible
    [SerializeField] private TextPlacement _Placement;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private string LayerToUse;

    private const string LOGO_FONT = "Syncopate-Bold SDF";
    private const string INSCRIBE_FONT = "Syncopate-Bold SDF";
    private const string HANDMADE_FONT = "Montserrat-Regular SDF";
    private const string AUTOGRAPH_FONT = "Holimount SDF";

    private void Awake()
    {
        // 0. make the clone of this and make it a child
        var innerObject = new GameObject(name + "_original", typeof(MeshRenderer)).AddComponent<MeshFilter>();
        innerObject.transform.SetParent(transform);
        innerObject.transform.localPosition = Vector3.zero;
        innerObject.transform.localRotation = Quaternion.identity;
        innerObject.transform.localScale = Vector3.one;
        // copy over the mesh
        innerObject.mesh = GetComponent<MeshFilter>().mesh;
        name = name + "_textDecal";

        // 1. Create and configure the RenderTexture
        var renderTexture = new RenderTexture(2048, 2048, 24) { name = name + "_RenderTexture" };

        // 2. Create material
        var textMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultTextMat");
        //var textMaterial = new Material(Shader.Find("HDRP/Lit"));

        // assign the new renderTexture as Albedo
        textMaterial.mainTexture = renderTexture;

        // set RenderMode to Fade
        //textMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        //textMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //textMaterial.
        textMaterial.SetInt("_ZWrite", 10);
        //textMaterial.DisableKeyword("_ALPHATEST_ON");
        //textMaterial.EnableKeyword("_ALPHABLEND_ON");
        //textMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        textMaterial.renderQueue = 3000;

        // Extra
        //textMaterial.SetFloat("_SurfaceType", 1);
        textMaterial.SetFloat("_AlphaCutoffEnable", 1);
        textMaterial.SetFloat("_AlphaCutoff", 1);

        // 3. WE CAN'T CREATE A NEW LAYER AT RUNTIME SO CONFIGURE THEM BEFOREHAND AND USE LayerToUse

        // 4. exclude the Layer in the normal camera
        if (!mainCamera) mainCamera = Camera.main;
        mainCamera.cullingMask &= ~(1 << LayerMask.NameToLayer(LayerToUse));

        // 5. Add new Camera as child of this object
        var camera = new GameObject("TextCamera").AddComponent<Camera>();
        camera.transform.SetParent(transform, false);
        camera.backgroundColor = new Color(0, 0, 0, 0);
        camera.clearFlags = CameraClearFlags.Color;
        camera.cullingMask = 1 << LayerMask.NameToLayer(LayerToUse);
        //camera.depthTextureMode = DepthTextureMode.Depth;

        // make it render to the renderTexture
        camera.targetTexture = renderTexture;
        camera.forceIntoRenderTexture = true;

        // 6. add the UI to your scene as child of the camera
        var Canvas = new GameObject("Canvas", typeof(RectTransform)).AddComponent<Canvas>();
        Canvas.transform.SetParent(camera.transform, false);
        Canvas.gameObject.AddComponent<CanvasScaler>();
        Canvas.renderMode = RenderMode.WorldSpace;
        var canvasRectTransform = Canvas.GetComponent<RectTransform>();
        canvasRectTransform.anchoredPosition3D = new Vector3(0, 0, 3);
        canvasRectTransform.sizeDelta = Vector2.one;

        //var text = new GameObject("Text", typeof(RectTransform)).AddComponent<Text>();
        var text = new GameObject("Text", typeof(RectTransform)).AddComponent<TextMeshProUGUI>();
        text.transform.SetParent(Canvas.transform, false);
        var textRectTransform = text.GetComponent<RectTransform>();

        switch (_Placement)
        {
            case TextPlacement.NotSetYet:
                break;

            case TextPlacement.Inscribe:
                text.font = Resources.Load<TMP_FontAsset>("/Fonts/" + INSCRIBE_FONT);
                text.fontStyle = FontStyles.UpperCase | FontStyles.Bold;
                //text.fontSize = 170;
                text.fontSize = 290;
                text.characterSpacing = -11.3f;
                text.text = "";
                textRectTransform.localScale = Vector3.one * 0.001f;
                textRectTransform.localPosition = new Vector3(0.775f, -1.3f, 0);
                textRectTransform.localScale = new Vector3(0.00125f, 0.001f, 0.001f);
                textRectTransform.sizeDelta = new Vector2(2500, 1);
                break;

            case TextPlacement.OutsideLogo:
                text.fontStyle = FontStyles.UpperCase | FontStyles.Bold;
                text.font = Resources.Load<TMP_FontAsset>("Fonts/" + LOGO_FONT);
                text.text = "DAHCOR";
                text.fontSize = 290;
                text.characterSpacing = -11.3f;
                textRectTransform.localScale = new Vector3(0.00125f, 0.0013f, 0.001f);
                textRectTransform.localPosition = new Vector3(0.5f, 1.37f, 0);
                textRectTransform.sizeDelta = new Vector2(2500, 1);
                break;

            case TextPlacement.PhygitallyMade:
                text.font = Resources.Load<TMP_FontAsset>("Fonts/" + HANDMADE_FONT);
                text.text = "PHYGITALLY MADE IN PORTUGAL";
                text.fontStyle = FontStyles.UpperCase | FontStyles.Bold;
                text.fontSize = 120;
                text.characterSpacing = -8f;
                textRectTransform.localPosition = new Vector3(0, -0.05f, 0);
                textRectTransform.localScale = new Vector3(0.0015f, 0.002f, 0.001f);
                textRectTransform.sizeDelta = new Vector2(3250, 1);
                break;


            case TextPlacement.Autograph:
                text.font = Resources.Load<TMP_FontAsset>("Fonts/" + AUTOGRAPH_FONT);
                text.text = "";
                //text.fontStyle = FontStyles.Subscript;
                text.characterSpacing = -4f;
                text.fontSize = 800;
                textRectTransform.localPosition = new Vector3(0.2f, -0.05f, 0);
                textRectTransform.localScale = new Vector3(0.001f, 0.00075f, 0.001f);
                textRectTransform.sizeDelta = new Vector2(6000, 1);
                break;
            default:
                break;
        }

        //text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.white;
        
        text.isOrthographic = true;
        text.horizontalAlignment = HorizontalAlignmentOptions.Center;
        text.verticalAlignment = VerticalAlignmentOptions.Middle;
        //text.horizontalOverflow = HorizontalWrapMode.Wrap;
        //text.verticalOverflow = VerticalWrapMode.Overflow;

        Canvas.gameObject.layer = LayerMask.NameToLayer(LayerToUse);
        text.gameObject.layer = LayerMask.NameToLayer(LayerToUse);

        // 7. finally assign the material to the child object and hope everything works ;)
        innerObject.GetComponent<MeshRenderer>().material = textMaterial;

        HDShaderUtils.ResetMaterialKeywords(innerObject.GetComponent<MeshRenderer>().material);
    }
}