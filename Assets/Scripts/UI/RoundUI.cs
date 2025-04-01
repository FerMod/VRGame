using Garitto;
using System;
using TMPro;
using UnityEngine;

public class RoundUI : MonoBehaviour
{
    public TMP_Text elapsedTime;
    public TMP_Text elapsedTimePercentage;

    public void UpdateElapsedTime(double elapsedTime)
    {
        this.elapsedTime.text = TimeSpan.FromSeconds(elapsedTime).ToString(@"hh\:mm");
    }
}
