using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartToModify
{
    None,
    Body,
    Head,
    Bumper
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

    public override void Awake()
    {
        base.Awake();

        _Racket = transform.GetChild(0).GetChild(0);

        _Body = _Racket.GetChild(3);
        _Head = _Racket.GetChild(0);
        _Bumper = _Racket.GetChild(1);
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
            case PartToModify.Bumper:
                return Resources.Load<Material>("Materials/Racket/ButtcapMat");
            case PartToModify.Body:
                return Resources.Load<Material>("Materials/Racket/ButtcapMat");
            case PartToModify.Head:
                return Resources.Load<Material>("Materials/Racket/ButtcapMat");
            default:
                return null;
        }
    }
}