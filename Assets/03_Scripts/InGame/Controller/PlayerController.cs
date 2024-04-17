using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static StateManagement;
namespace MJ.Player
{
    public class PlayerController : MonoBehaviour
    {
        /// <summary>
        /// 플레이어의 체력카운트
        /// </summary>
        public int hpCount { get => _hpCount; set => _hpCount = value; }
        /// <summary>
        /// 공격 가능여부 > 외부에서 변동해야함
        /// </summary>
        public bool canAttack { get => _canAttack; set => _canAttack = value; }
        /// <summary>
        /// 현재 상태
        /// </summary>
        public State currentState { get => _currentState; set { } }
        /// <summary>
        /// 인스펙터에서 등록할 inputActionAsset
        /// </summary>
        public InputActionAsset inputActionAsset;
        /// <summary>
        /// 중력
        /// </summary>
        [SerializeField] private float _gravity;
        /// <summary>
        /// 플레이어 앞뒤 움직임 속도
        /// </summary>
        [SerializeField] private float _moveSpeed;
        /// <summary>
        /// 플레이어 회전 속도
        /// </summary>
        [SerializeField] private float _rotateSpeed;
        /// <summary>
        /// 플레이어 점프력
        /// </summary>
        [SerializeField] private float _jumpPower;
        /// <summary>
        /// 지면에 닿는지 체크하기 위한 거리
        /// </summary>
        [SerializeField] private float _groundDistance;
        /// <summary>
        /// 레이캐스트로 체크할 대상의 레이어마스크
        /// </summary>
        [SerializeField] private LayerMask groundLayer;
        /// <summary>
        /// 공격 후 공격 불가능한 시간
        /// </summary>
        [SerializeField] private float _attackDelay;
        /// <summary>
        /// 캐릭터의 캐릭터 컨트롤러 스크립트
        /// </summary>
        private CharacterController _characterController;
        /// <summary>
        /// 캐릭터의 수직 속도
        /// </summary>
        private float _verticalVelocity;
        /// <summary>
        /// 입력 유무를 확인하는 용도
        /// </summary>
        private bool _onTouching = false;
        /// <summary>
        /// 첫 입력값을 저장할 포지션을 저장한 변수
        /// </summary>
        private Vector3 _startPosition;
        /// <summary>
        /// 실시간으로 업데이트 받는 현재 입력값을 저장한 변수
        /// </summary>
        private Vector3 _currentPosition;
        /// <summary>
        /// 캐릭터가 살아있는지 체크용
        /// </summary>
       public bool isLive;
        /// <summary>
        /// 캐릭터 컨트롤러에 넣을 벡터값 저장용
        /// </summary>
        private Vector3 _moveVector;
        /// <summary>
        /// 캐릭터 컨트롤러에 넣을 점프용 벡터 값
        /// </summary>
        private Vector3 _jumpVector;
        /// <summary>
        /// 첫 입력과 실시간으로 바뀌는 입력값을 뺀 방향 벡터
        /// </summary>
        private Vector3 _direction;
        
        [SerializeField]private int _hpCount;
        /// <summary>
        /// 캐릭터가 땅에 닿았는지 체크하는 용도
        /// </summary>
        private bool isGrounded;
        /// <summary>
        /// 사망 시 부활 포인트 포지션 저장용 배열
        /// </summary>
        [SerializeField] private Transform[] _respawnPoint;

        [SerializeField]private State _currentState;
        private bool _invisible = false;
        private bool _isMove;
        [SerializeField]private bool _canAttack = false;
        //
        public bool _damaged;
        //
        Animator _animator;

        //처음 스타트용
        private float _startTime;
        //지속 시간
        [SerializeField]private float _durationTime;
        //지속 시간 갱신용
        private bool _whileDuration;
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            
            this.currentState = State.Idle;

            _animator = GetComponent<Animator>();
            
