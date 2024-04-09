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

    private void Awake()
    {
        _myCanvas = GetComponent<Canvas>();
    }

    /// <summary>
    /// 로그인 버튼
    /// </summary>
    [SerializeField]
    private Button _loginButton;   

    [SerializeField]
    private Button _RegisterButton;//회원가입 버튼

    public void OnClickButton()
    {
        id = _idInputField.text;
        pw = _pwInputField.text;

        print($"id: {id} : pw: {pw}");
    }

    public override void InputAct()
    {
        base.InputAct();

    }
            

}

