using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 싱글톤 추상화 함수 ,
/// <typeparam name="T"> 적용하는 클래스 자료형 </typeparam>
public abstract class SingletonOfT<T> : MonoBehaviour
    where T : SingletonOfT<T>
{
    /// <summary>
    /// 인스턴스 생성 프로퍼티
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            { 
                _instance = new GameObject(nameof(SingletonOfT<T>)).AddComponent<T>();//없으면 생성
            }
            return _instance;
        }
    }

    public static T _instance;//내부변수

    public virtual void Awake()
    {
        if (_instance != null)//다른 것이 존재하면
        { 
            if(_instance!= (T)this)
                Destroy(this.gameObject);//삭제
            return;
        }
        _instance = (T)this;
    }
}
