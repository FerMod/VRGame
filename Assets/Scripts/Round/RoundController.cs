using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Garitto
{
    public class RoundController : MonoBehaviour
    {
        [Tooltip("The in-game round duration in seconds")]
        public float roundDuration = 60f;

        [Tooltip("The time step in seconds at which the round timer updates")]
        public float timeStep = 5f;

        [Tooltip("The interval in seconds at which the round timer updates")]
        public float updateInterval = 5f;

        public event Action OnRoundStart;
        public event Action OnRoundPause;
        public event Action OnRoundResume;
        public event Action OnRoundEnd;
        public event Action OnRoundTick;

        public double ElapsedTime { get; private set; } = 0f;
        public bool IsPaused { get; private set; } = false;

        public double ElapsedTimePercentage => ElapsedTime / roundDuration;
        public float RealTimeDuration => roundDuration * (updateInterval / timeStep);

        private Coroutine roundCoroutine;

        public void StartRound()
        {
            StartTimerCoroutine();
            OnRoundStart?.Invoke();
        }

        public void PauseRound()
        {
            IsPaused = true;
            OnRoundPause?.Invoke();
        }

        public void ResumeRound()
        {
            IsPaused = false;
            OnRoundResume?.Invoke();
        }

        public void RestartRound()
        {
            StopTimerCoroutine();
            StartRound();
        }

        private void EndRound()
        {
            StopTimerCoroutine();
            OnRoundEnd?.Invoke();
        }

        private void StartTimerCoroutine()
        {
            StopTimerCoroutine();

            ElapsedTime = 0f;
            IsPaused = false;
            roundCoroutine = StartCoroutine(RoundTimer());
        }

        private void StopTimerCoroutine()
        {
            if (roundCoroutine == null) return;

            StopCoroutine(roundCoroutine);
            roundCoroutine = null;
        }

        private IEnumerator RoundTimer()
        {
            while (ElapsedTime < roundDuration)
            {
                yield return new WaitForSeconds(updateInterval);

                if (!IsPaused)
                {
                    ElapsedTime += timeStep;
                    OnRoundTick?.Invoke();
                }
            }

            EndRound();
        }

        private void OnDestroy()
        {
            StopTimerCoroutine();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(RoundController))]
    public class RoundControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var roundController = (RoundController)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Runtime Info", EditorStyles.boldLabel);
            EditorGUILayout.LabelField("Real Time Duration", FormattedTime(roundController.RealTimeDuration));
            EditorGUILayout.LabelField("Elapsed Time / Duration", $"{FormattedTime(roundController.ElapsedTime)} / {FormattedTime(roundController.roundDuration)}");

            // Ensure the inspector updates in real-time
            if (Application.isPlaying && EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
        }

        private string FormattedTime(double seconds)
        {
            TimeSpan timeSpan;
            try
            {
                timeSpan = TimeSpan.FromSeconds(seconds);
            }
            catch (OverflowException)
            {
                timeSpan = TimeSpan.Zero;
            }

            return $"{timeSpan.Hours:D2}h {timeSpan.Minutes:D2}m {timeSpan.Seconds:D2}s";
        }
    }
#endif
}
