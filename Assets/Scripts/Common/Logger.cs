using UnityEngine;
using System;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

public interface ILogger // 확장 예시
{
    void Info(string message);
    void Warning(string message);
    void Error(string message);

}

public static class Logger
{
    [Conditional("DEV_VER")]
    public static void Info(string message)
    {
        Debug.LogFormat("[{0}] {1}", DateTime.Now.ToString("yyy-mm-dd HH:mm:ss.fff"), message);
    }

    [Conditional("DEV_VER")]
    public static void Warning(string message)
    {
        Debug.LogWarningFormat("[{0}] {1}", DateTime.Now.ToString("yyy-mm-dd HH:mm:ss.fff"), message);
    }

    public static void Error(string message)
    {
        Debug.LogErrorFormat("[{0}] {1}", DateTime.Now.ToString("yyy-mm-dd HH:mm:ss.fff"), message);
    }
}
