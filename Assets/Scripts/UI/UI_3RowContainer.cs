using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_3RowContainer : UI_CustomComponent
{
    public UI_ContainerSO viewData;

    public GameObject containerTop;
    public GameObject containerCenter;
    public GameObject containerBottom;

    private Image imageTop;
    private Image imageCenter;
    private Image imageBotton;

    private VerticalLayoutGroup verticalLayoutGroup;

    public override void Setup() 
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
        imageTop = containerTop.GetComponent<Image>();
        imageCenter = containerCenter.GetComponent<Image>();
        imageBotton = containerBottom.GetComponent<Image>();
    }
    public override void Configure()
    {
        verticalLayoutGroup.padding = viewData.padding;
        verticalLayoutGroup.spacing = viewData.spacing;

        imageTop.color = viewData.theme.primary_bg;
        imageCenter.color = viewData.theme.secondary_bg;
        imageBotton.color = viewData.theme.tertiary_bg;
    }
}
