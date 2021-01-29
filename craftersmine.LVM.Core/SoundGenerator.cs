using NAudio.Wave;
using NAudio.Wave.SampleProviders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace craftersmine.LVM.Core
{
    /// <summary>
    /// Provides static methods for generating system beeper sounds
    /// </summary>
    public sealed class SoundGenerator
    {
        /// <summary>
        /// Plays sine wave sound with specified frequency and duration
        /// </summary>
        /// <param name="frequency">Sine wave frequency</param>
        /// <param name="duration">Sound play duration</param>
        public static void PlaySine(float frequency, float duration)
        {
            SignalGenerator generator = new SignalGenerator(44800, 2) { Frequency = frequency, Gain = 0.05, Type = SignalGeneratorType.Sin };
            var samples = generator.Take(TimeSpan.FromSeconds(duration));
            using (var wo = new WaveOutEvent())
            {
                wo.Init(samples);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

        /// <summary>
        /// Plays sine wave sounds in specific order
        /// </summary>
        /// <param name="code">
        /// <para>Code to play</para>
        /// <para>. = short beep</para>
        /// <para>- = long beep</para>
        /// <para>ex. "-.-" - long short long</para>
        /// </param>
        public static void BeepMorse(string code)
        {
            foreach (var c in code)
            {
                switch (c)
                {
                    case '-':
                        PlaySine(1000, 0.4f);
                        break;
                    case '.':
                        PlaySine(1000, 0.2f);
                        break;
                }
            }
        }
    }
}