using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public delegate void Score();
    public static event Score OnScore;

    public static void OnScoreMethod()
    {
        if (OnScore != null)
            OnScore();
    }

    public delegate void Shoot(int spawn_size);
    public static event Shoot OnShoot;

    public static void OnShootMethod(int spawn_size)
    {
        if (OnShoot != null)
            OnShoot(spawn_size);
    }

    public delegate void EndLevel();
    public static event EndLevel OnEndLevel;

    public static void OnEndLevelMethod()
    {
        if (OnEndLevel != null)
            OnEndLevel();
    }

    public delegate void NextLevel(int level);
    public static event NextLevel OnNextLevel;

    public static void OnNextLevelMethod(int level)
    {
        if (OnNextLevel != null)
            OnNextLevel(level);
    }

    public delegate void WaitFall();
    public static event WaitFall OnWaitFall;

    public static void WaitFallMethod()
    {
        if (OnWaitFall != null)
            OnWaitFall();
    }
}
