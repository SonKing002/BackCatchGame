using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
namespace MJ.Player
{
    public class PlayerController : MonoBehaviour
    {
        //프로퍼티 첫문자 대문자
        //private _소문자
        //public 걍 쓰세요
        //함수는 대문자
        //인스펙터에서 등록할 inputActionAsset
        public InputActionAsset inputActionAsset;
        //입력값을 저장
        private Vector2 _inputMoveValue;

        private Vector2 _touchStartposition;
        private Vector3 _touchChechStartPosition;
        private Vector2 _touchEndPosition;
        //중력
        [SerializeField] private float _gravity = -9.8f;
        //플레이어 앞뒤 움직임 속도
        [SerializeField] private float _moveSpeed = 4.0f;
        //플레이어 회전 속도
        [SerializeField] private float _rotateSpeed = 4.0f;
        //플레이어 점프력
        [SerializeField] private float _jumpPower = 2.0f;
        //지면에 닿는 거리
        [SerializeField] private float _groundDistance;
        private bool isGrounded;
        [SerializeField] private LayerMask groundLayer;
        //캐릭터 컨트롤러
        private CharacterController _characterController;
        //수평 중력
        private float verticalVelocity;
        //OnTouch pc테스트용 TODO 변경 필요
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
            //이동
            Move();
            //회전
            Rotate(_direction.normalized.y);
            //현재 마우스 포지션 
            currentPostision();
        }
        
        private void currentPostision()
        {
            _currentPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            Debug.Log($"touchstartposition : {_startPosition}, currentposition{_currentPosition}");
        }
        //좌우의 회전을 관리하는 함수.
        private void Rotate(float input)
        {
            transform.localRotation *= Quaternion.Euler(0f, input * _rotateSpeed * Time.deltaTime, 0f);
        }
        //캐릭터의 움직임을 관리하는 함수
        private void Move()
        {
            //바닥에 레이캐스트를 쏜다.(지상 확인용)
            isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, groundLayer);
            //Debug.Log("isGrounded: " + isGrounded);
            if (OnTouching)
            {
                //이동할 방향
                _direction = _startPosition - _currentPosition;
                //지역변수 초기화
                Vector3 move = Vector3.zero;
                //move +=
                //    ((transform.right * _inputMoveValue.x) + (transform.forward * _inputMoveValue.y)).normalized * _moveSpeed; 
                //지역 변수에 방향 값을 넣음
                move +=
                    (-(transform.right * _direction.x) + -(transform.forward * _direction.y)).normalized * _moveSpeed;
                //Debug.Log(move);
                //점프 안할 때는 중력X
                if (isGrounded && verticalVelocity < 0)
                {
                    verticalVelocity = 0;
                }
                else
                {
                    //점프 할 때는 중력 포함
                    verticalVelocity -= _gravity * Time.deltaTime;
                }
                move.y = verticalVelocity;
                //캐릭터 이동 함수
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
            //정확하게 눌렀을 때
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
                Debug.Log("시작함");
                //마우스 포지션의 xyz값을 지정한다.
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

