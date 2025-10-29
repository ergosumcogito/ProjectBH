using System;
using UnityEngine;

public static class RoundEvents
{
    public static Action<float> OnRoundStart;
    public static Action OnRoundEnd;
    public static Action OnRoundSurvived;
    public static Action OnRoundFailed;

    public static void Log(string message)
    {
        Debug.Log("[RoundEvent] " + message);
    }
}