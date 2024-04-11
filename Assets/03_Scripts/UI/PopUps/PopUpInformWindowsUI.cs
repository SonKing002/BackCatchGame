﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 알림용 UI 클래스
/// </summary>
public class PopUpInformWindowsUI : PopUpBaseUI
{

    #region 싱글톤 (따로 제작)
    public static PopUpInformWindowsUI Instance
    {
        get
        {
            return _instance;
        }
    }
    private static PopUpInformWindowsUI _instance;
    public GameObject obj;

    protected override void Awake()
    {
        base.Awake();
        _myCanvas.enabled = false;

        if (_instance != null)
        {
            if (_instance != this)
            {
                Destroy(_instance);
            }
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);

        obj = gameObject;
    }
    #endregion

    public Button confirmButton { get => _confirmButton; }
    

    [SerializeField]
    private bool isTrue;


    #region 색상 목록 직렬화 노출용 리스트
    [Header("경고창 색상")]
    [SerializeField]
    private Color _warningShieldColor = new Color(0, 15, 38, 203);
    [SerializeField]
    private Color _warningBackGroundColor = new Color(255, 61, 61, 255);
    [SerializeField]
    private Color _warningTitleTextColor = new Color(240, 255, 131, 255);
    [SerializeField]
    private Color _warningBodyColor = new Color(255, 255, 255, 255);
    [SerializeField]
    private Color _warningShadowColor = new Color(233, 198, 61, 255);
    [SerializeField]
    private Color _warningButtonColor = new Color(255, 181, 165, 255);

    [Header("확인창 색상")]
    [SerializeField]
    private Color _informShieldColor = new Color(0, 15, 38, 203);
    [SerializeField]
    private Color _informBackGroundColor = new Color(177, 215, 255, 255);
    [SerializeField]
    private Color _informTitleTextColor = new Color(240, 255, 131, 255);
    [SerializeField]
    private Color _informBodyColor = new Color(255, 255, 255, 255);
    [SerializeField]
    private Color _informShadowColor = new Color(136, 155, 255, 255);
    [SerializeField]
    private Color _informButtonColor = new Color(178, 255, 165, 255);

    [Header("연결해야할 오브젝트들")]
    [SerializeField]
    private Image _backShield;//배경 안눌리기 하기 위함
    [SerializeField]
    private Image _backGround;//백이미지
    [SerializeField]
    private TMP_Text _titleText;//제목
    [SerializeField]
    private TMP_Text _bodyText;//내용
    [SerializeField]
    private Shadow _shodowImage;//백이미지 그림자
    [SerializeField]
    private Image _buttonImage;//버튼의 이미지
    [SerializeField]
    private Button _confirmButton;//버튼 (특정상황: Action리스너 필요)
    #endregion

    /*
    public void Update()
    {
        //CheckUp(isTrue, "d");//테스트 확인
    }
    */

    public override void InputAct()
    {
        //미정
    }

    /// <summary>
    /// 사용하기 전 미리 리셋정리 함수
    /// </summary>
    public void MessageReset()
    {
        _titleText.text = "";
        _bodyText.text = "";
    }

    /// <summary>
    /// 안내창 컬러 결정
    /// </summary>
    /// <param name="isFine">true일 때 알림종류 분기</param>
    /// <param name="bodyText">알릴 메세지</param>
    public void MessageType(bool isFine)
    {
        switch (isFine)
        {
            case true://알림용
                InformingColor();
                MessageTitle("알림 :");
                break;
            case false://경고용
                WarningColor();
                
                MessageTitle("경고 :");
                break;
        }
    }

    /// <summary>
    /// 여러번 중복되는 코드를 줄이고자 따로 작성한 함수 : 제목
    /// </summary>
    /// <param name="titleText">제목</param>
    public void MessageTitle(string titleText)
    {
        _titleText.text += titleText;
    }

    /// <summary>
    /// 여러번 중복되는 코드를 줄이고자 따로 작성한 함수 : 내용
    /// </summary>
    /// <param name="bodyText">내용</param>
    public void MessageBody(string bodyText)
    {
        _bodyText.text += bodyText;
    }

    /// <summary>
    /// 경고용 임의 색상 할당
    /// </summary>
    private void WarningColor()
    {
        _backShield.color = _warningShieldColor;
        _backGround.color = _warningBackGroundColor;
        _titleText.color = _warningTitleTextColor;
        _bodyText.color = _warningBodyColor;
        _shodowImage.effectColor = _warningShadowColor;
        _buttonImage.color = _warningButtonColor;
    }
    /// <summary>
    /// 알림용 임의 색상 할당
    /// </summary>
    private void InformingColor()
    {
        _backShield.color = _informShieldColor;
        _backGround.color = _informBackGroundColor;
        _titleText.color = _informTitleTextColor;
        _bodyText.color = _informBodyColor;
        _shodowImage.effectColor = _informShadowColor;
        _buttonImage.color = _informButtonColor;
    }

    /// <summary>
    /// 버튼클릭시 동작
    /// </summary>
    public void OnClickButton_Confirm()
    {
        CavasHide();
    }

    #region 상황별 에러들을 정리한 함수 탬플릿

    /// <summary>
    /// 아이디 공란
    /// </summary>
    public void ERROR_EmptyInputID()
    {
        CanvasShow();
        MessageReset();
        MessageType(false);
        MessageTitle("아이디 입력 오류");
        MessageBody("아이디 입력이 빈칸 또는 공란입니다");
    }

    /// <summary>
    /// 비밀번호 공란
    /// </summary>
    public void ERROR_EmptyInputPW()
    {
        CanvasShow();
        MessageReset();
        MessageType(false);
        MessageTitle("비밀번호 입력 오류");
        MessageBody("비밀번호 입력이 빈칸 또는 공란입니다");
    }

    /// <summary>
    /// 잘못된 아이디 입력형식 
    /// </summary>
    public void ERROR_WrongFormID()
    {
        CanvasShow(); 
        MessageReset();
        MessageType(false);
        MessageTitle("아이디 입력 오류");
        MessageBody("아이디만 존재합니다 \n '아이디 + @이메일주소' 형식으로 작성해주세요");
    }
    public void ERROR_WrongFormID2()
    {
        CanvasShow();
        MessageReset();
        MessageType(false);
        MessageTitle("아이디 입력 오류");
        MessageBody("아이디가 잘못 되었습니다. \n 아이디는 문자 나 숫자로만 입력이 가능합니다");
    }

    /// <summary>
    /// 잘못된 비밀번호 입력형식
    /// </summary>
    public void ERROR_WrongFormPW() 
    {
        CanvasShow(); 
        MessageReset();
        MessageType(false);
        MessageTitle("비밀번호 입력 오류");
        MessageBody("비밀번호는 3~15자이어야 합니다");
    }

    /// <summary>
    /// 잘못된 비밀번호 입력형식2
    /// </summary>
    public void ERROR_WrongFormPW2()
    {
        CanvasShow();
        MessageReset();
        MessageType(false);
        MessageTitle("비밀번호 입력 오류");
        MessageBody("비밀번호는 \'숫자 + 영문자\'만 입력할 수 있습니다");
    }

    /// <summary>
    /// 성공 내용 작성 함수
    /// </summary>
    /// <param name="title">제목</param>
    /// <param name="body">내용</param>
    public void Success_Inform(string title, string body)
    {
        CanvasShow();
        MessageReset();
        MessageType(true);
        MessageTitle(title);
        MessageBody(body);
    }
    #endregion
}

