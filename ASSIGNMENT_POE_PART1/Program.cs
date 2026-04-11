using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Title = "Cybersecurity Awareness Bot";

        AudioPlayer.PlayGreeting();

        UIHelper.ShowHeader();
        UIHelper.Typing("Welcome to your Cybersecurity Learning Assistant...\n");

        Chatbot bot = new Chatbot();
        bot.Start();
    }
}