﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using JH.UIManager;
using UnityEngine.UI;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Pop을 그룹화할 Class로 받기 위한 Base클래스
/// </summary>
public class PopUpBaseUI : BaseUI<PopUpBaseUI>
{
    #region  프로퍼티
    public override int SortOrder { get => _sortOrder; set { } }
    public override Canvas Canvas { get => _myCanvas; set { }  }
    public override bool IsEnable { get => _myCanvas.enabled; set { } }
    public override Action<PopUpBaseUI> on { get => _on; set { }  }
    public override Action<PopUpBaseUI> off { get =>_off; set { } }
    #endregion

    #region 내부변수 (protected:상속만 사용)

    protected int _sortOrder = 0;//소팅 순서
    protected Canvas _myCanvas;

    protected Action<PopUpBaseUI> _on;
    protected Action<PopUpBaseUI> _off;
    #endregion

    protected virtual void Awake()
    {
        _myCanvas = this.GetComponent<Canvas>();
    }

    /// <summary>
    /// PopUIBase에서 보여주는 것은 추가한다 == 실행 : Push으로 추가
    /// </summary>
    public override void CanvasShow()
    {
        Switching(false);//false일 때, 이 함수로 true 스위치
        UIManager.Instance.uis.Push(this);//추가
        UIManager.Instance.uiList.Add(UIManager.Instance.uis.ToArray()[UIManager.Instance.uis.Count - 1]);//인스펙터 확인용
        UIManager.Instance.ReSortingOrder(this._myCanvas);//순서 재정렬
        //print(this.gameObject.name);
        //Debug.Log($"+1 : {UIManager.Instance.uis.Count}");
    }

    /// <summary>
    /// PopUIBase에서 숨기는 것은 == 팝업 창 종료 : Pop으로 빼준다.
    /// </summary>
    public override void CavasHide()
    {
        Switching(true);//true일 때, 이 함수로 false 스위치

        //빼내기 조건 처리
        if (UIManager.Instance.uis.Count <= 0)
            return;

        UIManager.Instance.uis.Pop();//제거
        UIManager.Instance.uiList.RemoveAt(UIManager.Instance.uis.Count);//인스펙터 확인용
        UIManager.Instance.ReSortingOrder(this._myCanvas);//순서 재정렬
    }

    /// <summary>
    /// 일괄처리하기 위함
    /// </summary>
    /// <param name="isTrue"></param>
    public override void Switching(bool isTrue)
    {
        switch (isTrue)
        {
            case true:
                _myCanvas.enabled = false;
                break;
            case false:
                _myCanvas.enabled = true;
                break;
        }
    }

    /// <summary>
    /// 콜백에서의 각 실행할 내용을 담을 함수
    /// </summary>
    public virtual void InputAct() 
    {

    }

}
