using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CustomUI/ContainerSO", fileName = "ContainerSO")] 
public class UI_ContainerSO : ScriptableObject
{
    public UI_ThemeSO theme;
    public RectOffset padding;
    public float spacing;
}
