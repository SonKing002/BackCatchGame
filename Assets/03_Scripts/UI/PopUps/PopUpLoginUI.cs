using JH.UIManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PopUpLoginUI : PopUpBaseUI
{
    public string id { get => _idInputField.text; set { } }
    public string pw { get => _pwInputField.text; set { } }

    [SerializeField]
    private TMP_InputField _idInputField;//아이디 입력
    [SerializeField]
    private TMP_InputField _pwInputField;//비밀번호 입력

   
    /// <summary>
    /// 로그인 버튼
    /// </summary>
    [SerializeField]
    private Button _loginButton;

    /// <summary>
    /// 회원가입 버튼
    /// </summary>
    [SerializeField]
    private Button _RegisterButton;
    [SerializeField]
    private PopUpRegisterUI _registerUI;


    /// <summary>
    /// 버튼 AddListener
    /// </summary>
    public void OnClickButton_Login()
    {
        id = _idInputField.text;
        pw = _pwInputField.text;

        print($"id: {id} : pw: {pw}");
    }
    public void OnClickButton_Register()
    {
        _registerUI.CanvasShow();
    }

    public override void InputAct()
    {
        base.InputAct();

    }
}

