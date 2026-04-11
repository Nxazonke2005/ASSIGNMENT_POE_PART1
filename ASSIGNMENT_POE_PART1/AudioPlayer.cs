using System;
using System.Media;

public class AudioPlayer
{
    public static void PlayGreeting()
    {
        try
        {
            SoundPlayer player = new SoundPlayer(ASSIGNMENT_POE_PART1.Properties.Resources.Ano);
            player.Play();
        }
        catch
        {
            Console.WriteLine(" (Audio not available)");
        }
    }
}