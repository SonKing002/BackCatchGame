using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : SingletonOfT<AnimatorManager>
{

    //_state ĳ���� ���¸� enum������ ����
    private enum _state
    {
        Idle, Move, Attack, Damage, Death
    }
    Animator _animator;

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(this);
        _animator = GetComponent<Animator>();
    }


}
