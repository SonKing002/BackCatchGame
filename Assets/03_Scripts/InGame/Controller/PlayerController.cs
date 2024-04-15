using System;
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
        public int hpCount { get => _count; set => _count = value; }
        public bool canAttack { get => _canAttack; set => _canAttack = value; }
        //ÇÁ·ÎÆÛÆ¼ Ã¹¹®ÀÚ ¼Ò¹®ÀÚ
        //private _¼Ò¹®ÀÚ
        //public °Á ¾²¼¼¿ä
        //ÇÔ¼ö´Â ´ë¹®ÀÚ
        //ÀÎ½ºÆåÅÍ¿¡¼­ µî·ÏÇÒ inputActionAsset
        public InputActionAsset inputActionAsset;
        //ÀÔ·Â°ªÀ» ÀúÀå
        //Áß·Â
        [SerializeField] private float _gravity;
        //ÇÃ·¹ÀÌ¾î ¾ÕµÚ ¿òÁ÷ÀÓ ¼Óµµ
        [SerializeField] private float _moveSpeed;
        //ÇÃ·¹ÀÌ¾î È¸Àü ¼Óµµ
        [SerializeField] private float _rotateSpeed;
        //ÇÃ·¹ÀÌ¾î Á¡ÇÁ·Â
        [SerializeField] private float _jumpPower;
        //Áö¸é¿¡ ´ê´Â °Å¸®
        [SerializeField] private float _groundDistance;
        [SerializeField] private LayerMask groundLayer;
        //°ø°Ý µô·¹ÀÌ ¼±¾ð
        [SerializeField] private float _attackDelay;
        //°ø°Ý °¡´ÉÇÑÁö ¼±¾ð
        //Ä³¸¯ÅÍ ÄÁÆ®·Ñ·¯
        private CharacterController _characterController;
        //¼öÆò Áß·Â
        private float verticalVelocity;
        //OnTouch pcÅ×½ºÆ®¿ë TODO º¯°æ ÇÊ¿ä
        private bool OnTouching = false;
        //Å¬¸¯ ÇÒ ¶§ ÀúÀåÇÒ Æ÷Áö¼ÇÀ» ÀúÀåÇÒ °÷
        private Vector3 _startPosition;
        //½Ç½Ã°£À¸·Î ¾÷µ¥ÀÌÆ® ¹Þ´Â ÇöÀç ¸¶¿ì½º À§Ä¡¸¦ ÀúÀåÇÒ °÷
        private Vector3 _currentPosition;
        //±× µÎ º¤ÅÍ°ªÀ» »« º¤ÅÍ°ªÀ» ÀúÀåÇÒ °÷
        private Vector3 _direction;
        //»óÅÂ °¡Á®¿À±â
        public State currentState;
        private int _count;
        private bool isGrounded;
        
        private bool _canAttack = true;

        //
        public bool _damaged;
        //
        Animator _animator;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            this.currentState = State.Idle;
            _animator = GetComponent<Animator>();
            hpCount = 0;
        }

        private void Start()
        {
            
        }
        private void Update()
        {
            if (currentState == State.Death)
            {
                return;
            }
            //ÀÌµ¿
            Move();
            //È¸Àü
            Rotate(-_direction.normalized.x);
            //ÇöÀç ¸¶¿ì½º Æ÷Áö¼Ç °»½Å
            currentPostision();
            switchUpdate(currentState);
        }
        public void switchUpdate(State state)
        {
            switch (state)
            {
                case State.Idle:
                    //Idle ¾Ö´Ï¸ÞÀÌ¼Ç Ãß°¡
                    _animator.Play("IdleA");
                    break;
                case State.Move:
                    //Move ¾Ö´Ï¸ÞÀÌ¼Ç Ãß°¡
                    _animator.Play("Run");
                    break;
                case State.Attack:
                    //Attack ¾Ö´Ï¸ÞÀÌ¼Ç Ãß°¡
                    //ÇÔ¼ö µô·¹ÀÌ ¿ëÀ¸·Î °ø°Ý, ¾Ö´Ï¸ÞÀÌ¼Ç Ãß°¡ (ÃßÈÄ ¼öÁ¤¿ä±¸)
                    Debug.Log("Animation play called");
                    Attack();
                    break;
                case State.Damage:
                    //Damage ¾Ö´Ï¸ÞÀÌ¼Ç Ãß°¡
                    _animator.Play("Damage");
                    break;
                case State.Death:
                    //Death ¾Ö´Ï¸ÞÀÌ¼Ç Ãß°¡
                    _animator.Play("DieA");
                    break;
            }
        }

        private void Attack()
        {
            if (!_canAttack) return;

            Debug.Log("ÇÔ¼ö È£­ƒµÊ;; play called");
            _animator.Play("ATK1");
            currentState = State.Attack;
            _canAttack = false;
            Invoke("AttackDelay", 1.0f);
        }

        private void AttackDelay()
        {
            _canAttack = true;
            currentState = State.Idle;
        }

        private void currentPostision()
        {
            //¸¶¿ì½º Æ÷Áö¼Ç
            _currentPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        }
        //ÁÂ¿ìÀÇ È¸ÀüÀ» °ü¸®ÇÏ´Â ÇÔ¼ö. ¿ª½Ã Á×¾úÀ» ¶§´Â ¿òÁ÷ÀÌ¸é ¾ÈµÊ~
        private void Rotate(float input)
        {
            if (currentState == State.Death) return;
            transform.localRotation *= Quaternion.Euler(0f, input * _rotateSpeed * Time.deltaTime, 0f);
        }
        //Ä³¸¯ÅÍÀÇ ¿òÁ÷ÀÓÀ» °ü¸®ÇÏ´Â ÇÔ¼ö
        private void Move()
        {
            //Á×¾úÀ» ¶§´Â ¿òÁ÷ÀÌ¸é ¾ÈµÊ~
            if (currentState == State.Death) return;

               //¹Ù´Ú¿¡ ·¹ÀÌÄ³½ºÆ®¸¦ ½ð´Ù.(Áö»ó È®ÀÎ¿ë)
               isGrounded = Physics.Raycast(transform.position, Vector3.down, _groundDistance, groundLayer);
            Vector3 move = Vector3.zero;

            if (isGrounded && verticalVelocity < 0)
            {
                verticalVelocity = 0;
            }
            else
            {
                //Á¡ÇÁ ÇÒ ¶§´Â Áß·Â Æ÷ÇÔ
                verticalVelocity -= _gravity * Time.deltaTime;
            }
            move.y = verticalVelocity;
            //Ä³¸¯ÅÍ ÀÌµ¿ ÇÔ¼ö
            

            if (!OnTouching)
            {
                //µ¥¹ÌÁö¸¦ ÀÔ¾ú°Å³ª °ø°ÝÇÒ¶§´Â  Idle»óÅÂ°¡ ¾Æ´Ï°Ô Ã¼Å© 
                if (currentState == State.Attack || currentState == State.Damage) return;
                this.currentState = State.Idle;
            }
            else
            {
                _direction = _startPosition - _currentPosition;
                //Áö¿ª º¯¼ö¿¡ ¹æÇâ °ªÀ» ³ÖÀ½
                move +=
                    (-(transform.right * _direction.x) + -(transform.forward * _direction.y)).normalized * _moveSpeed;
                if (currentState == State.Attack || currentState == State.Damage) return;
                this.currentState = State.Move;
            }
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
            //Á¤È®ÇÏ°Ô ´­·¶À» ¶§
            if (contexts.performed)
            {
                Jump();
            }
        }
        public void OnClick(InputAction.CallbackContext context)
        {
            //´­·¶À» ¶§
            if (context.started)
            {
                if (OnTouching)
                {
                    return;
                }
                OnTouching = true;
                //Å¬¸¯ ½ÃÀÛ ½Ã ¸¶¿ì½º Æ÷Áö¼ÇÀÇ xyz°ªÀ» ÁöÁ¤ÇÑ´Ù.
                _startPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            }
            //¶®À» ¶§
            if (context.canceled)
            {
                OnTouching = false;
                //direction°ªÀ» ÃÊ±âÈ­ ½ÃÄÑÁÜ
                _direction = Vector3.zero;
            }
        }
    }
}

