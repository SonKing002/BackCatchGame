using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PopupRegisterUI : PopUpBaseUI
{
    public string id { get => _idInputField.text; set { } }
    public string pw { get => _pwInputField.text; set { } }

    [SerializeField]
    private TMP_InputField _idInputField;//아이디 입력
    [SerializeField]
    private TMP_InputField _pwInputField;//비밀번호 입력

    /// <summary>
    /// 회원가입 버튼
    /// </summary>
    [SerializeField]
    private Button _RegisterButton;

    private bool _isConditionSatisfied;

    /// <summary>
    /// AddListener 등록 클릭시 실행 함수
    /// </summary>
    public void OnClickButton_Register()
    {
        CheckConditionToRegist(_idInputField.text, _pwInputField.text);
    }

    public void CheckConditionToRegist(string idText,string pwText)
    {
        if (string.IsNullOrEmpty(idText) || string.IsNullOrWhiteSpace(idText))
        {
            print(PopUpInformWindowsUI.Instance.obj.name);
            PopUpInformWindowsUI.Instance.CanvasShow();
            PopUpInformWindowsUI.Instance.CheckUp(false,"입력이 빈칸 또는 공란입니다");
            return;
        }
        if (string.IsNullOrEmpty(pwText) || string.IsNullOrWhiteSpace(pwText))
        {
            print(PopUpInformWindowsUI.Instance.obj.name);
            PopUpInformWindowsUI.Instance.CanvasShow();
            PopUpInformWindowsUI.Instance.CheckUp(false, "입력이 빈칸 또는 공란입니다");
            return;
        }
    }

    
}

