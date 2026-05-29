using System;

namespace CyberSecurityAwarenessBot
{
    public class Chatbot
    {
        // Tracks current topic being discussed
        private string currentTopic = "";

        // Counts how many times user asked for more information
        private int followUpCount = 0;

        // Detects user sentiment (curious, worried, neutral)
        private string lastUserSentiment = "neutral";

        // Counts tips given for current topic
        private int tipCount = 0;

        // Main method that processes user input and returns bot response
        public string GetResponse(string input)
        {
            // Converts input to lowercase for easier matching
            input = input.ToLower().Trim();

            // SENTIMENT DETECTION - checks for emotional keywords
            if (input.Contains("curious") || input.Contains("interest") || input.Contains("want to learn"))
                lastUserSentiment = "curious";
            else if (input.Contains("worried") || input.Contains("scared") || input.Contains("concerned") || input.Contains("nervous") || input.Contains("afraid"))
                lastUserSentiment = "worried";
            else
                lastUserSentiment = "neutral";

            // TOPIC DETECTION - Check for cybersecurity topics FIRST (before greetings)

            // Phishing topic
            if (input.Contains("phishing") || input.Contains("scam") || input.Contains("fake email") || input.Contains("fake website"))
            {
                // Reset tracking for new topic
                currentTopic = "phishing";
                followUpCount = 0;
                tipCount = 0;
                return GetPhishingResponse(input);
            }

            // Password topic
            else if (input.Contains("password") || input.Contains("pin") || input.Contains("login") || input.Contains("account"))
            {
                currentTopic = "password";
                followUpCount = 0;
                tipCount = 0;
                return GetPasswordResponse(input);
            }

            // Malware topic
            else if (input.Contains("malware") || input.Contains("virus") || input.Contains("trojan") || input.Contains("worm") || input.Contains("ransomware"))
            {
                currentTopic = "malware";
                followUpCount = 0;
                tipCount = 0;
                return GetMalwareResponse(input);
            }

            // Safe browsing topic
            else if (input.Contains("safe browsing") || input.Contains("internet safety") || input.Contains("online safety") || input.Contains("privacy"))
            {
                currentTopic = "safebrowsing";
                followUpCount = 0;
                tipCount = 0;
                return GetSafeBrowsingResponse(input);
            }

            // FOLLOW-UPS based on current topic
            else if (input.Contains("tell me more") || input.Contains("more information") || input.Contains("elaborate"))
            {
                if (string.IsNullOrEmpty(currentTopic))
                    return "Please ask me about a topic first, like 'phishing' or 'passwords'.";

                followUpCount++;
                return GetMoreInfo(currentTopic);
            }

            else if (input.Contains("another tip") || input.Contains("give me a tip") || input.Contains("more tips"))
            {
                if (string.IsNullOrEmpty(currentTopic))
                    return "Please ask me about a topic first, like 'phishing' or 'passwords'.";

                tipCount++;
                return GetAnotherTip(currentTopic);
            }

            else if (input.Contains("example") || input.Contains("examples") || input.Contains("show me an example"))
            {
                if (string.IsNullOrEmpty(currentTopic))
                    return "Please ask me about a topic first, like 'phishing' or 'passwords'.";

                return GetExample(currentTopic);
            }

            // GREETINGS - Check AFTER topics so "phishing" doesn't get treated as greeting
            else if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey") || input.Contains("greetings") || input == "help" || input.Contains("start"))
            {
                return "Hello. I can help you learn about passwords, phishing, malware, and online safety. What would you like to know?";
            }

            // EXIT conversation
            else if (input.Contains("bye") || input.Contains("goodbye") || input.Contains("exit") || input.Contains("quit"))
            {
                return "Goodbye. Stay safe online. Remember: Think before you click.";
            }

            // HELP command
            else if (input.Contains("help") || input.Contains("what can i ask"))
            {
                return GetHelpMessage();
            }

            // DEFAULT response when input doesn't match any pattern
            else
            {
                return "I can help with cybersecurity topics. Try: 'phishing', 'passwords', 'malware', or 'safe browsing'.";
            }
        }

        // Generates phishing response based on user input and sentiment
        private string GetPhishingResponse(string input)
        {
            // Check for specific question types
            if (input.Contains("tell me about") || input.Contains("what is") || input.Contains("explain"))
            {
                if (lastUserSentiment == "curious")
                    return "Since you're curious: Phishing is when scammers send fake emails or texts pretending to be real companies like PayPal, Amazon, or your bank. They want you to click a link and enter your password on their fake website.";
                else if (lastUserSentiment == "worried")
                    return "I understand you're worried. Phishing is common, but here's good news: you can spot it. Always check the sender's email address, look for spelling errors, and never click links in unexpected messages.";
                else
                    return "Phishing is a cyberattack where criminals trick you into revealing personal information through fake messages that look legitimate.";
            }
            else if (input.Contains("example"))
            {
                return "Phishing example: You receive an email from security at paypa1.com (notice the number 1 instead of letter l) saying 'Your account is locked. Click here to verify.' The link goes to a fake PayPal login page that steals your credentials.";
            }
            else
            {
                // For just "phishing" alone - give a helpful intro response
                return "Phishing is when scammers send fake messages to steal your passwords or personal information. Would you like me to tell you more, show an example, or give you tips to avoid it?";
            }
        }

