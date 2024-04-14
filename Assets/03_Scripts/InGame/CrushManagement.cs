using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CrushManagement : MonoBehaviour
{
    //벌어지는 각도(등 판정 체크를 위함
    [SerializeField][Range(0f, 360f)] float _viewAngle;
    //등 판정 체크할 거리
    [SerializeField] float _distance;
    //체크 할 적 레이어 마스크
    [SerializeField] LayerMask _enemyMask;
    //장애물 마스크 > 적과 사이에 벽, 지형지물, 장애물 체크용
    [SerializeField] LayerMask _obstacleMask;

    List<Collider> _hitTargetList = new List<Collider>();
    private void OnDrawGizmos()
    {
        //내 포지션 체크 오브젝트 위치보다 조금 더 높게 설정한 이유는 >점프하고 닿았을 때를 대비함 (수정필요)
        Vector3 myPosition = transform.position + Vector3.up * 0.3f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(myPosition, _distance);

        //오브젝트가 y축을 기준으로 회전한 각도를 반환, 여기에 180도를 더해 현재 오브젝트가 바라보는 반대 방향의 각도를 구합니다.
        float backAngle = (transform.eulerAngles.y)+ 180;  
        Vector3 rightDir = AngleToDir(backAngle + _viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(backAngle - _viewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(backAngle);

        Debug.DrawRay(myPosition, rightDir * _distance, Color.black);
        Debug.DrawRay(myPosition, leftDir * _distance, Color.black);
        Debug.DrawRay(myPosition, lookDir * _distance, Color.yellow);

        //리스트 내용을 전부 제거
        _hitTargetList.Clear();

        Collider[] Targets = Physics.OverlapSphere(myPosition, _distance, _enemyMask);

        if (Targets.Length == 0) return;

        foreach (Collider Enemy in Targets)
        {
            //레이캐스트에 들어온 충돌체의 위치값 체크
            Vector3 targetPos = Enemy.transform.position;
            //충돌한 거리 체크
            Vector3 targetDir = (targetPos - myPosition).normalized;
            //충돌체와 나와의 각도 계산
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= _viewAngle * 0.5f && !Physics.Raycast(myPosition, targetDir, _distance, _obstacleMask))
            {
                //만약 리스트 안에 존재하는 적이라면 중복 등록을 피하기 위해 if문 사용
                if (_hitTargetList.Contains(Enemy))
                {
                    return;
                }
                //리스트에 충돌체 정보 추가
                _hitTargetList.Add(Enemy);
                //디버깅 및 테스트용으로 사용하는 줄 > TODO 삭제요망
                Debug.DrawLine(myPosition, targetPos, Color.red);
            }
        }
    }

    //각도를 벡터값으로 바꿔주는 함수
    Vector3 AngleToDir(float angle)
    {
        //각도를 라디안으로 반환하고.
        float radian = angle * Mathf.Deg2Rad;
        //각도에 해당하는 방향 벡터 계산 , 라디안 값의 사인, 0 , 라디안 값의 코사인으로 계산함. > 방향벡터는 xz평면에만 존재하기에 y축 좌표는 0으로 처리
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

}
