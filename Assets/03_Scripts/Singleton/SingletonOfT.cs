using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̱��� �߻�ȭ �Լ� ,
/// <typeparam name="T"> �����ϴ� Ŭ���� �ڷ��� </typeparam>
public abstract class SingletonOfT<T> : MonoBehaviour
    where T : MonoBehaviour
{
    /// <summary>
    /// �ν��Ͻ� ���� ������Ƽ
    /// </summary>
    public static SingletonOfT<T> Instance
    {
        get
        {
            if (_instance == null)
            { 
                _instance = new GameObject(nameof(SingletonOfT<T>)).AddComponent<SingletonOfT<T>>();//������ ����
            }
            return _instance;
        }
    }

    public static SingletonOfT<T> _instance;//���κ���

    public virtual void Awake()
    {
        if (_instance != this)//�ٸ� ���� �����ϸ�
        { 
            Destroy(this);//����
        }

        DontDestroyOnLoad(this);//����
    }
}
