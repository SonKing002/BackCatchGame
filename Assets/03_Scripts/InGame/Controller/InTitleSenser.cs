using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InTitleSensor : SingletonOfT<InTitleSensor>
{
    [SerializeField]
    //private Transform[] transform;


    private void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }
}
