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
        /// �÷��̾��� ü��ī��Ʈ
        /// </summary>
        public int hpCount { get => _hpCount; set => _hpCount = value; }
        /// <summary>
        /// ���� ���ɿ��� > �ܺο��� �����ؾ���
        /// </summary>
        public bool canAttack { get => _canAttack; set => _canAttack = value; }
        /// <summary>
        /// ���� ����
        /// </summary>
        public State currentState { get => _currentState; set { } }
        /// <summary>
        /// �ν����Ϳ��� ����� inputActionAsset
        /// </summary>
        public InputActionAsset inputActionAsset;
        /// <summary>
        /// �߷�
        /// </summary>
        [SerializeField] private float _gravity;
        /// <summary>
        /// �÷��̾� �յ� ������ �ӵ�
        /// </summary>
        [SerializeField] private float _moveSpeed;
        /// <summary>
        /// �÷��̾� ȸ�� �ӵ�
        /// </summary>
        [SerializeField] private float _rotateSpeed;
        /// <summary>
        /// �÷��̾� ������
        /// </summary>
        [SerializeField] private float _jumpPower;
        /// <summary>
        /// ���鿡 ����� üũ�ϱ� ���� �Ÿ�
        /// </summary>
        [SerializeField] private float _groundDistance;
        /// <summary>
        /// ����ĳ��Ʈ�� üũ�� ����� ���̾��ũ
        /// </summary>
        [SerializeField] private LayerMask groundLayer;
        /// <summary>
        /// ���� �� ���� �Ұ����� �ð�
        /// </summary>
        [SerializeField] private float _attackDelay;
        /// <summary>
        /// ĳ������ ĳ���� ��Ʈ�ѷ� ��ũ��Ʈ
        /// </summary>
        private CharacterController _characterController;
        /// <summary>
        /// ĳ������ ���� �ӵ�
        /// </summary>
        private float _verticalVelocity;
        /// <summary>
        /// �Է� ������ Ȯ���ϴ� �뵵
        /// </summary>
        private bool _onTouching = false;
        /// <summary>
        /// ù �Է°��� ������ �������� ������ ����
        /// </summary>
        private Vector3 _startPosition;
        /// <summary>
        /// �ǽð����� ������Ʈ �޴� ���� �Է°��� ������ ����
        /// </summary>
        private Vector3 _currentPosition;
        /// <summary>
        /// ĳ���Ͱ� ����ִ��� üũ��
        /// </summary>
       public bool isLive;
        /// <summary>
        /// ĳ���� ��Ʈ�ѷ��� ���� ���Ͱ� �����
        /// </summary>
        private Vector3 _moveVector;
        /// <summary>
        /// ĳ���� ��Ʈ�ѷ��� ���� ������ ���� ��
        /// </summary>
        private Vector3 _jumpVector;
        /// <summary>
        /// ù �Է°� �ǽð����� �ٲ�� �Է°��� �� ���� ����
        /// </summary>
        private Vector3 _direction;
        
        [SerializeField]private int _hpCount;
        /// <summary>
        /// ĳ���Ͱ� ���� ��Ҵ��� üũ�ϴ� �뵵
        /// </summary>
        private bool isGrounded;
        /// <summary>
        /// ��� �� ��Ȱ ����Ʈ ������ ����� �迭
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

        //ó�� ��ŸƮ��
        private float _startTime;
        //���� �ð�
        [SerializeField]private float _durationTime;
        //���� �ð� ���ſ�
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
            //�̵�
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
        /// ���� ����� ������Ʈ�� �Լ�
        /// </summary>
        /// <param name="state">���� ����</param>
        public void switchStateUpdate(State state)
        {
            switch (state)
            {
                case State.Idle:
                    //Idle �ִϸ��̼� �߰�
                    _animator.Play("IdleA");
                    break;//���� ������ ���� startposition�� �ٽ� �ʱ�ȭ �����ش�. 
                case State.Move:
                    //Move �ִϸ��̼� �߰�
                    MoveConditionCheck();
                    _animator.Play("Run");
                    break;//������ ���� �ִϸ��̼��� ���� �ϰ� �����̴� �Լ��� ����
                case State.Attack:
                    //Attack �ִϸ��̼� �߰�
                    //�Լ� ������ ������ ����, �ִϸ��̼� �߰� (���� �����䱸)
                    Debug.Log("Animation play called");
                    //Attack();
                    break;//���� �� ���� �����ð� ���� ������ �� ���� �����ð� ���� ������ �� ������ ���� �ð� �� ������ �� �ִ�.
                case State.Damage:
                    //Damage �ִϸ��̼� �߰�
                    ++_hpCount;
                    _animator.Play("Damage");
                    break;//�������� ���� �� ������ �� ����,�� ������ �ǰ� ������ �����ϰ� ���� �ð����� Ÿ�� ���� �� ������ ���� �ð� �� ������ �� �ִ�. 
                case State.Death:
                    //Death �ִϸ��̼� �߰�
                    transform.position = _respawnPoint[0].position;
                    _animator.Play("DieA");
                    hpCount = 0;
                    Invoke("ResetIdle", 2f);
                    break;//���� ���°� ���� ���¸� �ִϸ��̼��� �����ϰ� ������ �� ���� �ǰݴ��� �� ����. Ÿ�� �� �� ����. ������ �ð��� �ð� �Ŀ� ������ ��ġ�� �ڷ���Ʈ �Ѵ�.
            }
        }
        /// <summary>
        /// ĳ������ ������ ����ϴ� ��� �Լ�
        /// </summary>
        private void Jump()
        {
            JumpVector();
            _characterController.Move(_jumpVector * Time.deltaTime);
        }
        /// <summary>
        /// ĳ������ ��,������ ����ϴ� ��� �Լ�
        /// </summary>
        private void Move()
        {
            MovingVector();
            _characterController.Move(_moveVector * Time.deltaTime);
        }
        /// <summary>
        /// ĳ���͸� ȸ���ϴ� ��� �Լ�
        /// </summary>
        private void Rotate()
        {
            CheckDirection();
            transform.localRotation *= Quaternion.Euler(0f, -_direction.normalized.x * _rotateSpeed * Time.deltaTime, 0f);
        }
        //1.�����ð����� �������� ���ߴ� �Լ� 
        //2.�����ð����� Ÿ���� �ȵǴ� �Լ�
        //3.�����ð����� �ǰ��� �ȵǴ� �Լ�

        //1.�����̴��� �ƴ���
        //2.�����ϴ���
        //3.����ִ��� �׾��ִ���

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
                }//�������� �Ծ��� ��
                else
                {
                    if (_canAttack)
                    {
                        _currentState = State.Attack;
                    }//������ ������ ��
                    else
                    {
                        if (_onTouching)
                        {
                            _currentState = State.Move;
                            return;
                        }//�Է��� ���� ��
                        else
                        {
                            _currentState = State.Idle;
                            return;
                        }//�Է��� ���� ��
                    }
                }
            }//������� ��
            else
            {
                _currentState = State.Death;
            }//�׾� ���� ��
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
        ///// �ǰ��� �ȵǴ��� Ȯ�ο���
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
        ///// ���� ���� üũ�� �Լ�
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
        ///// ������ ���� üũ�� �Լ�
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
        /// �Է� �� ���� ���� �������ִ� �Լ�
        /// </summary>
        private Vector3 InputVector(Vector3 input)
        {
            return new Vector3(input.x, input.y, input.z);
        }
        /// <summary>
        /// ���� ���� ���� ���ϴ� �Լ�
        /// </summary>
        private void CurrentPosition()
        {
            Vector3 tempInput = Vector3.zero;

#if UNITY_STANDALONE //PC������ ���콺�� �Է°��� �޴´�
            tempInput = new Vector3(UnityEngine.Input.mousePosition.x, UnityEngine.Input.mousePosition.y, -Camera.main.transform.position.z);

#elif UNITY_ANDROID //�ȵ���̵� ȯ�濡���� ��ġ�� �Է°��� �޴´�.

#endif
            _currentPosition = InputVector(tempInput);
        }
        /// <summary>
        /// ĳ������ ���� ����, ȸ�� , ������ �������� �������� �����ϴ� �Լ�
        /// </summary>
        private void MoveConditionCheck()
        {
            Jump();
            Move();
            Rotate();
        }
        /// <summary>
        /// ���� ������ ������ ���Ͱ��� ���ϴ� �Լ�
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
        /// ������ �� ������ ���Ͱ��� ���ϴ� �Լ�
        /// </summary>
        private void JumpVector()
        {
            Vector3 move = Vector3.zero;
            if (isGrounded && _verticalVelocity < 0)
            {
                _verticalVelocity = 0;
            }//���� ĳ���Ͱ� ��� ���� �� ���� �ӵ��� ���� ����
            else
            {
                _verticalVelocity -= _gravity * Time.deltaTime;
            } //ĳ���Ͱ� ������ ������ ���� ���� ���� �ӵ��� �߷��� ������
            move.y = _verticalVelocity;
            _jumpVector = InputVector(move);
        }
        /// <summary>
        /// ù �Է��� ���Ͱ��� ���� �Է��� ���Ͱ��� ���ϴ� �Լ�
        /// </summary>
        private Vector3 CheckDirection()
        {
            _direction = _startPosition - _currentPosition;
            return _direction; 
        }
        /// <summary>
        /// ������ �۶� ���� �ִ��� �Ǵ��ϴ� �Լ� ��� Update���� ���������� üũ�� �������
        /// </summary>
        private void IsJump()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, groundLayer);
        }
        /// <summary>
        /// ���� �ӵ��� �������ִ� �Լ�
        /// </summary>
        private void InputVerticalVelocity()
        {
            if (!isGrounded)
            {
                return;
            } //���� ���� ���� ����ó��
            _verticalVelocity = _jumpPower;
        }
        /// <summary>
        /// ��ǲ �ý����� ���� �Է��� ������ Ȱ��ȭ �Ǵ� �̺�Ʈ �Լ�
        /// </summary>
        /// <param name="contexts"></param>
        public void OnJump(InputAction.CallbackContext contexts)
        {
            if (!isLive)
            {
                return;
            }//�׾��� ���� ���� ����
            //��Ȯ�ϰ� ������ ��
            if (contexts.performed)
            {
                InputVerticalVelocity();
            }
        }
        /// <summary>
        /// ��ǲ�ý����� Ŭ�� �Է��� ������ �Է� �޴��� üũ�ϴ� �̺�Ʈ �Լ�
        /// </summary>
        /// <param name="context"></param>
        public void OnClick(InputAction.CallbackContext context)
        {
            //������ ��
            if (context.started)
            {
                _onTouching = true;
                //Ŭ�� ���� �� ���콺 �������� xyz���� �����Ѵ�.
                _startPosition = _currentPosition;
                //IsMove();

            }
            if (context.performed)
            {
            }
            //���� ��
            if (context.canceled)
            {
                _onTouching = false;
                _direction = Vector3.zero;
            }
        }
    }
}