        // Generates password response based on user input and sentiment
        private string GetPasswordResponse(string input)
        {
            if (input.Contains("tell me about") || input.Contains("what is") || input.Contains("explain"))
            {
                if (lastUserSentiment == "curious")
                    return "Great that you're curious. Strong passwords use uppercase, lowercase, numbers, and symbols. Example: G7 hash kL9 dollar qR2 exclamation is strong, but password123 is weak. Length matters too - aim for 12 or more characters.";
                else if (lastUserSentiment == "worried")
                    return "No need to worry. You can protect yourself easily. Use a password manager like Bitwarden or LastPass to create and store unique, strong passwords for every account. Then you only need to remember one master password.";
                else
                    return "Passwords are your first defense against hackers. A strong password is unique, long with 12 or more characters, and uses a mix of character types.";
            }
            else if (input.Contains("example"))
            {
                return "Password examples: Weak passwords include 'password123', 'John1980', or 'qwerty'. Strong passwords include 'S hash 9mR exclamation t2 dollar kLp' or 'Purple-Dolphin-Runs-92 exclamation'.";
            }
            else
            {
                // For just "password" alone
                return "Strong passwords protect your accounts from hackers. Use a mix of uppercase, lowercase, numbers, and symbols. Would you like an example or some tips?";
            }
        }

        // Generates malware response based on user input and sentiment
        private string GetMalwareResponse(string input)
        {
            if (input.Contains("tell me about") || input.Contains("what is") || input.Contains("explain"))
            {
                if (lastUserSentiment == "curious")
                    return "Curious about malware? It's malicious software including viruses that spread between files, worms that spread without human help, trojans that pretend to be legitimate software, and ransomware that locks your files for money.";
                else if (lastUserSentiment == "worried")
                    return "It's smart to be concerned, but you can stay safe. Install antivirus software like Windows Defender or Malwarebytes, keep software updated, and never download from untrusted sites.";
                else
                    return "Malware is harmful software designed to damage devices, steal data, or spy on users. It often spreads through infected downloads, email attachments, or malicious advertisements.";
            }
            else if (input.Contains("example"))
            {
                return "Malware example: You download a free PDF converter from a pop-up advertisement. Inside is a trojan that records every keystroke including passwords and sends them to hackers.";
            }
            else
            {
                // For just "malware" alone
                return "Malware includes viruses, ransomware, and trojans that can damage your computer. Would you like me to explain more or give you protection tips?";
            }
        }

        // Generates safe browsing response
        private string GetSafeBrowsingResponse(string input)
        {
            if (input.Contains("tell me about") || input.Contains("what is"))
            {
                return "Safe browsing means visiting only trusted websites, checking for 'https://' and a padlock icon, and avoiding suspicious downloads. Never save passwords on public computers, and always log out when finished.";
            }
            else
            {
                return "Safe browsing protects you from fake websites and malware. Look for https:// and padlock icons. Would you like more details?";
            }
        }

        // Provides deeper information based on how many times user asked for more
        private string GetMoreInfo(string topic)
        {
            if (topic == "phishing")
            {
                if (followUpCount == 1)
                    return "More on phishing: Over 80 percent of cyberattacks start with phishing. Always hover over links before clicking to see the real URL. If suspicious, type the website address manually in your browser.";
                else if (followUpCount == 2)
                    return "Even deeper: Spear phishing targets you personally using your name, job, or recent purchases. Vishing is phone phishing. Smishing is SMS phishing. Never share OTP codes with anyone calling you.";
                else
                    return "Advanced phishing: Hackers create fake login pages for Google, Microsoft, or banking websites. Always check the URL bar. If it doesn't match exactly, don't type your password.";
            }
            else if (topic == "password")
            {
                if (followUpCount == 1)
                    return "More on passwords: Avoid common patterns like 'Admin123', 'Welcome2024', or 'Summer exclamation'. Use a passphrase like 'Correct-Horse-Battery-Staple' - long random words are hard to crack but easy to remember.";
                else if (followUpCount == 2)
                    return "Password managers like Bitwarden, 1Password, or Apple Keychain generate random passwords and autofill them. You only need to remember one master password.";
                else
                    return "Two-Factor Authentication or 2FA is your best friend. Even if hackers steal your password, they cannot log in without your phone's code. Use an authenticator app like Google Authenticator instead of SMS when possible.";
            }
            else if (topic == "malware")
            {
                if (followUpCount == 1)
                    return "More on malware: Ransomware attacks increased by 300 percent recently. Always back up important files to an external hard drive or cloud storage disconnected from your computer.";
                else if (followUpCount == 2)
                    return "Hidden malware: Some malware hides in free software cracks, driver updaters, or browser extensions. Only download from official app stores or verified developer websites.";
                else
                    return "Real-time protection: Keep Windows Defender or antivirus software turned on, enable firewall, run weekly scans, and turn on automatic updates for Windows and your browser.";
            }
            else if (topic == "safebrowsing")
            {
                return "Safe browsing tips: Use ad-blockers like uBlock Origin, avoid pirated content sites, check HaveIBeenPwned for data breaches, and clear your browser cache and cookies regularly.";
            }
            else
            {
                return "General tip: Keep all software updated. Hackers exploit known vulnerabilities in old versions. Enable automatic updates everywhere.";
            }
        }

