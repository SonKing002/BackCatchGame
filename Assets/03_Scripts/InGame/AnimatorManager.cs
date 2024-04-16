using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : SingletonOfT<AnimatorManager>
{

    //_state ĳ���� ���¸� enum������ ����
    //�߰��ϰ� ���� ���°� �ִٸ� enum�� �߰� �� case�� State.Case �������� �߰��ϸ� ��
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
