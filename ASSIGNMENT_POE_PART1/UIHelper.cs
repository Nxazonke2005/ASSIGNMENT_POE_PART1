using System;
using System.Threading;

public class UIHelper
{
    public static void ShowHeader()
    {
        Console.ForegroundColor = ConsoleColor.Green;

        Console.WriteLine("=================================================");
        Console.WriteLine("     🤖 CYBERSECURITY AWARENESS BOT 🔐");
        Console.WriteLine("=================================================");

        Console.ResetColor();

        Console.WriteLine(@"
   _______  __   __  _______  ______   _______
  |       ||  | |  ||       ||    _ | |       |
  |       ||  |_|  ||    ___||   | || |  _____|
  |       ||       ||   |___ |   |_||_| |_____
  |      _||       ||    ___||    __  |_____  |
  |     |_ |   _   ||   |___ |   |  | | _____| |
  |_______||__| |__||_______||___|  |_||_______|

        🔐 STAY SAFE • STAY SMART 🔐
        ");
    }

    public static void ShowMenu()
    {
        Console.WriteLine("\n💬 What would you like to learn about?");
        Console.WriteLine("1️⃣ Phishing");
        Console.WriteLine("2️⃣ Password Safety");
        Console.WriteLine("3️⃣ Safe Browsing");
        Console.WriteLine("4️⃣ My Purpose");
        Console.WriteLine("5️⃣ Exit");
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