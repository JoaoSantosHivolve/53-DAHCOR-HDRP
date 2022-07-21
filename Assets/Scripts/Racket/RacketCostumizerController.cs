using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PartToModify
{
    None,
    Body,
    Head,
    Bumper,
    Strings,
    Grip,
    Buttcap
}

public enum PremadeFinish
{
    Glossy,
    Matte,
    Metal,
    Chrome
}

public class RacketCostumizerController : Singleton<RacketCostumizerController>
{
    private Transform _Racket;
    private Transform _Body;
    private Transform _Head;
    private Transform _Bumper;
    private Transform _Strings;
    private Transform _Grip;
    private Transform _Buttcap;
    private Transform _TextInscribe;

    private Material _DefaultFrameMaterial;
    private Material _DefaultHeadMaterial;
    private Material _DefaultGripMaterial;
    private Material _DefaultButtCapMaterial;
    private Material _DefaultTapeMaterial;
    private Material _DefaultBumperMaterial;

    public override void Awake()
    {
        base.Awake();

        _Racket = transform.GetChild(0).GetChild(0);

        _Body = _Racket.GetChild(1);
        _Head = _Racket.GetChild(2);
        _Bumper = _Racket.GetChild(0);
        _Strings = _Racket.GetChild(3);
        _Grip = _Racket.GetChild(4);
        _Buttcap = _Racket.GetChild(5);
        _TextInscribe = _Racket.GetChild(6).GetChild(0);

        _DefaultFrameMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultFrameMat");
        _DefaultHeadMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultHeadMat");
        _DefaultGripMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultGripMat");
        _DefaultButtCapMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultButtCapMat");
        _DefaultTapeMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultTapeMat");
        _DefaultBumperMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultBumperMat");
    }

    // ----- COLOR
    public void ChangePart(PartToModify part, Color color, int index)
    {
        if (part == PartToModify.None)
            return;

        var renderer = GetPartRenderer(part);
        var material = GetDefaultMaterial(part);
        var exampleMat = renderer.materials[index];
        Material[] mats = renderer.materials;
        mats[index] = new Material(material);
        mats[index].SetFloat("_Metallic", exampleMat.GetFloat("_Metallic"));
        mats[index].SetFloat("_Smoothness", exampleMat.GetFloat("_Smoothness"));
        mats[index].color = color;
        renderer.materials = mats;

        ////renderer.materials[index] = new Material(Shader.Find("HDRP/Lit"));
        //renderer.materials[index].color = color;
        ////renderer.materials[index].mainTexture = null;
        //renderer.materials[index].SetTexture("_MaskMap", null);
        //renderer.materials[index].SetTexture("_NormalMap", null);
    }
    // ----- TEXTURE
    public void ChangePart(PartToModify part, TextureData textureData, int index)
    {
        if (part == PartToModify.None)
            return;

        var renderer = GetPartRenderer(part);
        Material[] mats = renderer.materials;
#if PLATFORM_STANDALONE_WIN
        mats[index] = new Material(Shader.Find("HDRP/Lit"));
        mats[index].SetTexture("_MaskMap", textureData.maskMap.texture);
#else
        mats[index] = new Material(Shader.Find("Universal Render Pipeline/Lit"));
#endif
        mats[index].color = Color.white;
        mats[index].mainTexture = textureData.baseMap.texture;
        mats[index].SetTexture("_NormalMap", textureData.normalMap.texture);
        mats[index].mainTextureScale = textureData.tiling;
        renderer.materials = mats;
        //renderer.materials[index].SetTextureScale("_MainTex", textureData.tiling);
        //renderer.materials[index].SetTextureScale("_MaskMap", textureData.tiling);
        //renderer.materials[index].SetTextureScale("_NormalMap", textureData.tiling);
    }
    public void SetDefaultMat(PartToModify part)
    {
        var renderer = GetPartRenderer(part);
        renderer.material = new Material(GetDefaultMaterial(part));
    }
    // ----- MODEL
    public void ChangeObject(PartToModify part, ModelData data)
    {
        // Change mesh
        var meshFilter = GetPartMeshFilter(part);
        meshFilter.mesh = data.mesh;

        // Set default materials
        var defaultMat = GetDefaultMaterial(part);
        var newMats = new Material[data.material_count];
        if(part != PartToModify.Grip && part != PartToModify.Buttcap)
        {
            for (int i = 0; i < newMats.Length; i++)
            {
                newMats[i] = defaultMat;
            }

            var renderer = GetPartRenderer(part);
            renderer.materials = newMats;
        }
    }
    // ----- FINISH
    public void SetFinish(PartToModify part, PremadeFinish finish)
    {
        if (part == PartToModify.None)
            return;

        var finishValues = new Vector2();

        switch (finish)
        {
            case PremadeFinish.Glossy:
                finishValues = new Vector2(0.5f, 0.5f);
                break;
            case PremadeFinish.Matte:
                finishValues = new Vector2(0.0f, 0.5f);
                break;
            case PremadeFinish.Metal:
                finishValues = new Vector2(1.0f, 0.75f);
                break;
            case PremadeFinish.Chrome:
                finishValues = new Vector2(1.0f, 1.0f);
                break;
            default:
                break;
        }

        SetFinish(part, finishValues.x, finishValues.y, finish != PremadeFinish.Matte);
    }
    public void SetFinish(PartToModify part, float metallic, float smoothness, bool applyCoat)
    {
        if (part == PartToModify.None)
            return;

        var renderer = GetPartRenderer(part);
        for (int i = 0; i < renderer.materials.Length; i++)
        {
            renderer.materials[i].SetFloat("_Metallic", metallic);
            renderer.materials[i].SetFloat("_Smoothness", smoothness);
            renderer.materials[i].SetFloat("_CoatMask", applyCoat ? 1.0f : 0.0f);
        }
    }
    // ----- TEXT
    public void ChangeText(TextPlacement placement, string answer)
    {
        var textToChange = GetText(placement);
        textToChange.text = answer;
    }

