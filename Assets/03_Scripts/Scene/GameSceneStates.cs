using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

public class GameSceneStates : SceneManagerBase
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

    /// <summary>
    /// 현재 씬의 인덱스
    /// </summary>
    [SerializeField]
    private int nowSceneIndex;
    /// <summary>
    /// 이전 씬의 인덱스
    /// </summary>
    [SerializeField]
    private int prevSceneIndex;
    #endregion

    private void Start()
    {
        if (ResetToDisable() == true)//모두 비활성화 되면
        { 
            //동기화 & 활성화할 스크립트
            prevSceneIndex = NowSceneIndex();
            ManagerSetActive(nowSceneIndex, true);
            print("Done");
        }
    }

    /// <summary>
    /// 모든 게임메니저 오브젝트를 비활성화하는 함수
    /// </summary>
    /// <returns>정상 동작 체크용 bool</returns>
    private bool ResetToDisable()
    {
        for (int i = 0; i < Manager.Instance.managerScipts.Count; i++)
        {
            ManagerSetActive(i, false);
        }
        return true;
    }

    /// <summary>
    /// 현재 씬 인덱스 받아오는 함수
    /// </summary>
    /// <returns>현재씬의 Index번호</returns>
    private int NowSceneIndex()
    {
        Scene scene = SceneManager.GetActiveScene();
        return nowSceneIndex = scene.buildIndex;
    }

    /// <summary>
    /// 원하는 게임오브젝트 활성여부를 컨트롤하기 위함
    /// </summary>
    /// <param name="i">인덱스</param>
    /// <param name="isTrue"></param>
    private void ManagerSetActive(int i, bool isTrue)
    {
        Manager.Instance.managerScipts[i].gameObject.SetActive(isTrue);
    }

    /// <summary>
    /// 씬 검토
    /// </summary>
    private void CheckScene()
    {
        NowSceneIndex();
        if (prevSceneIndex == nowSceneIndex)
        {
            return;
        }

        //달라졌다면 (다른 씬 이동처리)
        _selectedScene = (EnumScene)nowSceneIndex;
        
        //해당 씬에서 처리 할 것
        switch (_selectedScene)
        {
            case EnumScene.Title:
                break;
            case EnumScene.Lobby:
                break;
            case EnumScene.Room:
                break;
            case EnumScene.Game:
                break;
        }
        //GetEnumIndex(_selectedScene);
    }
    /*아직 필요없음
    /// <summary>
    /// enum으로 씬에 대한 숫자를 반환하는 함수
    /// </summary>
    /// <param name="_selectedScene">현재 enum 상태</param>
    /// <returns></returns>
    private int GetEnumIndex(EnumScene _selectedScene)
    {
        return nowSceneIndex = Convert.ToInt32(_selectedScene);
    }
    */
    /// <summary>
    /// EnumScene에 해당하는 sceneindex 통해 씬을 검색하는 기능
    /// </summary>
    private void GetSceneByIndex(int index)
    {
        SceneManager.GetSceneByBuildIndex(index);
    }

    /// <summary>
    /// 씬 부르기
    /// </summary>
    /// <param name="i">씬 인덱스</param>
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
