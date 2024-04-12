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



        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            //�̵�
            Move();
            //ȸ��
            Rotate(-_direction.normalized.x);
            //���� ���콺 ������ ����
            currentPostision();
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
            Debug.Log(input);
        }
        //ĳ������ �������� �����ϴ� �Լ�
        private void Move()
        {
            //�ٴڿ� ����ĳ��Ʈ�� ���.(���� Ȯ�ο�)
            isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, groundLayer);
            if (OnTouching)
            {
                //�̵��� ����
                _direction = _startPosition - _currentPosition;
                //�������� �ʱ�ȭ
                Vector3 move = Vector3.zero;
                //���� ������ ���� ���� ����
                move +=
                    (-(transform.right * _direction.x) + -(transform.forward * _direction.y)).normalized * _moveSpeed;
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

