using UnityEngine;

namespace Utils
{
    /// <summary>
    /// Timer has finished handler, method that should be called when the time set on the timer has passed.
    /// </summary>
    public delegate void TimerHasFinishedHandler();

    /// <summary>
    /// Timer implements a timer that notifies the calling object when the set time has passed.
    /// </summary>
    public class Timer
    {
        private readonly TimerHasFinishedHandler TimerHasFinishedHandler;

        private float TimeToReachInS = 0;
        private float TimePassedInS = 0;

        private bool Active = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="timerHasFinishedHandler">The function that should be called when the timer has finished.</param>
        public Timer(TimerHasFinishedHandler timerHasFinishedHandler)
        {
            TimerHasFinishedHandler = timerHasFinishedHandler;
        }

        /// <summary>
        /// Reset the timer to a new time.
        /// <param name="timeToReachInS">The time that should pass before the timer rings.</param>
        /// </summary>
        public void Set(float timeToReachInS)
        {
            Reset();
            TimeToReachInS = timeToReachInS;
            Activate();
        }

        private void Reset(){
            TimePassedInS = 0;
        }

        private void Deactivate(){
            Active = false;
        }

        private void Activate(){
            Active = true;
        }

        private bool IsCompleted()
        {
            return Active && TimePassedInS >= TimeToReachInS;
        }

        /// <summary>
        /// Update this instance with the time between frames.
        /// </summary>
        public void Tic()
        {
            if (Active) TimePassedInS += Time.deltaTime;

            if (IsCompleted())
            {
                TimerHasFinishedHandler();
                Reset();
                Deactivate();
            }
        }
    }
}
