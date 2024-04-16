using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : SingletonOfT<AnimatorManager>
{

    //_state 캐릭터 상태를 enum형으로 만듬
    //추가하고 싶은 상태가 있다면 enum에 추가 후 case에 State.Case 형식으로 추가하면 됨
    public enum State
    {
        Idle, Move, Attack, Damage, Death
    }
    private void Awake()
    {
        Init();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
    }

}
