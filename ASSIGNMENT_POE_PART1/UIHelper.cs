using System;
using System.Threading;

public class UIHelper
{
    public static void ShowHeader()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("=================================================");
        Console.WriteLine("      CYBERSECURITY AWARENESS BOT ");
        Console.WriteLine("=================================================");

        Console.ResetColor();

        Console.WriteLine(@"
         CYBERSECURITY AWARENESS 

             .--------.
            / .------. \
           / /        \ \
           | |        | |
          _| |________| |_
        .' |_|        |_| '.
        '._____ ____ _____.'
        |     .'____'.     |
        '.__.'.'    '.'.__.'
        '.__  | LOCK |  __.'
        |   '.'.____.'.'   |
        '.____'.____.'____.'

         USE STRONG PASSWORD 
");
    }

    public static void ShowMenu()
    {
        Console.WriteLine("\n What would you like to learn about?");
        Console.WriteLine("1 Phishing");
        Console.WriteLine("2 Password Safety");
        Console.WriteLine("3 Safe Browsing");
        Console.WriteLine("4 My Purpose");
        Console.WriteLine("5 Exit");
    }

    public static void Typing(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(15);
        }
        Console.WriteLine();
    }
}