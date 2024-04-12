using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// 싱글톤이 적용된 메니저
/// </summary>
public class Manager : SingletonOfT<Manager>
{
    public PopUpUIManager popUpUIManager { get => _popUpUIManager; }
    public List<SceneManagerBase> managerScipts { get => _managerScipts; }
    #region 연결 목록
    //하이어라키에 올라온 게임오브젝트의 컴포넌트기준
    [Header("Scene")]
    [SerializeField]
    private List<SceneManagerBase> _managerScipts;

    [Header("UI")]
    [SerializeField]
    private PopUpUIManager _popUpUIManager;
    #endregion

    public void Awake()
    {
        if (Init() == true)
        {
            DontDestroyOnLoad(this);
        }
    }
}

