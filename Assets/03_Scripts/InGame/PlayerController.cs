using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //프로퍼티 첫문자 대문자
    //private _소문자
    //public 걍 쓰세요
    //함수는 대문자
    //인스펙터에서 등록할 inputActionAsset
    public InputActionAsset inputActionAsset;
    //입력값을 저장
    private Vector2 _inputValue;
    //점프입력 값을 저장
    private float _inputJumpValue;
    //RigidBody
    private Rigidbody _rigidBody;
    private float _gravity;
    //플레이어 앞뒤 움직임 속도
    [SerializeField] private float _moveSpeed = 4.0f;
    //플레이어 회전 속도
    [SerializeField] private float _rotateSpeed = 4.0f;
    [SerializeField] private float _jumpPower = 2.0f;
    [SerializeField] private float _groundDistance;
    private bool isGrounded;
    [SerializeField]private LayerMask groundLayer;
    Transform _transform;
    private void Awake()
    {   
        _transform = this.transform;
        _rigidBody = GetComponent<Rigidbody>();
    }

    //이동 시에 사용할 업데이트 함수 물리 연산을 포함하는 함수라 FixedUpdate에 추가했음
    private void FixedUpdate()
    {
        Move(_inputValue.y);
        Rotate(_inputValue.x);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, _groundDistance);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, groundLayer);
        if (isGrounded)
        {
            Jump();
        }
        
    }

    //좌우의 회전을 관리하는 함수.
    private void Rotate(float input)
    {
        transform.localRotation *= Quaternion.Euler(0f, input * _rotateSpeed * Time.deltaTime, 0f);
    }
    //앞 뒤의 움직임을 관리하는 함수
    private void Move(float input)
    {

        //TODO rigidbody 빼고만들기
        Vector3 newPosition = _rigidBody.position + (transform.forward * input * _moveSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        //TODO rigidbody 빼고 만들기
    }
    //인풋 액션으로 OnMove값의 입력값을 받는다.
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputValue = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext contexts)
    {
        Debug.Log("점프 눌렀어요~");
        _inputJumpValue = contexts.ReadValue<float>();
    }
}
