using System;
using System.Collections.Generic;
using UnityEngine;

public interface IUI
{
    public Canvas Canvas { get; }//캔버스 단위
    public bool isEnable { get; }//활성화 여부
    public int SortOrder { get; }

}

