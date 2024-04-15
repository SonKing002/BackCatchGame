using MJ.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimatorManager;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CrushManagement : MonoBehaviour
{
    //�������� ����(�� ���� üũ�� ����
    [SerializeField][Range(0f, 360f)] float _viewAngle;
    //�� ���� üũ�� �Ÿ�
    [SerializeField] float _distance;
    //üũ �� �� ���̾� ����ũ
    [SerializeField] LayerMask _enemyTeamMask;
    //��ֹ� ����ũ > ���� ���̿� ��, ��������, ��ֹ� üũ��
    [SerializeField] LayerMask _obstacleMask;
    //���� ���� ����
    public bool _vulnerable = false;
    //�ȿ� ���� ����� ���� üũ
    List<Collider> _hitTargetList = new List<Collider>();
    //���� ������ �Ÿ�
    [SerializeField] float _attackDistance;
    //����� üũ�� ���� ���� �ø� ������ ���ٴ� �������� �����Ǽ� �÷��� ��Ȯ�ϰ� �Ǵܿ�
    [SerializeField] float _height;
    


    PlayerController _playerController;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        Debug.Log(_vulnerable);
    }
    private void OnDrawGizmos()
    {
        //�� ������ üũ ������Ʈ ��ġ���� ���� �� ���� ������ ������ >�����ϰ� ����� ���� ����� (�����ʿ�)
        Vector3 myPosition = transform.position + transform.up * _height;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(myPosition, _distance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(myPosition, _attackDistance);

        //������Ʈ�� y���� �������� ȸ���� ������ ��ȯ, ���⿡ 180���� ���� ���� ������Ʈ�� �ٶ󺸴� �ݴ� ������ ������ ���մϴ�.
        float backAngle = (transform.eulerAngles.y)+ 180;  
        Vector3 rightDir = AngleToDir(backAngle + _viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(backAngle - _viewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(backAngle);

        Debug.DrawRay(myPosition, rightDir * _distance, Color.black);
        Debug.DrawRay(myPosition, leftDir * _distance, Color.black);
        Debug.DrawRay(myPosition, lookDir * _distance, Color.yellow);

        //����Ʈ ������ ���� ����
        _hitTargetList.Clear();

        Collider[] Targets = Physics.OverlapSphere(myPosition, _distance, _enemyTeamMask);

        if (Targets.Length == 0)
        {
            _vulnerable = false;
            return;
        }

        foreach (Collider Enemy in Targets)
        {
            //����ĳ��Ʈ�� ���� �浹ü�� ��ġ�� üũ
            Vector3 targetPos = Enemy.transform.position + transform.up * _height;
            //�浹�� �Ÿ� üũ
            Vector3 targetDir = (targetPos - myPosition).normalized;
            //�浹ü�� ������ ���� ���
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= _viewAngle * 0.5f && !Physics.Raycast(myPosition, targetDir, _distance, _obstacleMask))
            {
                //���� ����Ʈ �ȿ� �����ϴ� ���̶�� �ߺ� ����� ���ϱ� ���� if�� ���
                if (_hitTargetList.Contains(Enemy))
                {
                    return;
                }
                //����Ʈ�� �浹ü ���� �߰�
                _hitTargetList.Add(Enemy);
                //����� �� �׽�Ʈ������ ����ϴ� �� > TODO �������
                Debug.DrawLine(myPosition, targetPos, Color.red);
                _vulnerable = true;
                if (_vulnerable && _hitTargetList.Contains(Enemy) && Physics.Raycast(myPosition, targetDir, _attackDistance, _enemyTeamMask))
                {

                    //ü���� ��� �Լ� �߰���� TODO

                    _playerController.currentState = State.Damage;
                    _playerController.switchUpdate(_playerController.currentState);
                    _playerController._damaged = true;
                    Debug.Log("���Ŀ�Ф�");

                    //TODO ���� �䱸 
                    Invoke("Damaged", 0.5f);
                }
            }
        }
    }
    void Damaged()
    {
        _playerController._damaged = false;
    }
    //������ ���Ͱ����� �ٲ��ִ� �Լ�
    Vector3 AngleToDir(float angle)
    {
        //������ �������� ��ȯ�ϰ�.
        float radian = angle * Mathf.Deg2Rad;
        //������ �ش��ϴ� ���� ���� ��� , ���� ���� ����, 0 , ���� ���� �ڻ������� �����. > ���⺤�ʹ� xz��鿡�� �����ϱ⿡ y�� ��ǥ�� 0���� ó��
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

}
