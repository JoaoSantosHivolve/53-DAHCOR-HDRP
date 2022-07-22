using System;
using System.Collections.Generic;
using TMPro;
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
    Buttcap,
    TextsAndLogos
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

   
    private Transform _LogosHolder;
    private Transform _TextInscribe;
    private Transform _TextOutsideLogo;
    private Transform _TextPhygitallyMade;
    private Transform _TextAutograph;

    private Material _DefaultFrameMaterial;
    private Material _DefaultHeadMaterial;
    private Material _DefaultGripMaterial;
    private Material _DefaultButtCapMaterial;
    private Material _DefaultTapeMaterial;
    private Material _DefaultBumperMaterial;
    private Material _DefaultStringMaterial;
    private Material _DefaultLogoMaterial;

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
        _LogosHolder = _Racket.GetChild(6).GetChild(0);
        _TextInscribe = _Racket.GetChild(6).GetChild(1);
        _TextOutsideLogo = _Racket.GetChild(6).GetChild(2);
        _TextPhygitallyMade = _Racket.GetChild(6).GetChild(3);
        _TextAutograph = _Racket.GetChild(6).GetChild(4);

        _DefaultFrameMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultFrameMat");
        _DefaultHeadMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultHeadMat");
        _DefaultGripMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultGripMat");
        _DefaultButtCapMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultButtCapMat");
        _DefaultTapeMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultTapeMat");
        _DefaultBumperMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultBumperMat");
        _DefaultStringMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultStringMat");
        _DefaultLogoMaterial = Resources.Load<Material>("Materials/Racket/Default/DefaultLogoMat");
    }

    // ----- COLOR
    public void ChangePart(PartToModify part, Color color, int index)
    {
        if (part == PartToModify.None)
            return;
        if(part != PartToModify.TextsAndLogos)
        {
            var renderer = GetPartRenderer(part);
            var material = GetDefaultMaterial(part);
            var exampleMat = renderer.materials[index];
            Material[] mats = renderer.materials;
            mats[index] = new Material(material);
            mats[index].SetFloat("_Metallic", exampleMat.GetFloat("_Metallic"));
            mats[index].SetFloat("_Smoothness", exampleMat.GetFloat("_Smoothness"));
            mats[index].color = color;
            renderer.materials = mats;
        }
        // CHANGE TEXTS AND LOGOS
        else
        {
            var texts = new List<TextMeshProUGUI>();
            texts.Add(GetText(TextPlacement.Inscribe));
            texts.Add(GetText(TextPlacement.OutsideLogo));
            texts.Add(GetText(TextPlacement.PhygitallyMade));
            texts.Add(GetText(TextPlacement.Autograph));

            foreach (var item in texts)
            {
                item.color = color;
            }

            for (int i = 0; i < _LogosHolder.childCount; i++)
            {
                if(i != 3)
                    _LogosHolder.GetChild(i).GetComponent<MeshRenderer>().material.color = color;
            }
        }
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

        if(part != PartToModify.TextsAndLogos)
        {
            var renderer = GetPartRenderer(part);
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                renderer.materials[i].SetFloat("_Metallic", metallic);
                renderer.materials[i].SetFloat("_Smoothness", smoothness);
                renderer.materials[i].SetFloat("_CoatMask", applyCoat ? 1.0f : 0.0f);
            }
        }
        else
        {
            var textMaterials = new List<MeshRenderer>();
            textMaterials.Add(_TextInscribe.GetChild(0).GetComponent<MeshRenderer>());
            textMaterials.Add(_TextOutsideLogo.GetChild(0).GetComponent<MeshRenderer>());
            textMaterials.Add(_TextPhygitallyMade.GetChild(0).GetComponent<MeshRenderer>());
            textMaterials.Add(_TextAutograph.GetChild(0).GetComponent<MeshRenderer>());

            for (int i = 0; i < _LogosHolder.childCount; i++)
            {
                textMaterials.Add(_LogosHolder.GetChild(i).GetComponent<MeshRenderer>());
            } 

            foreach (var item in textMaterials)
            {
                item.material.SetFloat("_Metallic", metallic);
                item.material.SetFloat("_Smoothness", smoothness);
                item.material.SetFloat("_CoatMask", applyCoat ? 1.0f : 0.0f);
            }
        }
    }
    // ----- COUNTRY FLAGS
    public void ChangeFlag(CountryFlagsData item)
    {
        if (item.flag != null)
            _LogosHolder.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = item.flag.texture;
        else
            _LogosHolder.GetChild(3).GetComponent<MeshRenderer>().material.mainTexture = null;
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
            case PartToModify.Strings:
                return _DefaultStringMaterial;
            case PartToModify.TextsAndLogos:
                return _DefaultLogoMaterial;
            default:
                return Resources.Load<Material>("Materials/Racket/RacketMat");
        }
    }
    private TextMeshProUGUI GetText(TextPlacement placement)
    {
        Transform part = null;

        switch (placement)
        {
            case TextPlacement.NotSetYet:
                return null;
            case TextPlacement.Inscribe:
                part = _TextInscribe;
                break;
            case TextPlacement.OutsideLogo:
                part = _TextOutsideLogo;
                break;
            case TextPlacement.PhygitallyMade:
                part = _TextPhygitallyMade;
                break;
            case TextPlacement.Autograph:
                part = _TextAutograph;
                break;
            default:
                Debug.Log("No text component found");
                return null;
        }

        return part.GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
    }
}