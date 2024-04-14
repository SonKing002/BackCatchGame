using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushManagement : MonoBehaviour
{
    //�������� ����(�� ���� üũ�� ����
    [SerializeField][Range(0f, 360f)] float _viewAngle;
    //�� ���� üũ�� �Ÿ�
    [SerializeField] float _distance;
    //üũ �� �� ���̾� ����ũ
    [SerializeField] LayerMask _enemyMask;
    
    [SerializeField] LayerMask _obstacleMask;

    List<Collider> _hitTargetList = new List<Collider>();

    private void Update()
    {
     
    }
    private void OnDrawGizmos()
    {
        //�� ������ üũ
        Vector3 myPosition = transform.position + Vector3.up * 0.5f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(myPosition, _distance);

        float lookingAngle = (transform.eulerAngles.y)+ 180;  //ĳ���Ͱ� �ٶ󺸴� ������ �� ��ġ
        Vector3 rightDir = AngleToDir(lookingAngle + _viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(lookingAngle - _viewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Debug.DrawRay(myPosition, rightDir * _distance, Color.black);
        Debug.DrawRay(myPosition, leftDir * _distance, Color.black);
        Debug.DrawRay(myPosition, lookDir * _distance, Color.yellow);

        //����Ʈ ������ ���� ����
        _hitTargetList.Clear();

        Collider[] Targets = Physics.OverlapSphere(myPosition, _distance, _enemyMask);

        if (Targets.Length == 0) return;
    }

    //������ ���Ͱ����� �ٲ��ִ� �Լ�
    Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

}
