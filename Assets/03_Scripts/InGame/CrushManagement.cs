using MJ.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.UIElements;
using static AnimatorManager;

public class CrushManagement : MonoBehaviour
{
    //������ �ð�
    public float respawnTime {  get => _respawnTime; set => _respawnTime = value; }
    //�������� ����(�� ���� üũ�� ����
    [SerializeField][Range(0f, 360f)] float _viewAngle;
    //�� ���� üũ�� �Ÿ�
    [SerializeField] float _distance;
    //üũ �� �� ���̾� ����ũ
    [SerializeField] LayerMask _enemyTeamMask;
    //��ֹ� ����ũ > ���� ���̿� ��, ��������, ��ֹ� üũ��
    [SerializeField] LayerMask _obstacleMask;
    //���� ���� ����
    private bool _vulnerable = false;
    //�ȿ� ���� ����� ���� üũ
    List<Collider> _hitTargetList = new List<Collider>();
    //���� ������ �Ÿ�
    [SerializeField]private float _attackDistance;
    //����� üũ�� ���� ���� �ø� ������ ���ٴ� �������� �����Ǽ� �÷��� ��Ȯ�ϰ� �Ǵܿ�
    [SerializeField]private float _height;
    [SerializeField]private Transform[] _respawnPoint;
    //�ǰ� �� ���� �ð�
    [SerializeField] private float _invisibleTime;
    Transform _playerTransform;

    private PlayerController _enemyPlayerController;

    private PlayerController _playerController;

    private float _respawnTime;



    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _playerTransform = GetComponent<Transform>();
    }
    private void Update()
    {
        Vector3 myPosition = transform.position + transform.up * _height;
        //������Ʈ�� y���� �������� ȸ���� ������ ��ȯ, ���⿡ 180���� ���� ���� ������Ʈ�� �ٶ󺸴� �ݴ� ������ ������ ���մϴ�.
        float backAngle = (transform.eulerAngles.y) + 180;
        Vector3 rightDir = AngleToDir(backAngle + _viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(backAngle - _viewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(backAngle);

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
                //�׾��� ���� ���� �ȵ�
                if (_playerController.currentState == State.Death || _hitTargetList.Contains(Enemy)) return;

                
                //����Ʈ�� �浹ü ���� �߰�
                _hitTargetList.Add(Enemy);
                _enemyPlayerController = Enemy.GetComponent<PlayerController>();

                //����� �� �׽�Ʈ������ ����ϴ� �� > TODO �������
                Debug.DrawLine(myPosition, targetPos, Color.red);
                _vulnerable = true;
                if (_vulnerable && _hitTargetList.Contains(Enemy) && Physics.Raycast(myPosition, targetDir, _attackDistance, _enemyTeamMask))
                {
                    //�׾��ų� �������� ���� ���� �ߺ� ������ üũ
                    if (_playerController.currentState == State.Death || !_enemyPlayerController.canAttack || _playerController._damaged == true)
                    {
                        return;
                    }
                    _playerController.currentState = State.Damage;
                    _playerController.hpCount += 1;
                    _playerController.switchUpdate(_playerController.currentState);
                    Debug.Log("������" + _enemyPlayerController.currentState);
                    _enemyPlayerController.currentState = State.Attack;
                    Debug.Log("������" + _enemyPlayerController.currentState);
                    _enemyPlayerController.switchUpdate(_enemyPlayerController.currentState);
                    _playerController._damaged = true;
                    Invoke("Damaged", _invisibleTime);

                    //2�� �´´ٸ�...
                    if (_playerController.hpCount >= 2)
                    {
                        //_playerTransform.position = _respawnPoint[0].position;
                        //Ȥ�� 3�� �̻� ī��Ʈ�� �ö󰡰ų� ���� ���¶��
                        if (_playerController.hpCount >= 3 || _playerController.currentState == State.Death) { return; }

                        _playerController.currentState = State.Death;
                        _playerController.switchUpdate(_playerController.currentState);
                        Invoke("Respawn", 5.0f);
                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if (_playerController.hpCount >= 2)
        {
            _playerTransform.position = _respawnPoint[0].position;
        }
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


    }
    void Damaged()
    {
        _playerController._damaged = false;
    }
    void Respawn()
    {

        if (_playerController.currentState == State.Death)
        {
            _playerController.hpCount = 0;
            _playerController.currentState = State.Idle;
            _playerController.switchUpdate(_playerController.currentState);
        }
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
