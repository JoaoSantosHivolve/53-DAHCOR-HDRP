using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PartToModify
{
    None,
    Bumper,
    Buttcap,

}

public class RacketMaterialController : Singleton<RacketMaterialController>
{
    private Transform _Racket;
    private Transform _Bumper;
    private Transform _Buttcap;

    public override void Awake()
    {
        base.Awake();

        _Racket = transform.GetChild(0).GetChild(0);

        _Bumper = _Racket.GetChild(1);
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

    private MeshRenderer GetPartRenderer(PartToModify part)
    {
        switch (part)
        {
            case PartToModify.None:
                return null;
            case PartToModify.Bumper:
                return _Bumper.GetComponent<MeshRenderer>();
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
            case PartToModify.Bumper:
                return Resources.Load<Material>("Materials/Racket/ButtcapMat");
            case PartToModify.Buttcap:
                return Resources.Load<Material>("Materials/Racket/ButtcapMat");
            default:
                return null;
        }
    }
}