            _hpCount = 0;
        }
        private void Update()
        {
            //이동
           // MoveConditionCheck();
            CurrentPosition();
            switchStateUpdate(currentState);
            IsJump();
            StateCheck();

            //IsIdle();
            //IsLive();
            //IsMove();
        }
        /// <summary>
        /// 상태 변경시 업데이트용 함수
        /// </summary>
        /// <param name="state">현재 상태</param>
        public void switchStateUpdate(State state)
        {
            switch (state)
            {
                case State.Idle:
                    //Idle 애니메이션 추가
                    _animator.Play("IdleA");
                    break;//정지 상태일 때는 startposition을 다시 초기화 시켜준다. 
                case State.Move:
                    //Move 애니메이션 추가
                    MoveConditionCheck();
                    _animator.Play("Run");
                    break;//움직일 때는 애니메이션을 실행 하고 움직이는 함수를 실행
                case State.Attack:
                    //Attack 애니메이션 추가
                    //함수 딜레이 용으로 공격, 애니메이션 추가 (추후 수정요구)
                    Debug.Log("Animation play called");
                    //Attack();
                    break;//공격 할 때는 일정시간 동안 움직일 수 없고 일정시간 동안 공격할 수 없으며 일정 시간 후 움직일 수 있다.
                case State.Damage:
                    //Damage 애니메이션 추가
                    ++_hpCount;
                    _animator.Play("Damage");
                    break;//데미지를 입을 때 움직일 수 없고,이 곳에서 피격 판정을 실행하고 일정 시간동안 타격 당할 수 없으며 일정 시간 후 움직일 수 있다. 
                case State.Death:
                    //Death 애니메이션 추가
                    transform.position = _respawnPoint[0].position;
                    _animator.Play("DieA");
                    hpCount = 0;
                    Invoke("ResetIdle", 2f);
                    break;//현재 상태가 죽은 상태면 애니메이션을 실행하고 움직일 수 없고 피격당할 수 없고. 타격 할 수 없다. 리스폰 시간의 시간 후에 정해진 위치로 텔레포트 한다.
            }
        }
        /// <summary>
        /// 캐릭터의 점프를 담당하는 기능 함수
        /// </summary>
        private void Jump()
        {
            JumpVector();
            _characterController.Move(_jumpVector * Time.deltaTime);
        }
        /// <summary>
        /// 캐릭터의 전,후진을 담당하는 기능 함수
        /// </summary>
        private void Move()
        {
            MovingVector();
            _characterController.Move(_moveVector * Time.deltaTime);
        }
        /// <summary>
        /// 캐릭터를 회전하는 기능 함수
        /// </summary>
        private void Rotate()
        {
            CheckDirection();
            transform.localRotation *= Quaternion.Euler(0f, -_direction.normalized.x * _rotateSpeed * Time.deltaTime, 0f);
        }
        //1.일정시간동안 움직임이 멈추는 함수 
        //2.일정시간동안 타격이 안되는 함수
        //3.일정시간동안 피격이 안되는 함수

        //1.움직이는지 아닌지
        //2.공격하는지
        //3.살아있는지 죽어있는지

        private void StateCheck()
        {
            if (isLive)
            {
                if (!_damaged)
                {
                    StartCoroutine("CheckTime");
                    _damaged = true;
                    if (_whileDuration)
                    {
                        _currentState = State.Damage;
                    }
                }//데미지를 입었을 때
                else
                {
                    if (_canAttack)
                    {
                        _currentState = State.Attack;
                    }//공격이 가능할 때
                    else
                    {
                        if (_onTouching)
                        {
                            _currentState = State.Move;
                            return;
                        }//입력이 있을 때
                        else
                        {
                            _currentState = State.Idle;
                            return;
                        }//입력이 없을 때
                    }
                }
            }//살아있을 때
            else
            {
                _currentState = State.Death;
            }//죽어 있을 때
        }
        
        IEnumerator CheckTime()
        {
            
            while (true)
            {
                _whileDuration = true;
                _startTime = 0.0f;
                _startTime += Time.deltaTime;

                if (_startTime == _durationTime)
                {
                    break;
                }
                yield return Time.deltaTime;
            }
            _whileDuration = false;
            yield return null;
        }
        ///// <summary>
        ///// 피격이 안되는지 확인여부
        ///// </summary>
        //private void BeInvisible()
        //{
        //    _invisible = true;
        //}
        //private void ResetIdle()
        //{
        //    currentState = State.Idle;
        //    isLive = true;
        //}
        //private void IsIdle()
        //{
        //    _invisible = false;
        //    if (_onTouching || !isLive || _damaged)
        //    {
        //        return;
        //    }
        //    _currentState = State.Idle;
        //}
        ///// <summary>
        ///// 생존 여부 체크용 함수
        ///// </summary>
        //public void IsLive()
        //{
        //    if (hpCount == 2)
        //    {
        //        isLive = false;
        //        _currentState = State.Death;
        //    }
            
            
        //}
        ///// <summary>
        ///// 움직임 여부 체크용 함수
        ///// </summary>
        //private void IsMove()
        //{
        //    if (!_onTouching || _damaged ||!isLive)
        //    {
        //        return;
        //    }
        //    _currentState = State.Move;
        //}
        
