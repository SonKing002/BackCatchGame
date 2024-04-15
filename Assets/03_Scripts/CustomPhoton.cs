using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CustomPhoton : MonoBehaviourPunCallbacks //프로퍼티와 메소드등 작성되어있다
{
    //클라이언트 버전 체크용 : 출시 후의 변경사항이 없는 한 1로 유지
    string gameVersion = "1";

    [SerializeField]
    private TMP_Text _logText; 

    private void Awake()
    {
        //마스터가 로드레벨시, 나머지 클라이언트가 자동으로 같은 방에 싱크될 수 있도록 제어한다.
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        Connect();
    }

    //호출하지 않아도 이곳에 작성하면, 
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("OnConnectedToMaster by called PhotonPun");
        _logText.text = "서버 접속 완료";

    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        _logText.text = "서버 접속 실패";
    }

    public void Connect()
    {
        //서버에 연결되어있지 않으면
        if (PhotonNetwork.IsConnected == false)
        {
            PhotonNetwork.GameVersion = gameVersion; //현재 버전을 할당한다
            PhotonNetwork.ConnectUsingSettings(); //준비된 구성파일로 서버에 연결하는 함수
            //OnConnectedToMaster();
        }
        else
        { 
            
        }
    }
}
