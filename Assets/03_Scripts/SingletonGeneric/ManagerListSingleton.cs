using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ۼ����� �� ��ϵ�
/// </summary>
public enum SceneList
{
    None,
    Title,
    Lobby,
    Room,
    Game
}

/// <summary>
/// ������ �޴��� Ŭ����
/// </summary>
public  class ManagerListSingleton : SingletonOfT<ManagerListSingleton>
{
    #region ������Ƽ

    /// <summary>
    /// ���� ��
    /// </summary>
    public SceneList currentlist { get => _currentlist; set { } }

    public PopUpUIManager popUpUIManager { get => _popUpUIManager; }
    #endregion 

    #region �̱���
    public void Awake()
    {
        if (Init() == true)
        { 
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    #region ���κ���
    [SerializeField]
    private SceneList _currentlist = SceneList.None;
    #endregion

    #region ���� ���
    //���̾��Ű�� �ö�� ���ӿ�����Ʈ�� ������Ʈ����

    [Header("Scene")]
    [SerializeField]
    private List<SceneManagerBase> _managerScipts;

    [Header("UI")]
    [SerializeField]
    private PopUpUIManager _popUpUIManager;
    #endregion

    /// <summary>
    /// ���� ���� ���� �б��Լ�
    /// </summary>
    public void CurrentScene()
    {
        switch (currentlist)
        {
            case SceneList.None:
                break;
            case SceneList.Title:
                break;
            case SceneList.Lobby:
                break;
            case SceneList.Room:
                break;
            case SceneList.Game:
                break;
        }
    }
}