    private MeshRenderer GetPartRenderer(PartToModify part)
    {
        switch (part)
        {
            case PartToModify.None:
                return null;
            case PartToModify.Body:
                return _Body.GetComponent<MeshRenderer>();
            case PartToModify.Head:
                return _Head.GetComponent<MeshRenderer>();
            case PartToModify.Bumper:
                return _Bumper.GetComponent<MeshRenderer>();
            case PartToModify.Strings:
                return _Strings.GetComponent<MeshRenderer>();
            case PartToModify.Grip:
                return _Grip.GetComponent<MeshRenderer>();
            case PartToModify.Buttcap:
                return _Buttcap.GetComponent<MeshRenderer>();
            default:
                return null;
        }
    }
    private MeshFilter GetPartMeshFilter(PartToModify part)
    {
        switch (part)
        {
            case PartToModify.None:
                return null;
            case PartToModify.Body:
                return _Body.GetComponent<MeshFilter>();
            case PartToModify.Head:
                return _Head.GetComponent<MeshFilter>();
            case PartToModify.Bumper:
                return _Bumper.GetComponent<MeshFilter>();
            case PartToModify.Strings:
                return _Strings.GetComponent<MeshFilter>();
            case PartToModify.Grip:
                return _Grip.GetComponent<MeshFilter>();
            case PartToModify.Buttcap:
                return _Buttcap.GetComponent<MeshFilter>();
            default:
                return null;
        }
    }
    private Material GetDefaultMaterial(PartToModify part)
    {
        switch (part)
        {
            case PartToModify.None:
                return null;
            case PartToModify.Body:
                return _DefaultFrameMaterial;
            case PartToModify.Head:
                return _DefaultHeadMaterial;
            case PartToModify.Buttcap:
                return _DefaultButtCapMaterial;
            case PartToModify.Grip:
                return _DefaultGripMaterial;
            case PartToModify.Bumper:
                return _DefaultBumperMaterial;
            default:
                return Resources.Load<Material>("Materials/Racket/RacketMat");
        }
    }
    private Text GetText(TextPlacement placement)
    {
        Transform part = null;

        switch (placement)
        {
            case TextPlacement.NotSetYet:
                return null;
            case TextPlacement.Inscribe:
                part = _TextInscribe;
                break;
            default:
                Debug.Log("No text component found");
                return null;
        }

        return part.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
    }
}