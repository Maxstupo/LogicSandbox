namespace Maxstupo.LogicSandbox.Logic {

    using System;
    using System.Diagnostics;
    using System.Timers;

    public class CircuitSimulator {

        private readonly Timer timer = new Timer();
        private readonly Stopwatch stopwatch = new Stopwatch();


        /// <summary>The circuit to simulate.</summary>
        public Circuit Circuit { get; set; }

        public bool IsRunning => timer.Enabled;

        /// <summary>The delta time modifier for increasing simulation speed.</summary>
        public float Speed { get; set; } = 1f;

        /// <summary>The number of simulation updates per second, the simulator should attempt to achieve.</summary>
        public int TargetUps { get; set; } = 30;

        /// <summary>Returns the actual number of updates per second.</summary>
        public int ActualUps { get; private set; }

        public event EventHandler OnStateChange;

        private long OptimalTime => 1000 / TargetUps;

        private int ups = 0;
        private long lastUpsTime = 0;

        public CircuitSimulator() {
            timer.Elapsed += (s, e) => Step();
            timer.AutoReset = true;
            timer.Interval = 16;
        }

        public void SingleStep(float delta = 1) {
            if (Circuit?.Step(delta * Speed) ?? false)
                OnStateChange?.Invoke(this, EventArgs.Empty);
        }

        private void Step() {
            long elapsedTime = stopwatch.ElapsedMilliseconds;
            stopwatch.Restart();

            // Time between each simulation step in seconds.
            float delta = elapsedTime / 1000f;

            lastUpsTime += elapsedTime;
            ups++;

            if (lastUpsTime >= 1000) { // Update counter.
                ActualUps = ups;
                ups = 0;
                lastUpsTime = 0;
            }

            SingleStep(delta);

            double interval = (stopwatch.ElapsedMilliseconds + OptimalTime);
            if (interval > 0)
                timer.Interval = interval;
        }


        public void Start() {
            stopwatch.Reset();
            stopwatch.Start();
            timer.Start();
        }

        public void Stop() {
            stopwatch.Stop();
            timer.Stop();
        }

        public void Toggle() {
            if (IsRunning) {
                Stop();
            } else {
                Start();
            }
        }

    }

}