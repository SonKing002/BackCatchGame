using System;
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// 닉네임 설정 클래스
/// </summary>
public class PopUpNicknameUI: PopUpInputField1BaseUI
{
    protected override void Awake()
    {
        base.Awake();

        SetNickName();
    }

    /// <summary>
    /// 닉네임이 비어있다면, 설정하도록 설정한다
    /// </summary>
    public void SetNickName()
    {

        Utils.LogRed(DependencySource.Instance.nickname);

        if (Utils.IsStringValid(DependencySource.Instance.nickname) == false)
        {
            CanvasShow();
        }

    }

    public void OnClickButton_Confirm()
    {
        DependencySource.Instance.nickname = _inputField.text;

        // Todo : 닉네임 중복체크해야한다
        CustomPlayfab.Instance.UpdateNickname();
        CavasHide();
    }
}
