using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 작성중인 씬 목록들
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
/// 관리용 메니저 클래스
/// </summary>
public  class ManagerListSingleton : SingletonOfT<ManagerListSingleton>
{
    #region 프로퍼티

    /// <summary>
    /// 현재 씬
    /// </summary>
    public SceneList currentlist { get => _currentlist; set { } }

    public PopUpUIManager popUpUIManager { get => _popUpUIManager; }
    #endregion 

    #region 싱글톤
    public void Awake()
    {
        if (Init() == true)
        { 
            DontDestroyOnLoad(this);
        }
    }
    #endregion

    #region 내부변수
    [SerializeField]
    private SceneList _currentlist = SceneList.None;
    #endregion

    #region 연결 목록
    //하이어라키에 올라온 게임오브젝트의 컴포넌트기준

    [Header("Scene")]
    [SerializeField]
    private List<SceneManagerBase> _managerScipts;

    [Header("UI")]
    [SerializeField]
    private PopUpUIManager _popUpUIManager;
    #endregion

    /// <summary>
    /// 현재 씬에 대한 분기함수
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
