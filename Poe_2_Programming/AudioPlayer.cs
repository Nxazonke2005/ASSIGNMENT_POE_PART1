using System.Media;

namespace CyberSecurityAwarenessBot
{
    public class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Greeting.wav");

                player.Play();
            }
            catch
            {

            }
        }
    }
}