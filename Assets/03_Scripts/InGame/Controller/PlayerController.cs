using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using static AnimatorManager;
namespace MJ.Player
{
    public class PlayerController : MonoBehaviour
    {
        //������Ƽ ù���� �빮��
        //private _�ҹ���
        //public �� ������
        //�Լ��� �빮��
        //�ν����Ϳ��� ����� inputActionAsset
        public InputActionAsset inputActionAsset;
        //�Է°��� ����
        //�߷�
        [SerializeField] private float _gravity;
        //�÷��̾� �յ� ������ �ӵ�
        [SerializeField] private float _moveSpeed;
        //�÷��̾� ȸ�� �ӵ�
        [SerializeField] private float _rotateSpeed;
        //�÷��̾� ������
        [SerializeField] private float _jumpPower;
        //���鿡 ��� �Ÿ�
        [SerializeField] private float _groundDistance;
        private bool isGrounded;
        [SerializeField] private LayerMask groundLayer;
        //ĳ���� ��Ʈ�ѷ�
        private CharacterController _characterController;
        //���� �߷�
        private float verticalVelocity;
        //OnTouch pc�׽�Ʈ�� TODO ���� �ʿ�
        private bool OnTouching = false;
        //Ŭ�� �� �� ������ �������� ������ ��
        private Vector3 _startPosition;
        //�ǽð����� ������Ʈ �޴� ���� ���콺 ��ġ�� ������ ��
        private Vector3 _currentPosition;
        //�� �� ���Ͱ��� �� ���Ͱ��� ������ ��
        private Vector3 _direction;
        //���� ��������
        public State currentState;

        //
        public bool _damaged;
        //
        Animator _animator;



        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            this.currentState = State.Idle;
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            
        }
        private void Update()
        {
            //�̵�
            Move();
            //ȸ��
            Rotate(-_direction.normalized.x);
            //���� ���콺 ������ ����
            currentPostision();
            switchUpdate(currentState);
        }
        public void switchUpdate(State state)
        {
            switch (state)
            {
                case State.Idle:
                    //Idle �ִϸ��̼� �߰�
                    _animator.Play("IdleA");
                    break;
                case State.Move:
                    //Move �ִϸ��̼� �߰�
                    _animator.Play("Run");
                    break;
                case State.Attack:
                    //Attack �ִϸ��̼� �߰�
                    break;
                case State.Damage:
                    //Damage �ִϸ��̼� �߰�
                    _animator.Play("Damage");
                    break;
                case State.Death:
                    //Death �ִϸ��̼� �߰�
                    break;
            }
        }
        private void currentPostision()
        {
            //���콺 ������
            _currentPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        }
        //�¿��� ȸ���� �����ϴ� �Լ�.
        private void Rotate(float input)
        {
            transform.localRotation *= Quaternion.Euler(0f, input * _rotateSpeed * Time.deltaTime, 0f);
        }
        //ĳ������ �������� �����ϴ� �Լ�
        private void Move()
        {
            //�ٴڿ� ����ĳ��Ʈ�� ���.(���� Ȯ�ο�)
            isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, groundLayer);
            Vector3 move = Vector3.zero;
            
            if (!OnTouching)
            {
                if(!_damaged)
                this.currentState = State.Idle;
            }
            else
            {
                if(!_damaged)
                this.currentState = State.Move;

                _direction = _startPosition - _currentPosition;
                //���� ������ ���� ���� ����
                move +=
                    (-(transform.right * _direction.x) + -(transform.forward * _direction.y)).normalized * _moveSpeed;
            }
            
            if (isGrounded && verticalVelocity < 0)
            {
                verticalVelocity = 0;
            }
            else
            {
                //���� �� ���� �߷� ����
                verticalVelocity -= _gravity * Time.deltaTime;
            }
            move.y = verticalVelocity;
            //ĳ���� �̵� �Լ�
            _characterController.Move(move * Time.deltaTime);
        }
        private void Jump()
        {
            if (isGrounded)
            {
                verticalVelocity = _jumpPower;
            }
        }
        public void OnJump(InputAction.CallbackContext contexts)
        {
            //��Ȯ�ϰ� ������ ��
            if (contexts.performed)
            {
                Jump();
            }
        }
        public void OnClick(InputAction.CallbackContext context)
        {
            //������ ��
            if (context.started)
            {
                if (OnTouching)
                {
                    return;
                }
                OnTouching = true;
                //Ŭ�� ���� �� ���콺 �������� xyz���� �����Ѵ�.
                _startPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            }
            //���� ��
            if (context.canceled)
            {
                OnTouching = false;
                //direction���� �ʱ�ȭ ������
                _direction = Vector3.zero;
            }
        }
    }
}

