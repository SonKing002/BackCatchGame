using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushManagement : MonoBehaviour
{
    //벌어지는 각도(등 판정 체크를 위함
    [SerializeField][Range(0f, 360f)] float _viewAngle;
    //등 판정 체크할 거리
    [SerializeField] float _distance;
    //체크 할 적 레이어 마스크
    [SerializeField] LayerMask _enemyMask;
    
    [SerializeField] LayerMask _obstacleMask;

    List<Collider> _hitTargetList = new List<Collider>();

    private void Update()
    {
     
    }
    private void OnDrawGizmos()
    {
        //내 포지션 체크
        Vector3 myPosition = transform.position + Vector3.up * 0.5f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(myPosition, _distance);

        float lookingAngle = (transform.eulerAngles.y)+ 180;  //캐릭터가 바라보는 방향의 등 위치
        Vector3 rightDir = AngleToDir(lookingAngle + _viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(lookingAngle - _viewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPosition, rightDir * _distance, Color.black);
        Debug.DrawRay(myPosition, leftDir * _distance, Color.black);
        Debug.DrawRay(myPosition, lookDir * _distance, Color.yellow);

        //리스트 내용을 전부 제거
        _hitTargetList.Clear();

        Collider[] Targets = Physics.OverlapSphere(myPosition, _distance, _enemyMask);

        if (Targets.Length == 0) return;
    }

    //각도를 벡터값으로 바꿔주는 함수
    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

}
