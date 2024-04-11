using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 로그인과 회원가입에 대한 함수 수정을 일괄 처리하기 위해 상위 클래스작성
/// </summary>
public class PopUpInputFieldBaseUI : PopUpBaseUI
{
    #region 입력창 2개에 대한 프로퍼티
    public string idInputField { get => _idInputField.text; set { } }
    public string pwInputField { get => _pwInputField.text; set { } }
    public bool isSuccess { get => _isSuccess; set { } }
    #endregion

    #region 내부변수 (protected)
    /*
     * 파생 클래스의 인스펙터 창에서 연결하는 형식으로 제작
     */
    [SerializeField]
    protected TMP_InputField _idInputField;//아이디
    [SerializeField]
    protected TMP_InputField _pwInputField;//비밀번호
    [SerializeField]
    protected Button _confirmButton;
    protected bool _isSuccess;
    #endregion

    /// <summary>
    /// 버튼의 연결사항 함수
    /// </summary>
    public virtual void OnClickButton_Link() 
    {
        //파생 클래스의 각각의 OnClickButton_Link 함수를 활용, (해당 코드 블럭 내부에 호출하고, 확장사용가능)
        _isSuccess = CheckCondition(_idInputField.text, _pwInputField.text);
    }

    public bool CheckCondition(string idText, string pwText)
    {
        //빈 공간이라면,
        if (string.IsNullOrEmpty(idText) || string.IsNullOrWhiteSpace(idText))
        {
            PopUpInformWindowsUI.Instance.ERROR_EmptyInputID();
            return false;
        }
        if (string.IsNullOrEmpty(pwText) || string.IsNullOrWhiteSpace(pwText))
        {
            PopUpInformWindowsUI.Instance.ERROR_EmptyInputPW();
            return false;
        }

        #region 아이디 검사
        //이메일 형식 검사항목
        Regex regexRull = new Regex("@");
        Match match1 = regexRull.Match(idText);

        //이메일 형식이 아니라면
        if (match1.Success == false)
        {
            PopUpInformWindowsUI.Instance.ERROR_WrongFormID();
            return false;
        }

        //아이디부분이 비어있다면
        string[] vals = regexRull.Split(idText);
        if (vals[0].Length <= 0)
        {
            print($"아이디 앞부분 : {vals[0]}");
            PopUpInformWindowsUI.Instance.ERROR_WrongFormID();
        }

        //숫자문자 입력 검사항목
        string ourPattern = "^[a-zA-Z0-9]";//문자나 숫자
        regexRull = new Regex(ourPattern);
        if (regexRull.IsMatch(idText)==false)
        {
            PopUpInformWindowsUI.Instance.ERROR_WrongFormID2();
            return false;
        }
        #endregion

        #region 패스워드 검사

        //글자수 >= 4, 숫자랑 문자만 입력 가능
        if (pwText.Length <= 3 || regexRull.IsMatch(pwText)==false)
        {
            PopUpInformWindowsUI.Instance.ERROR_WrongFormPW();
            return false;
        }

        if (regexRull.IsMatch(pwText) == false)
        {
            PopUpInformWindowsUI.Instance.ERROR_WrongFormPW2();
            return false;
        }
        return true;
        #endregion
    }

}
