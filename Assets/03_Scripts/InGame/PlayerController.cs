using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

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
    //�߷�
    [SerializeField]private float _gravity = -9.8f;
    //�÷��̾� �յ� ������ �ӵ�
    [SerializeField] private float _moveSpeed = 4.0f;
    //�÷��̾� ȸ�� �ӵ�
    [SerializeField] private float _rotateSpeed = 4.0f;
    //�÷��̾� ������
    [SerializeField] private float _jumpPower = 2.0f;
    //���鿡 ��� �Ÿ�
    [SerializeField] private float _groundDistance;
    private bool isGrounded;
    [SerializeField]private LayerMask groundLayer;
    //ĳ���� ��Ʈ�ѷ�
    private CharacterController _characterController;
    //���� �߷�
    private float verticalVelocity;

    Vector3 moveDirection;

    private void Awake()
    {   
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
        Rotate(_inputMoveValue.x);
        Debug.DrawRay(transform.position, Vector3.down, Color.red, _groundDistance);
    }

    //�¿��� ȸ���� �����ϴ� �Լ�.
    private void Rotate(float input)
    {
        transform.localRotation *= Quaternion.Euler(0f, input * _rotateSpeed * Time.deltaTime, 0f);
    }
    //�� ���� �������� �����ϴ� �Լ�
    private void Move()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, groundLayer);
        Debug.Log("isGrounded: " + isGrounded);

        Vector3 move = new Vector3(_inputMoveValue.x, 0, _inputMoveValue.y) * _moveSpeed;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0;  
        }
        else
        {
            verticalVelocity -= _gravity * Time.deltaTime;
        }
        move.y = verticalVelocity;
        _characterController.Move(move * Time.deltaTime);
    }
    private void Jump()
    {
        if (isGrounded)
        {
            verticalVelocity = _jumpPower;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputMoveValue = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext contexts)
    {
        //��Ȯ�ϰ� ������ ��
        if (contexts.performed)
        {
            Jump();
        }
    }
}
