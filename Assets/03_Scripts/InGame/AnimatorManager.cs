using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : SingletonOfT<AnimatorManager>
{

    //_state 캐릭터 상태를 enum형으로 만듬
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
