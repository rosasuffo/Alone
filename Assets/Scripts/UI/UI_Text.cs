using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text : UI_CustomComponent
{
    public UI_TextSO textData;
    public Style style;
    private TextMeshProUGUI textMeshProUGUI;

    public override void Setup()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
    public override void Configure()
    {
        /*if(textData != null)
        {
            textMeshProUGUI.color = textData.theme.GetTextColor(style); 
            textMeshProUGUI.font = textData.font;
            textMeshProUGUI.fontSize = textData.size;
        }*/
    }
}
