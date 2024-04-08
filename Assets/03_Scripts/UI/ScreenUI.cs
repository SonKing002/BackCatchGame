using System;
using System.Collections.Generic;
using UnityEngine;
public class ScreenUI : SingletonOfT<ScreenUI>, IScreenUI 
{
    private Canvas _myCanvas;
    public int SortOrder  { get => sortOrder; }

    Canvas IUI.Canvas
    {
        get
        {
            return _myCanvas = GetComponent<Canvas>();
        }
    }

    bool IUI.isEnable { get => _myCanvas.enabled; }

    int IUI.SortOrder { get => _myCanvas.sortingOrder; }

    private int sortOrder = 0;


    public override void Awake()
    {
        base.Awake();
    }
}

