using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CustomPhoton : MonoBehaviourPunCallbacks //������Ƽ�� �޼ҵ�� �ۼ��Ǿ��ִ�
{
    //Ŭ���̾�Ʈ ���� üũ�� : ��� ���� ��������� ���� �� 1�� ����
    string gameVersion = "1";

    [SerializeField]
    private TMP_Text _logText; 

    private void Awake()
    {
        //�����Ͱ� �ε巹����, ������ Ŭ���̾�Ʈ�� �ڵ����� ���� �濡 ��ũ�� �� �ֵ��� �����Ѵ�.
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        Connect();
    }

    //ȣ������ �ʾƵ� �̰��� �ۼ��ϸ�, 
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster by called PhotonPun");
        _logText.text = "���� ���� �Ϸ�";

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        _logText.text = "���� ���� ����";
    }

    public void Connect()
    {
        //������ ����Ǿ����� ������
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion; //���� ������ �Ҵ��Ѵ�
            PhotonNetwork.ConnectUsingSettings(); //�غ�� �������Ϸ� ������ �����ϴ� �Լ�
            //OnConnectedToMaster();
        }
        else
        { 
            
        }
    }
}
