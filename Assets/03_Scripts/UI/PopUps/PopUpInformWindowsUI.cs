using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




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

    [SerializeField]
    private bool isTrue;

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
    private Image _backGround;
    [SerializeField]
    private TMP_Text _titleText;
    [SerializeField]
    private TMP_Text _bodyText;
    [SerializeField]
    private Shadow _shodowImage;
    [SerializeField]
    private Image _buttonImage;

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
    /// 안내창 컬러 결정
    /// </summary>
    /// <param name="isTrue">true일 때 알림종류 분기</param>
    /// <param name="text">알릴 메세지</param>
    public void CheckUp(bool isTrue, string text)
    {
        switch (isTrue)
        {
            case true://알림용
                InformingColor();
                _titleText.text = "확인";
                _bodyText.text = text;
                break;
            case false://경고용
                WarningColor();
                _titleText.text = "경고";
                _bodyText.text = text;
                break;
        }
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

}

