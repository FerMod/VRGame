using System;
using UnityEditor;
using UnityEngine;

namespace Garitto
{
    [RequireComponent(typeof(RoundController))]
    public class RoundManager : MonoBehaviour
    {
        public RoundUI roundUI;
        private RoundController roundController;

        [Range(0, 23)]
        public int hours;

        [Range(0, 59)]
        public int minutes;

        [Range(0, 59)]
        public int seconds;

        public TimeSpan StartTimeStamp { get => new(hours, minutes, seconds); }

        void Start()
        {
            roundController = GetComponent<RoundController>();

            roundController.StartRound();
            roundController.OnRoundTick += UpdateTimeText;

            UpdateTimeText();
        }

        private void UpdateTimeText()
        {
            roundUI.UpdateElapsedTime(StartTimeStamp.TotalSeconds + roundController.ElapsedTime);
            roundUI.elapsedTimePercentage.text = $"{roundController.ElapsedTimePercentage:P0}";
        }
    }
}
