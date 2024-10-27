using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/TextSO", fileName = "TextSO")]
public class UI_TextSO : ScriptableObject
{
    public UI_ThemeSO theme;

    public TMP_FontAsset font;
    public float size;
}
