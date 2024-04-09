using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using JH.UIManager;
using UnityEngine.UI;
using System.Diagnostics.CodeAnalysis;

/// <summary>
/// Pop�� �׷�ȭ�� Class�� �ޱ� ���� BaseŬ����
/// </summary>
public class PopUpBaseUI : BaseUI<PopUpBaseUI>
{
    #region ������ ������Ƽ
    public override int SortOrder { get => _sortOrder; set { } }
    public override Canvas Canvas { get => _myCanvas; set { }  }
    public override bool IsEnable { get => _myCanvas.enabled; set { } }
    public override Action<PopUpBaseUI> on { get => _on; set { }  }
    public override Action<PopUpBaseUI> off { get =>_off; set { } }
    #endregion

    #region ���κ��� (protected:��Ӹ� ���)

    protected int _sortOrder = 0;//���� ����
    protected Canvas _myCanvas;

    protected Action<PopUpBaseUI> _on;
    protected Action<PopUpBaseUI> _off;
    #endregion


    /// <summary>
    /// PopUIBase���� �����ִ� ���� �߰��Ѵ�
    /// </summary>
    public override void CanvasShow()
    {
        Switching(false);
        UIManager.Instance.uis.Push(this);
        //_on?.Invoke(invoke);
    }

    /// <summary>
    /// PopUIBase���� ����� ���� Pop���� ���ش�.
    /// </summary>
    public override void CavasHide()
    {
        Switching(true);
        UIManager.Instance.uis.Pop();
    }

    /// <summary>
    /// �ϰ�ó���ϱ� ����
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
    /// �ݹ鿡���� �� ������ ������ ���� �Լ�
    /// </summary>
    public virtual void InputAct() 
    {
        print("��");
    }

}
