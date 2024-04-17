using UnityEngine;

// 자주 사용하는 기능을 모아두는 유틸리티 클래스.


public class Utils
{
    // 비교할 문자열이 빈 문자열(또는 Null) 또는 공란이면 true 값이 있으면 false.
    public static bool IsStringValid(string compareString)
    {
        return (!string.IsNullOrEmpty(compareString) && !string.IsNullOrWhiteSpace(compareString));
    }

    // 로그 함수.
    public static void Log(object message)
    {
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }

    public static void LogRed(object message)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=red>{message}</color>");
#endif
    }

    public static void LogGreen(object message)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=green>{message}</color>");
#endif
    }

    public static void LogBlue(object message)
    {
#if UNITY_EDITOR
        Debug.Log($"<color=blue>{message}</color>");
#endif
    }
}