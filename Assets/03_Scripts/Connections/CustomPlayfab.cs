using PlayFab.ClientModels;
using PlayFab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class CustomPlayfab : SingletonOfT<CustomPlayfab>
{
    private void Awake()
    {
        Init();
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        //타이틀 아이디 비어있다면
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
            PlayFabSettings.staticSettings.TitleId = "E5507";
            Manager.Instance.logText.text = "에러발생, playfabs TitleID 세팅이 필요합니다";
        }
    }

    /// <summary>
    /// 플래이펩 로그인 시도 함수
    /// </summary>
    /// <param name="id">입력된 아이디</param>
    /// <param name="pw">입력된 비밀번호</param>
    public void TryLogin(string id, string pw)
    {
        var request = new LoginWithEmailAddressRequest { Email = id , Password = pw};//입력값 기준으로
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);//로그인 시도
    }

    /// <summary>
    /// 플래이펩 회원가입 시도 함수
    /// </summary>
    /// <param name="id">입력된 아이디</param>
    /// <param name="pw">입력된 비밀번호</param>
    public void TryRegister(string id, string pw)
    { 
        var request = new RegisterPlayFabUserRequest { Email = id , Password = pw };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
    }

    /// <summary>
    /// 로그인 성공시 콜백으로 실행되는 함수
    /// </summary>
    /// <param name="result"></param>
    private void OnLoginSuccess(LoginResult result) 
    {
        Manager.Instance.logText.text = "로그인 성공"; 
    }

    /// <summary>
    /// 로그인 실패시 콜백으로 실행되는 함수
    /// </summary>
    /// <param name="error"></param>
    private void OnLoginFailure(PlayFabError error)
    {
        Manager.Instance.logText.text = $"로그인 실패";
    }

    /// <summary>
    /// 회원가입 성공시 콜백으로 실행되는 함수
    /// </summary>
    /// <param name="result"></param>
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Manager.Instance.logText.text = "회원가입 성공";

    }

    /// <summary>
    /// 회원가입 실패시 콜백으로 실행되는 함수
    /// </summary>
    /// <param name="error"></param>
    private void OnRegisterFailure(PlayFabError error)
    {
        Manager.Instance.logText.text = "회원가입 실패";
    }
}

