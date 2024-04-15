using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DependencySource : SingletonOfT<DependencySource>
{
    [Header("유저 정보")]
    private string _nickname;
  
}
