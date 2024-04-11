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
    #endregion

    /// <summary>
    /// 버튼의 연결사항 함수
    /// </summary>
    public virtual void OnClickButton_Link() 
    {
        //파생 클래스의 각각의 OnClickButton_Link 함수를 활용, 해당 코드 블럭 내부에 호출해서 실행하면 됌
        CheckCondition(_idInputField.text, _pwInputField.text);
    }

    public void CheckCondition(string idText, string pwText)
    {
        //빈 공간이라면,
        if (string.IsNullOrEmpty(idText) || string.IsNullOrWhiteSpace(idText))
        {
            PopUpInformWindowsUI.Instance.ERROR_EmptyInputID();
            return;
        }
        if (string.IsNullOrEmpty(pwText) || string.IsNullOrWhiteSpace(pwText))
        {
            PopUpInformWindowsUI.Instance.ERROR_EmptyInputPW();
            return;
        }

        #region 아이디 검사
        //아이디 형식 검사항목
        Regex regexIDRull1 = new Regex("@");
        Match match1 = regexIDRull1.Match(idText);

        //이메일 형식이 아니라면
        if (match1.Success == false)
        {
            PopUpInformWindowsUI.Instance.ERROR_WrongFormID();
            return;
        }

        //아이디부분이 비어있다면
        string[] vals = regexIDRull1.Split(idText);
        if (vals[0].Length <= 0)
        {
            print($"아이디 앞부분 : {vals[0]}");
            PopUpInformWindowsUI.Instance.ERROR_WrongFormID();
        }
        #endregion

        #region 패스워드 검사

        //글자수 >= 4
        if (pwText.Length <= 3)
        {
            PopUpInformWindowsUI.Instance.ERROR_WrongFormPW();
            return;
        }

        //숫자 + 영문 
        foreach (char val in pwText)
        {
            bool isText = (65 <= val && val <= 90) || (97 <= val && val <= 121);
            bool isNum;

        }
        #endregion

    }

}
