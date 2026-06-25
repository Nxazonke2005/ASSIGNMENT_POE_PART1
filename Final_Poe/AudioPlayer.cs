using System.Media;

namespace CyberSecurityAwarenessBot
{
    public class AudioPlayer
    {
        public static void PlayGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Ano.wav");

                player.Play();
            }
            catch
            {

            }
        }
    }
}