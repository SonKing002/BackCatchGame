using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 회원가입
/// </summary>
public class PopUpRegisterUI : PopUpInputFieldBaseUI
{
    public static PopUpRegisterUI Instance
    {
        get => _instance;
    }
    private static PopUpRegisterUI _instance;

    private void Init()
    {
        //rect = (RectTransform)transform.GetChild(1).transform;

        //tempTarget = new Vector3(rect.position.x, 0, rect.position.y);//목표값
        //y = rect.position.y; //초기값 캐싱
    }

    public override void CanvasShow()
    {
        base.CanvasShow();
        //Init();
    }


    /// <summary>
    /// 등록버튼 클릭시 실행 함수
    /// </summary>
    public override void OnClickButton_Link()
    {
        base.OnClickButton_Link();//공통사항 아이디 비밀번호 검사
        InputAct();
    }

    /// <summary>
    /// 회원가입 조건 
    /// </summary>
    public override void InputAct()
    {
        //최소 조건 부합하면,
        if (_isSuccess == true)
        {
            
            PopUpInformWindowsUI.Instance.Success_Inform("아이디 생성 완료", $"생성된 아이디 :{_idInputField.text} \n 이제, 로그인을 통해 게임을 접속할 수 있습니다");
            Manager.Instance.logText.text = "회원가입 완료";

            //성공했을 때,
            CavasHide();
            RegisterReset();
        }
    }

    /// <summary>
    /// 회원가입 리셋
    /// </summary>
    public void RegisterReset()
    {
        _isSuccess = false;
        _idInputField.text = null;
        _pwInputField.text = null;
    }
}

