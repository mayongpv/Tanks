using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : BaseUI<InGameUI>
{
    public override string HierarchyPath {get{return "Canvas/InGameUI";}}

    GameObject ui;
    protected override void OnInit()
    {
        base.OnInit();

        ui = transform.Find("UI").gameObject;
        ui.transform.Find("ExitButton").AddOrGetComponent<Button>()
            .AddListener(this, UIStackManager.Instance.MoveBack);
        ui.SetActive(false);
    }

    protected override void OnShow()
    {
        base.OnShow();

        ui.SetActive(true);
    }

    public override void Close()
    {
        base.Close();

        //// 이전 UI(OutGameUI)를 보여주자.
        ShowPreviousMenu(this); // <- 마지막에 열었던게 자기 자신이므로 자기자신을 먼저 뺀다음 실행하자. 
    }
}