        //public void IsAttack()
        //{
        //    _currentState = State.Attack;
        //}
        //public void IsDamaged()
        //{
        //    if(_invisible || _damaged || !isLive)
        //    {
        //        return;
        //    }
        //    _currentState = State.Damage;
        //}

        //private void Attack()
        //{
        //    if (!_canAttack) return;

        //    currentState = State.Attack;
        //    _canAttack = false;
        //    Invoke("AttackDelay", 1.0f);
        //}

        //private void AttackDelay()
        //{
        //    _canAttack = true;
        //    //currentState = State.Idle;
        //}

        /// <summary>
        /// 입력 할 벡터 값을 리턴해주는 함수
        /// </summary>
        private Vector3 InputVector(Vector3 input)
        {
            return new Vector3(input.x, input.y, input.z);
        }
        /// <summary>
        /// 현재 벡터 값을 구하는 함수
        /// </summary>
        private void CurrentPosition()
        {
            Vector3 tempInput = Vector3.zero;

#if UNITY_STANDALONE //PC에서는 마우스로 입력값을 받는다
            tempInput = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, -Camera.main.transform.position.z);

#elif UNITY_ANDROID //안드로이드 환경에서는 터치로 입력값을 받는다.

#endif
            _currentPosition = InputVector(tempInput);
        }
        /// <summary>
        /// 캐릭터의 전진 후진, 회전 , 점프등 전반적인 움직임을 관리하는 함수
        /// </summary>
        private void MoveConditionCheck()
        {
            Jump();
            Move();
            Rotate();
        }
        /// <summary>
        /// 전진 후진에 대입할 벡터값을 구하는 함수
        /// </summary>
        private void MovingVector()
        {
            Vector3 move = Vector3.zero;
            CheckDirection();
            move += (-(transform.right * _direction.x) + -(transform.forward * _direction.y)).normalized * _moveSpeed;
            _moveVector = InputVector(move);
            Debug.Log(move);
        }
        /// <summary>
        /// 점프할 때 대입할 벡터값을 구하는 함수
        /// </summary>
        private void JumpVector()
        {
            Vector3 move = Vector3.zero;
            if (isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = 0;
            }//땅에 캐릭터가 닿아 있을 때 수직 속도를 주지 않음
            else
            {
                _verticalVelocity -= _gravity * Time.deltaTime;
            } //캐릭터가 땅에서 떨어져 있을 때는 수직 속도에 중력을 적용함
            move.y = _verticalVelocity;
            _jumpVector = InputVector(move);
        }
        /// <summary>
        /// 첫 입력의 벡터값과 현재 입력의 벡터값을 비교하는 함수
        /// </summary>
        private Vector3 CheckDirection()
        {
            _direction = _startPosition - _currentPosition;
            return _direction; 
        }
        /// <summary>
        /// 점프를 뛸때 지상에 있는지 판단하는 함수 얘는 Update에서 지속적으로 체크를 해줘야함
        /// </summary>
        private void IsJump()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, groundLayer);
        }
        /// <summary>
        /// 수직 속도를 대입해주는 함수
        /// </summary>
        private void InputVerticalVelocity()
        {
            if (!isGrounded)
            {
                return;
            } //지상에 없을 때의 예외처리
            _verticalVelocity = _jumpPower;
        }
        /// <summary>
        /// 인풋 시스템의 점프 입력을 받으면 활성화 되는 이벤트 함수
        /// </summary>
        /// <param name="contexts"></param>
        public void OnJump(InputAction.CallbackContext contexts)
        {
            if (!isLive)
            {
                return;
            }//죽었을 때의 점프 예외
            //정확하게 눌렀을 때
            if (contexts.performed)
            {
                InputVerticalVelocity();
            }
        }
        /// <summary>
        /// 인풋시스템의 클릭 입력을 받으면 입력 받는지 체크하는 이벤트 함수
        /// </summary>
        /// <param name="context"></param>
        public void OnClick(InputAction.CallbackContext context)
        {
            //눌렀을 때
            if (context.started)
            {
                _onTouching = true;
                //클릭 시작 시 마우스 포지션의 xyz값을 지정한다.
                _startPosition = _currentPosition;
                //IsMove();

            }
            if (context.performed)
            {
            }
            //땠을 때
            if (context.canceled)
            {
                _onTouching = false;
                _direction = Vector3.zero;
            }
        }
    }
}

