using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //������Ƽ ù���� �빮��
    //private _�ҹ���
    //public �� ������
    //�Լ��� �빮��
    //�ν����Ϳ��� ����� inputActionAsset
    public InputActionAsset inputActionAsset;
    //�Է°��� ����
    private Vector2 _inputValue;
    //�����Է� ���� ����
    private float _inputJumpValue;
    //RigidBody
    private Rigidbody _rigidBody;
    private float _gravity;
    //�÷��̾� �յ� ������ �ӵ�
    [SerializeField] private float _moveSpeed = 4.0f;
    //�÷��̾� ȸ�� �ӵ�
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

    //�̵� �ÿ� ����� ������Ʈ �Լ� ���� ������ �����ϴ� �Լ��� FixedUpdate�� �߰�����
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

    //�¿��� ȸ���� �����ϴ� �Լ�.
    private void Rotate(float input)
    {
        transform.localRotation *= Quaternion.Euler(0f, input * _rotateSpeed * Time.deltaTime, 0f);
    }
    //�� ���� �������� �����ϴ� �Լ�
    private void Move(float input)
    {

        //TODO rigidbody �������
        Vector3 newPosition = _rigidBody.position + (transform.forward * input * _moveSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        //TODO rigidbody ���� �����
    }
    //��ǲ �׼����� OnMove���� �Է°��� �޴´�.
    public void OnMove(InputAction.CallbackContext context)
    {
        _inputValue = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext contexts)
    {
        Debug.Log("���� �������~");
        _inputJumpValue = contexts.ReadValue<float>();
    }
}
