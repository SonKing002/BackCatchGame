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

public class GameSceneStates : SingletonOfT<GameSceneStates>
{

    #region ������Ƽ
    /// <summary>
    /// ���� ��
    /// </summary>
    public EnumScene selectScene { get => _selectedScene; set { } }
    #endregion

    #region ���κ���

    /// <summary>
    /// �� ���¸� �б��ϴ� �̳�Ÿ�� ����
    /// </summary>
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

    private void Awake()
    {
        if (Init() == true)
        { 
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        //����ȭ & Ȱ��ȭ�� ��ũ��Ʈ
        nowSceneIndex = NowSceneIndex();
        prevSceneIndex = nowSceneIndex;
    }

    private void Update()
    {
        CheckScene(); 
    }

    /// <summary>
    /// ���� �� �ε��� �޾ƿ��� �Լ�
    /// </summary>
    /// <returns>������� Index��ȣ</returns>
    private int NowSceneIndex()
    {
        Scene scene = SceneManager.GetActiveScene();
        return scene.buildIndex;
    }

    /// <summary>
    /// �� ����
    /// </summary>
    private void CheckScene()
    {
        //�޶����ٸ� (�ٸ� �� �̵�ó��)
        _selectedScene = (EnumScene)nowSceneIndex;
        
        //�ش� ������ ó�� �� ��
        switch (_selectedScene)
        {
            case EnumScene.Title:
                
                if (CustomPhoton.Instance.isLogin == false)
                {
                    return;
                }

                GoToNextScene();

                break;
            case EnumScene.Lobby:
                break;
            case EnumScene.Room:
                break;
            case EnumScene.Game:
                break;
        }

        if (prevSceneIndex == nowSceneIndex)
        {
            return;
        }
        //����ȭ
        prevSceneIndex = nowSceneIndex;
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

    /// <summary>
    /// ���� ������ �̵��ϴ� �Լ�
    /// </summary>
    public void GoToNextScene()
    {
        nowSceneIndex = NowSceneIndex();

        _selectedScene = (EnumScene)(++nowSceneIndex);
        ChangeScene(nowSceneIndex);
        print(nowSceneIndex);
        print(SceneManager.GetActiveScene());
    }
}
