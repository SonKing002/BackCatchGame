using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencySource : SingletonOfT<DependencySource>
{
    //public string nickname { get => _nickname; set { } }
    //public string lastLogin { get => _lastLogin; set { } }
    //public string userEmail { get => _userEmail; set { } }
    public string nickname;
    //public string NickName { get; set; }
    public string LastLogin { get; set; }
    public string UserEmail { get; set; }

    //[Header("���� ����")]
    //[SerializeField]
    //private string _nickname;
    //[SerializeField]
    //private string _lastLogin;
    //[SerializeField]
    //private string _userEmail;

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// �α��� ��ÿ� ������ ���� ����
    /// </summary>
    /// <param name="name"></param>
    /// <param name="time"></param>
    /// <param name="email"></param>
    public void GetData(string name, DateTime? time, string email)
    {
        Utils.LogGreen(name);
        nickname = name;
        Utils.LogGreen(nickname);
        LastLogin = time?.ToString("M");
        UserEmail = email;
    }
}