using System;
using System.Media;
using System.IO;

namespace CyberSecurityAwarenessBot
{
    public class AudioPlayer
    {
        private static AudioPlayer? instance;
        private SoundPlayer? soundPlayer;
        private bool isAudioEnabled = true;
        private int volume = 80;

        private AudioPlayer()
        {
            try
            {
                soundPlayer = new SoundPlayer();
                Console.WriteLine("Audio system initialized.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Audio system not available: " + ex.Message);
                isAudioEnabled = false;
            }
        }

        public static AudioPlayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AudioPlayer();
                }
                return instance;
            }
        }

        public bool IsAudioEnabled
        {
            get { return isAudioEnabled; }
            set { isAudioEnabled = value; }
        }

        public int Volume
        {
            get { return volume; }
            set
            {
                volume = Math.Max(0, Math.Min(100, value));
            }
        }

        // Play WAV file
        public void PlayWav(string filePath)
        {
            if (!isAudioEnabled || string.IsNullOrWhiteSpace(filePath) || soundPlayer == null)
                return;

            try
            {
                if (File.Exists(filePath))
                {
                    soundPlayer.SoundLocation = filePath;
                    soundPlayer.Play(); // Play is supported on Windows
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing WAV: " + ex.Message);
            }
        }

        public void PlayWavLoop(string filePath)
        {
            if (!isAudioEnabled || string.IsNullOrWhiteSpace(filePath) || soundPlayer == null)
                return;

            try
            {
                if (File.Exists(filePath))
                {
                    soundPlayer.SoundLocation = filePath;
                    soundPlayer.PlayLooping();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing WAV loop: " + ex.Message);
            }
        }

        public void StopWav()
        {
            try
            {
                if (soundPlayer != null)
                {
                    soundPlayer.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error stopping WAV: " + ex.Message);
            }
        }

        public void PlayGreeting()
        {
            if (!isAudioEnabled)
                return;

            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "greeting.wav");
                PlayWav(soundPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing greeting: " + ex.Message);
            }
        }

        public void PlayNotification()
        {
            if (!isAudioEnabled)
                return;

            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "notification.wav");
                PlayWav(soundPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing notification: " + ex.Message);
            }
        }

        public void PlayTaskComplete()
        {
            if (!isAudioEnabled)
                return;

            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "complete.wav");
                PlayWav(soundPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing task complete: " + ex.Message);
            }
        }

        public void PlayQuizCorrect()
        {
            if (!isAudioEnabled)
                return;

            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "correct.wav");
                PlayWav(soundPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing quiz correct: " + ex.Message);
            }
        }

        public void PlayQuizIncorrect()
        {
            if (!isAudioEnabled)
                return;

            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "incorrect.wav");
                PlayWav(soundPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing quiz incorrect: " + ex.Message);
            }
        }

        public void PlaySuccess()
        {
            if (!isAudioEnabled)
                return;

            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "success.wav");
                PlayWav(soundPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing success: " + ex.Message);
            }
        }

        public void PlayError()
        {
            if (!isAudioEnabled)
                return;

            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "error.wav");
                PlayWav(soundPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error playing error: " + ex.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                if (soundPlayer != null)
                {
                    soundPlayer.Stop();
                    soundPlayer.Dispose();
                    soundPlayer = null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error disposing audio: " + ex.Message);
            }
        }
    }
}