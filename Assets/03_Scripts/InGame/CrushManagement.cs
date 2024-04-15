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
    //리스폰 시간
    public float respawnTime {  get => _respawnTime; set => _respawnTime = value; }
    //벌어지는 각도(등 판정 체크를 위함
    [SerializeField][Range(0f, 360f)] float _viewAngle;
    //등 판정 체크할 거리
    [SerializeField] float _distance;
    //체크 할 적 레이어 마스크
    [SerializeField] LayerMask _enemyTeamMask;
    //장애물 마스크 > 적과 사이에 벽, 지형지물, 장애물 체크용
    [SerializeField] LayerMask _obstacleMask;
    //공격 가능 여부
    private bool _vulnerable = false;
    //안에 들어온 대상의 정보 체크
    List<Collider> _hitTargetList = new List<Collider>();
    //공격 가능한 거리
    [SerializeField]private float _attackDistance;
    //기즈모 체크용 위로 조금 올린 이유는 땅바닥 기준으로 생성되서 올려서 정확하게 판단용
    [SerializeField]private float _height;
    [SerializeField]private Transform[] _respawnPoint;
    //피격 후 무적 시간
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
        //오브젝트가 y축을 기준으로 회전한 각도를 반환, 여기에 180도를 더해 현재 오브젝트가 바라보는 반대 방향의 각도를 구합니다.
        float backAngle = (transform.eulerAngles.y) + 180;
        Vector3 rightDir = AngleToDir(backAngle + _viewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(backAngle - _viewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(backAngle);

        //리스트 내용을 전부 제거
        _hitTargetList.Clear();

        Collider[] Targets = Physics.OverlapSphere(myPosition, _distance, _enemyTeamMask);

        if (Targets.Length == 0)
        {
            _vulnerable = false;
            return;
        }

        foreach (Collider Enemy in Targets)
        {
            //레이캐스트에 들어온 충돌체의 위치값 체크
            Vector3 targetPos = Enemy.transform.position + transform.up * _height;
            //충돌한 거리 체크
            Vector3 targetDir = (targetPos - myPosition).normalized;
            //충돌체와 나와의 각도 계산
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;



            if (targetAngle <= _viewAngle * 0.5f && !Physics.Raycast(myPosition, targetDir, _distance, _obstacleMask))
            {
                //죽었을 때는 판정 안됨
                if (_playerController.currentState == State.Death || _hitTargetList.Contains(Enemy)) return;

                
                //리스트에 충돌체 정보 추가
                _hitTargetList.Add(Enemy);
                _enemyPlayerController = Enemy.GetComponent<PlayerController>();

                //디버깅 및 테스트용으로 사용하는 줄 > TODO 삭제요망
                Debug.DrawLine(myPosition, targetPos, Color.red);
                _vulnerable = true;
                if (_vulnerable && _hitTargetList.Contains(Enemy) && Physics.Raycast(myPosition, targetDir, _attackDistance, _enemyTeamMask))
                {
                    //죽었거나 데미지를 입을 때는 중복 데미지 체크
                    if (_playerController.currentState == State.Death || !_enemyPlayerController.canAttack || _playerController._damaged == true)
                    {
                        return;
                    }
                    _playerController.currentState = State.Damage;
                    _playerController.hpCount += 1;
                    _playerController.switchUpdate(_playerController.currentState);
                    Debug.Log("변경전" + _enemyPlayerController.currentState);
                    _enemyPlayerController.currentState = State.Attack;
                    Debug.Log("변경후" + _enemyPlayerController.currentState);
                    _enemyPlayerController.switchUpdate(_enemyPlayerController.currentState);
                    _playerController._damaged = true;
                    Invoke("Damaged", _invisibleTime);

                    //2대 맞는다면...
                    if (_playerController.hpCount >= 2)
                    {
                        //_playerTransform.position = _respawnPoint[0].position;
                        //혹시 3대 이상 카운트가 올라가거나 죽음 상태라면
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
        //내 포지션 체크 오브젝트 위치보다 조금 더 높게 설정한 이유는 >점프하고 닿았을 때를 대비함 (수정필요)
        Vector3 myPosition = transform.position + transform.up * _height;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(myPosition, _distance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(myPosition, _attackDistance);

        //오브젝트가 y축을 기준으로 회전한 각도를 반환, 여기에 180도를 더해 현재 오브젝트가 바라보는 반대 방향의 각도를 구합니다.
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
        //각도를 벡터값으로 바꿔주는 함수
        Vector3 AngleToDir(float angle)
    {
        //각도를 라디안으로 반환하고.
        float radian = angle * Mathf.Deg2Rad;
        //각도에 해당하는 방향 벡터 계산 , 라디안 값의 사인, 0 , 라디안 값의 코사인으로 계산함. > 방향벡터는 xz평면에만 존재하기에 y축 좌표는 0으로 처리
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

}
