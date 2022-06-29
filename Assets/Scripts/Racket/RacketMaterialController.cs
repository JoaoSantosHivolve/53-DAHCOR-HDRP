using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public class RacketMaterialController : Singleton<RacketMaterialController>
{
    private Transform _Racket;
    private Transform _Body;
    private Transform _Head;
    private Transform _Bumper;
    private Transform _Strings;
    private Transform _Grip;
    private Transform _Buttcap;

    public override void Awake()
    {
        base.Awake();

        _Racket = transform.GetChild(0).GetChild(0);

        _Body = _Racket.GetChild(3);
        _Head = _Racket.GetChild(0);
        _Bumper = _Racket.GetChild(1);
        _Strings = _Racket.GetChild(6);
        _Grip = _Racket.GetChild(5);
        _Buttcap = _Racket.GetChild(2);
    }

    public void ChangePart(PartToModify part, Color color)
    {
        if (part == PartToModify.None)
            return;

        var renderer = GetPartRenderer(part);
        var material = GetPartMaterial(part);
        renderer.material = new Material(material);
        renderer.material.color = color;
        renderer.material.mainTexture = null;
    }
    public void ChangePart(PartToModify part, Texture2D texture)
    {
        if (part == PartToModify.None)
            return;

        var renderer = GetPartRenderer(part);
        var material = GetPartMaterial(part);
        renderer.material = new Material(material);
        renderer.material.color = Color.white;
        renderer.material.mainTexture = texture;
    }

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
                finishValues = new Vector2(0.0f, 0.0f);
                break;
            case PremadeFinish.Metal:
                finishValues = new Vector2(1.0f, 0.5f);
                break;
            case PremadeFinish.Chrome:
                finishValues = new Vector2(1.0f, 1.0f);
                break;
            default:
                break;
        }

        SetFinish(part, finishValues.x, finishValues.y);
    }
    public void SetFinish(PartToModify part, float metallic, float smoothness)
    {
        if (part == PartToModify.None)
            return;

        var renderer = GetPartRenderer(part);
        renderer.material.SetFloat("_Metallic", metallic);
        renderer.material.SetFloat("_Smoothness", smoothness);
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

    private Material GetPartMaterial(PartToModify part)
    {
        switch (part)
        {
            case PartToModify.None:
                return null;
            case PartToModify.Grip:
                return Resources.Load<Material>("Materials/Racket/GripMat");
            default:
                return Resources.Load<Material>("Materials/Racket/RacketMat");
        }
    }
}