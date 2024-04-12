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
/// �� ������ �޴��� Ŭ����
/// </summary>
public  class ManagerListSingleton : SingletonOfT<ManagerListSingleton>
{
    #region ������Ƽ
    /// <summary>
    /// ���� ��
    /// </summary>
    [SerializeField]
    public SceneList currentlist { get => _currentlist; set { } }
    #endregion 

    #region �̱���
    public override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
    }
    #endregion

    #region ���κ���
    private SceneList _currentlist = SceneList.None;

    [SerializeField]
    private List<SceneManagerBase> _managerScipts;
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
