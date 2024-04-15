using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    Animator _animator;
    private int _count;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _count = 0;
    }



}
