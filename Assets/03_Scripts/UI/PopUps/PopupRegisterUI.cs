using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PopUpRegisterUI : PopUpBaseUI
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

    }


    
}

