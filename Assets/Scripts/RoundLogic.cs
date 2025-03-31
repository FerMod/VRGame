using System;
using System.Collections;
using UnityEngine;

namespace Garitto
{
    public class RoundLogic : MonoBehaviour
    {
        [Tooltip("The duration of the round in seconds")]
        public float roundDuration = 60f;

        public event Action OnRoundStart;
        public event Action OnRoundPause;
        public event Action OnRoundResume;
        public event Action OnRoundEnd;

        public float ElapsedTime { get; private set; } = 0f;
        public bool IsPaused { get; private set; } = false;

        private Coroutine roundCoroutine;

        void Start()
        {
            StartRound();
        }

        public void StartRound()
        {
            StartTimerCouroutine();
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
            StartRound();
        }

        private void EndRound()
        {
            StopTimerCouroutine();
            OnRoundEnd?.Invoke();
        }

        private void StartTimerCouroutine()
        {
            StopTimerCouroutine();

            ElapsedTime = 0f;
            IsPaused = false;
            roundCoroutine = StartCoroutine(RoundTimer());
        }

        private void StopTimerCouroutine()
        {
            if (roundCoroutine == null) return;

            StopCoroutine(roundCoroutine);
            roundCoroutine = null;
        }

        private IEnumerator RoundTimer()
        {
            while (ElapsedTime < roundDuration)
            {
                if (!IsPaused)
                {
                    ElapsedTime += Time.deltaTime;
                }
                yield return null;
            }

            EndRound();
        }
    }
}
