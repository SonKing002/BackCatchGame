using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// �ۼ����� �� ��ϵ�
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
    #region ������Ƽ

    /// <summary>
    /// ���� ��
    /// </summary>
    public EnumScene selectScene { get => _selectedScene; set { } }
    #endregion

    #region ���κ���

    [SerializeField]
    private EnumScene _selectedScene = EnumScene.Title;

    [SerializeField]
    private int nowSceneNum;
    [SerializeField]
    private int prevSceneNum;//�Ź� foreach���� ������ �ʱ� ����
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
    /// <param name="i">�ε���</param>
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
    /// enum���� ���� ���� ���ڸ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_selectedScene">���� enum ����</param>
    /// <returns></returns>
    private int GetEnumIndex(EnumScene _selectedScene)
    {
        return nowSceneNum = Convert.ToInt32(_selectedScene);
    }

    /// <summary>
    /// EnumScene�� �ش��ϴ� sceneNum ���� ���� �ε� �ϴ�Sync �Լ�
    /// </summary>
    private void LoadScene(int enumIndex)
    {
        UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(enumIndex);
    }
    
}
