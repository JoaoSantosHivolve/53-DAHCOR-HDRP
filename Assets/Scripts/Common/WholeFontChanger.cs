using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WholeFontChanger : MonoBehaviour
{
    public TMP_FontAsset newFont;

    [ContextMenu("Change Font")]
    void ChangeFontSize()
    {
        var allTextComponents = GameObject.FindObjectsOfType<TextMeshProUGUI>();
        foreach (var textComponent in allTextComponents)
        {
            textComponent.font = newFont;
        }
    }
    [ContextMenu("Change Words spacing")]
    void ChangeFontSpacing()
    {
        var allTextComponents = GameObject.FindObjectsOfType<TextMeshProUGUI>();
        foreach (var textComponent in allTextComponents)
        {
            textComponent.characterSpacing = -6f;
        }
    }
}