using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 작성중인 씬 목록들
/// </summary>
public enum EnumScene
{
    Title,
    Lobby,
    Room,
    Game
}

public class GameSceneManager : SceneManagerBase
{
    #region 프로퍼티

    /// <summary>
    /// 현재 씬
    /// </summary>
    public EnumScene selectScene { get => _selectedScene; set { } }
    #endregion

    #region 내부변수

    [SerializeField]
    private EnumScene _selectedScene = EnumScene.Title;

    [SerializeField]
    private int nowSceneNum;
    [SerializeField]
    private int prevSceneNum;//매번 foreach문을 돌리기 않기 위함
    #endregion


    
    private bool ResetToDisable()
    {
        for (int i = 0; i < Manager.Instance.managerScipts.Count; i++)
        {
            ManagerSetActive(i, false);
        }
        return true;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="i">인덱스</param>
    /// <param name="isTrue"></param>
    private void ManagerSetActive(int i, bool isTrue)
    {
        Manager.Instance.managerScipts[i].enabled = isTrue;
    }

    private void SceneState()
    {
        switch (_selectedScene)
        {
            case EnumScene.Title:
                GetEnumIndex(_selectedScene);
                break;
            case EnumScene.Lobby:
                break;
            case EnumScene.Room:
                break;
            case EnumScene.Game:
                break;
        }
    }

    /// <summary>
    /// enum으로 씬에 대한 숫자를 반환하는 함수
    /// </summary>
    /// <param name="_selectedScene">현재 enum 상태</param>
    /// <returns></returns>
    private int GetEnumIndex(EnumScene _selectedScene)
    {
        return nowSceneNum = Convert.ToInt32(_selectedScene);
    }

    /// <summary>
    /// EnumScene에 해당하는 sceneNum 통해 씬을 로드 하는Sync 함수
    /// </summary>
    private void LoadScene(int enumIndex)
    {
        UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(enumIndex);
    }
    
}
