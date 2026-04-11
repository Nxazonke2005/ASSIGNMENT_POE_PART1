using System;

public class ResponseHandler
{
    public static void ShowPhishing()
    {
        Console.WriteLine(@"
        🎣 PHISHING ATTACK
        -------------------
           
        ");

        Console.WriteLine("Phishing is a type of cyberattack where criminals pretend to be a trusted source.");
        Console.WriteLine("\n DEEP EXPLANATION:");
        Console.WriteLine("Attackers create fake emails, messages, or websites that LOOK real.");
        Console.WriteLine("Their goal is to manipulate your trust and trick you into revealing sensitive data.");

        Console.WriteLine("\n TARGETED INFORMATION:");
        Console.WriteLine("- Passwords");
        Console.WriteLine("- Bank details");
        Console.WriteLine("- Identity numbers");

        Console.WriteLine("\n REAL-LIFE EXAMPLE:");
        Console.WriteLine("You receive an email saying:");
        Console.WriteLine("'Your bank account is locked. Click here to fix it.'");
        Console.WriteLine("→ This link leads to a FAKE website that steals your data.");

        Console.WriteLine("\n HOW TO PROTECT YOURSELF:");
        Console.WriteLine("- Never click unknown links");
        Console.WriteLine("- Check sender email carefully");
        Console.WriteLine("- Use official websites only");
    }

    public static void ShowPassword()
    {
        Console.WriteLine(@"
         PASSWORD SECURITY
        --------------------
           [********]
        ");

        Console.WriteLine("A password is a secret authentication key used to protect your digital accounts.");

        Console.WriteLine("\n DEEP EXPLANATION:");
        Console.WriteLine("Passwords act as a digital lock that prevents unauthorized users from accessing your data.");
        Console.WriteLine("Weak passwords are easily guessed using automated hacking tools.");

        Console.WriteLine("\n WEAK PASSWORD:");
        Console.WriteLine("123456");

        Console.WriteLine("\n STRONG PASSWORD:");
        Console.WriteLine("S@fe!Pass2026");

        Console.WriteLine("\n CHARACTERISTICS OF STRONG PASSWORDS:");
        Console.WriteLine("- Long (8–12+ characters)");
        Console.WriteLine("- Mix of letters, numbers, symbols");
        Console.WriteLine("- Unique for each account");

        Console.WriteLine("\n WHY IT MATTERS:");
        Console.WriteLine("A weak password can allow hackers to steal your identity and money.");

        Console.WriteLine("\n SAFETY TIPS:");
        Console.WriteLine("- Never share passwords");
        Console.WriteLine("- Change them regularly");
        Console.WriteLine("- Use a password manager");
    }

    public static void ShowBrowsing()
    {
        Console.WriteLine(@"
         SAFE BROWSING
        ----------------
           [    ]
        ");

        Console.WriteLine("Safe browsing means using the internet in a secure and responsible way.");

        Console.WriteLine("\n DEEP EXPLANATION:");
        Console.WriteLine("It involves avoiding harmful websites, protecting your personal data,");
        Console.WriteLine("and preventing malware infections.");

        Console.WriteLine("\n RISKS ONLINE:");
        Console.WriteLine("- Fake websites");
        Console.WriteLine("- Malware downloads");
        Console.WriteLine("- Tracking and data theft");

        Console.WriteLine("\n HOW TO STAY SAFE:");
        Console.WriteLine("- Only visit HTTPS websites");
        Console.WriteLine("- Avoid suspicious links");
        Console.WriteLine("- Keep your browser updated");
        Console.WriteLine("- Install antivirus software");
    }

    public static void ShowPurpose()
    {
        Console.WriteLine("\n MY PURPOSE:");
        Console.WriteLine("I am here to educate you about cybersecurity and help you stay safe online.");
        Console.WriteLine("I simplify complex topics so anyone can understand digital safety.");
    }
}