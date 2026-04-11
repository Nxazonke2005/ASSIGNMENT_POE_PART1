using System;

public class Chatbot
{
    private User user = new User();

    public void Start()
    {
        Console.Write(" Hello friend! What is your name? ");
        
        user.Name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(user.Name))
            user.Name = "Friend";

        Console.WriteLine($"\nWelcome, {user.Name}! I'm here to help you stay safe online.\n");
        Console.Write(" How are you?: ");
        string impilo = Console.ReadLine();
        while (true)
        {
            
            UIHelper.ShowMenu();
            Console.Write($"\n{user.Name}, choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ResponseHandler.ShowPhishing();
                    break;

                case "2":
                    ResponseHandler.ShowPassword();
                    break;

                case "3":
                    ResponseHandler.ShowBrowsing();
                    break;

                case "4":
                    ResponseHandler.ShowPurpose();
                    break;

                case "5":
                    Console.WriteLine(" Goodbye! Stay safe online ");
                    return;

                default:
                    Console.WriteLine(" Invalid choice. Please select 1–5.");
                    break;
            }
        }
    }
}