        // Provides different tips based on topic and cycles through multiple tips
        private string GetAnotherTip(string topic)
        {
            if (topic == "phishing")
            {
                string[] tips = {
                    "Phishing tip: If an email creates urgency like 'Your account will close today', that is a red flag. Pause and verify by calling the company directly.",
                    "Phishing tip: Forward suspicious emails to report at phishing dot gov in the US.",
                    "Phishing tip: Check email headers for Reply-To addresses that do not match the company's real domain.",
                    "Phishing tip: Enable Multi-Factor Authentication everywhere. Even if you click a phishing link, MFA blocks access."
                };
                return tips[tipCount % tips.Length];
            }
            else if (topic == "password")
            {
                string[] tips = {
                    "Password tip: Never reuse passwords across email, banking, and social media. One breach puts all accounts at risk.",
                    "Password tip: Change passwords immediately if a service you use announces a data breach.",
                    "Password tip: Avoid using pet names, birthdays, or favorite sports teams. Hackers check social media for these.",
                    "Password tip: A 12-character random password takes centuries to crack. '123456' takes less than 1 second."
                };
                return tips[tipCount % tips.Length];
            }
            else if (topic == "malware")
            {
                string[] tips = {
                    "Malware tip: Do not plug in unknown USB drives. Attackers leave infected USBs in parking lots as bait.",
                    "Malware tip: Show file extensions in Windows by turning on 'File name extensions'.",
                    "Malware tip: Fake 'Your PC is infected' pop-ups are scams. Do not call the number.",
                    "Malware tip: Use a standard non-admin user account for daily tasks. Malware cannot install without admin rights."
                };
                return tips[tipCount % tips.Length];
            }
            else
            {
                return "General tip: Turn on automatic updates for your operating system, browser, and all applications.";
            }
        }

        // Provides real-world examples for each topic
        private string GetExample(string topic)
        {
            if (topic == "phishing")
                return "Real phishing example: An email that says 'Your Netflix payment failed. Update now' - the link goes to a fake Netflix site that steals your credit card.";
            else if (topic == "password")
                return "Password example: 'BlueSky2024 exclamation' takes hackers 2 weeks to crack. 'B exclamation u3S hash kY dollar 2 ampersand kL9' takes over 1000 years. Length beats complexity.";
            else if (topic == "malware")
                return "Malware example: Fake 'Windows Update' pop-ups on websites. Click it and you download ransomware that encrypts all your photos and documents.";
            else if (topic == "safebrowsing")
                return "Safe browsing example: Always check URL - 'paypal.com' is safe, 'paypa1-login.com' is a scam. Look for the padlock icon and 'https://' before entering any password.";
            else
                return "Example: When shopping online, make sure the website URL starts with 'https://' and has a padlock icon before entering credit card information.";
        }

        // Shows help message with all available commands
        private string GetHelpMessage()
        {
            return "What you can ask me:" + Environment.NewLine +
                   Environment.NewLine +
                   "- Type 'phishing' for basic information" + Environment.NewLine +
                   "- Type 'tell me about phishing' for detailed explanation" + Environment.NewLine +
                   "- Type 'I'm worried about malware' for reassurance" + Environment.NewLine +
                   "- Type 'give me a password tip' for a security tip" + Environment.NewLine +
                   "- Type 'example' after any topic for real-world examples" + Environment.NewLine +
                   "- Type 'tell me more' for deeper information" + Environment.NewLine +
                   "- Type 'another tip' for another security tip" + Environment.NewLine +
                   Environment.NewLine +
                   "Try typing just 'phishing' to start.";
        }
    }
}