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
/// 씬 관리용 메니저 클래스
/// </summary>
public  class ManagerListSingleton : SingletonOfT<ManagerListSingleton>
{
    #region 프로퍼티
    /// <summary>
    /// 현재 씬
    /// </summary>
    [SerializeField]
    public SceneList currentlist { get => _currentlist; set { } }
    #endregion 

    #region 싱글톤
    public override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
    }
    #endregion

    #region 내부변수
    private SceneList _currentlist = SceneList.None;

    [SerializeField]
    private List<SceneManagerBase> _managerScipts;
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
