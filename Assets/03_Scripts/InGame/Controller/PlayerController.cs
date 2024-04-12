using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
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
        private Vector2 _inputMoveValue;

        private Vector2 _touchStartposition;
        private Vector3 _touchChechStartPosition;
        private Vector2 _touchEndPosition;
        //�߷�
        [SerializeField] private float _gravity = -9.8f;
        //�÷��̾� �յ� ������ �ӵ�
        [SerializeField] private float _moveSpeed = 4.0f;
        //�÷��̾� ȸ�� �ӵ�
        [SerializeField] private float _rotateSpeed = 4.0f;
        //�÷��̾� ������
        [SerializeField] private float _jumpPower = 2.0f;
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
        //
        //private Vector3 _inputValue;
        //private Vector3 _inputValueY;
        //
        private Vector3 _startPosition;
        private Vector3 _currentPosition;
        private Vector3 _direction;



        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            //�̵�
            Move();
            //ȸ��
            Rotate(_direction.normalized.y);
            //���� ���콺 ������ 
            currentPostision();
        }
        
        private void currentPostision()
        {
            _currentPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            Debug.Log($"touchstartposition : {_startPosition}, currentposition{_currentPosition}");
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
            //Debug.Log("isGrounded: " + isGrounded);
            if (OnTouching)
            {
                //�̵��� ����
                _direction = _startPosition - _currentPosition;
                //�������� �ʱ�ȭ
                Vector3 move = Vector3.zero;
                //move +=
                //    ((transform.right * _inputMoveValue.x) + (transform.forward * _inputMoveValue.y)).normalized * _moveSpeed; 
                //���� ������ ���� ���� ����
                move +=
                    (-(transform.right * _direction.x) + -(transform.forward * _direction.y)).normalized * _moveSpeed;
                //Debug.Log(move);
                //���� ���� ���� �߷�X
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
        }
        private void Jump()
        {
            if (isGrounded)
            {
                verticalVelocity = _jumpPower;
            }
        }
        //public void OnMove(InputAction.CallbackContext context)
        //{
        //    _inputMoveValue = context.ReadValue<Vector2>();
        //}
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
            if (context.started)
            {
                if (OnTouching)
                {
                    return;
                }
                OnTouching = true;
                Debug.Log("������");
                //���콺 �������� xyz���� �����Ѵ�.
                _startPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
                Debug.Log($"touchstartposition : {_startPosition}, endposition{_currentPosition}");

            }
            if (context.performed)
            {
                Debug.Log($"OnTouching:{OnTouching}");
            }
            if (context.canceled)
            {
                OnTouching = false;
                Debug.Log($"touchstartposition : {_startPosition}, endposition{_currentPosition}");
                Debug.Log($"OnTouching:{OnTouching}");

                _direction = Vector3.zero;
            }
        }
    }
}

