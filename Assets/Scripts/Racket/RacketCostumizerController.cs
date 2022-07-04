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

public class RacketCostumizerController : Singleton<RacketCostumizerController>
{
    [SerializeField]private Transform _Racket;
    [SerializeField]private Transform _Body;
    [SerializeField]private Transform _Head;
    [SerializeField]private Transform _Bumper;
    [SerializeField]private Transform _Strings;
    [SerializeField]private Transform _Grip;
    [SerializeField]private Transform _Buttcap;

    public override void Awake()
    {
        base.Awake();

        _Racket = transform.GetChild(0).GetChild(0);

        _Body = _Racket.GetChild(1);
        _Head = _Racket.GetChild(1);
        _Bumper = _Racket.GetChild(0);
        _Strings = _Racket.GetChild(2);
        _Grip = _Racket.GetChild(3);
        _Buttcap = _Grip.GetChild(0);
    }

    // ----- COLOR
    public void ChangePart(PartToModify part, Color color)
    {
        if (part == PartToModify.None)
            return;

        var renderer = GetPartRenderer(part);
        renderer.material.color = color;
        renderer.material.mainTexture = null;
    }
    // ----- TEXTURE
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
    public void ChangePart(PartToModify part, Material material)
    {
        var renderer = GetPartRenderer(part);
        renderer.material = new Material(material);
    }
    // ----- MODEL
    public void ChangeObject(PartToModify part, GameObject obj)
    {
        GameObject currentObject;

        switch (part)
        {
            case PartToModify.Body:
                currentObject = _Head.gameObject; // NEED CHANGING
                break;
            case PartToModify.Head:
                currentObject = _Head.gameObject; // NEED CHANGING
                break;
            case PartToModify.Buttcap:
                currentObject = _Grip.gameObject;
                break;
            default:
                Debug.Log("Not a valid choice");
                return;
        }

        // Set new Object
        var newObject = Instantiate(obj, _Racket);
        // UPDATE MATERIAL
        ChangePart(part, new Material(currentObject.GetComponent<MeshRenderer>().material));

        // Update transforms
        switch (part)
        {
            case PartToModify.Body:
                _Body = newObject.transform;
                break;
            case PartToModify.Head:
                _Head = newObject.transform;
                break;
            case PartToModify.Grip:
                _Grip = newObject.transform;
                _Buttcap = _Grip.GetChild(0);
                break;
            default:
                break;
        }

        // Delete old object
        Destroy(currentObject);
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
        renderer.material.SetFloat("_Metallic", metallic);
        renderer.material.SetFloat("_Smoothness", smoothness);
        renderer.material.SetFloat("_CoatMask", applyCoat ? 1.0f : 0.0f);
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