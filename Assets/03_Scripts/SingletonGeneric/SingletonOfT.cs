using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̱��� �߻�ȭ �Լ� ,
/// <typeparam name="T"> �����ϴ� Ŭ���� �ڷ��� </typeparam>
public abstract class SingletonOfT<T> : MonoBehaviour
    where T : SingletonOfT<T>
{
    /// <summary>
    /// �ν��Ͻ� ���� ������Ƽ
    /// </summary>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            { 
                _instance = new GameObject(nameof(SingletonOfT<T>)).AddComponent<T>();//������ ����
            }
            return _instance;
        }
    }

    public static T _instance;//���κ���

    public virtual void Awake()
    {
        if (_instance != null)//�ٸ� ���� �����ϸ�
        { 
            if(_instance!= (T)this)
                Destroy(this.gameObject);//����
            return;
        }
        _instance = (T)this;
    }
}
