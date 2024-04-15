using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

public class GameSceneStates : SceneManagerBase
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

    /// <summary>
    /// ���� ���� �ε���
    /// </summary>
    [SerializeField]
    private int nowSceneIndex;
    /// <summary>
    /// ���� ���� �ε���
    /// </summary>
    [SerializeField]
    private int prevSceneIndex;
    #endregion

    private void Start()
    {
        //����ȭ & Ȱ��ȭ�� ��ũ��Ʈ
        prevSceneIndex = NowSceneIndex();
    }



    /// <summary>
    /// ���� �� �ε��� �޾ƿ��� �Լ�
    /// </summary>
    /// <returns>������� Index��ȣ</returns>
    private int NowSceneIndex()
    {
        Scene scene = SceneManager.GetActiveScene();
        return nowSceneIndex = scene.buildIndex;
    }

    /// <summary>
    /// �� ����
    /// </summary>
    private void CheckScene()
    {
        NowSceneIndex();
        if (prevSceneIndex == nowSceneIndex)
        {
            return;
        }

        //�޶����ٸ� (�ٸ� �� �̵�ó��)
        _selectedScene = (EnumScene)nowSceneIndex;
        
        //�ش� ������ ó�� �� ��
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
    /*���� �ʿ����
    /// <summary>
    /// enum���� ���� ���� ���ڸ� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_selectedScene">���� enum ����</param>
    /// <returns></returns>
    private int GetEnumIndex(EnumScene _selectedScene)
    {
        return nowSceneIndex = Convert.ToInt32(_selectedScene);
    }
    */
    /// <summary>
    /// EnumScene�� �ش��ϴ� sceneindex ���� ���� �˻��ϴ� ���
    /// </summary>
    private void GetSceneByIndex(int index)
    {
        SceneManager.GetSceneByBuildIndex(index);
    }

    /// <summary>
    /// �� �θ���
    /// </summary>
    /// <param name="i">�� �ε���</param>
    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
