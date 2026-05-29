namespace CyberSecurityAwarenessBot
{
    public class Chatbot
    {
        // Stores the current topic
        private string currentTopic = "";

        // Creates user object
        private User user = new User();

        // Main chatbot response method
        public string GetResponse(string input)
        {
            // Convert input to lowercase
            input = input.ToLower();

            // ---------------- GREETINGS ----------------
            if (input.Contains("hello") ||
                input.Contains("hi") ||
                input.Contains("hey"))
            {
                return "Hello. I can help you learn about passwords, phishing, malware and online safety.";
            }

            // ---------------- PASSWORD QUESTIONS ----------------
            else if (input.Contains("password") ||
                     input.Contains("pin") ||
                     input.Contains("login") ||
                     input.Contains("account"))
            {
                currentTopic = "password";

                // Password examples and explanations
                if (input.Contains("strong"))
                {
                    return "A strong password should contain uppercase letters, lowercase letters, numbers and symbols. Example: P@ssWord2026!";
                }

                else if (input.Contains("weak"))
                {
                    return "A weak password is easy to guess. Example: 123456 or john2000.";
                }

                else if (input.Contains("example"))
                {
                    return "Example of a strong password: T!ger#2026Secure";
                }

                else if (input.Contains("hack"))
                {
                    return "Hackers can steal weak passwords using guessing tools or fake websites.";
                }

                else
                {
                    return ResponseHandler.GetPasswordResponse();
                }
            }

            // ---------------- PHISHING QUESTIONS ----------------
            else if (input.Contains("phishing") ||
                     input.Contains("scam") ||
                     input.Contains("fake email") ||
                     input.Contains("fake website") ||
                     input.Contains("otp"))
            {
                currentTopic = "phishing";

                if (input.Contains("example"))
                {
                    return "Example of phishing: A fake bank email asking you to click a link and enter your password.";
                }

                else if (input.Contains("avoid"))
                {
                    return "To avoid phishing, never click suspicious links and always verify the sender.";
                }

                else if (input.Contains("email"))
                {
                    return "Phishing emails often contain urgent messages like 'Your account will be locked.'";
                }

                else
                {
                    return ResponseHandler.GetPhishingResponse();
                }
            }

            // ---------------- MALWARE QUESTIONS ----------------
            else if (input.Contains("malware") ||
                     input.Contains("virus") ||
                     input.Contains("trojan") ||
                     input.Contains("worm"))
            {
                currentTopic = "malware";

                if (input.Contains("example"))
                {
                    return "Example of malware: A virus hidden inside a fake game download.";
                }

                else if (input.Contains("protect"))
                {
                    return "You can protect your computer by installing antivirus software and avoiding suspicious downloads.";
                }

                else if (input.Contains("spread"))
                {
                    return "Malware spreads through infected downloads, USB devices and dangerous websites.";
                }

                else
                {
                    return ResponseHandler.GetMalwareResponse();
                }
            }

            // ---------------- SAFE BROWSING ----------------
            else if (input.Contains("safe browsing") ||
                     input.Contains("internet safety") ||
                     input.Contains("browser") ||
                     input.Contains("online safety"))
            {
                currentTopic = "safe browsing";

                return ResponseHandler.GetSafeBrowsingResponse();
            }

            // ---------------- ASKING FOR TIPS ----------------
            else if (input.Contains("tip") ||
                     input.Contains("tips") ||
                     input.Contains("advice") ||
                     input.Contains("help"))
            {
                if (currentTopic == "password")
                {
                    return "Password tip: Use different passwords for every account and enable two-factor authentication.";
                }

                else if (currentTopic == "phishing")
                {
                    return "Phishing tip: Never trust emails asking for passwords or banking details.";
                }

                else if (currentTopic == "malware")
                {
                    return "Malware tip: Keep your software updated and scan files before opening them.";
                }

                else
                {
                    return "Cybersecurity tip: Never share passwords online and avoid suspicious websites.";
                }
            }

            // ---------------- MORE INFORMATION ----------------
            else if (input.Contains("tell me more") ||
                     input.Contains("more") ||
                     input.Contains("explain") ||
                     input.Contains("details"))
            {
                if (currentTopic == "password")
                {
                    return "Strong passwords protect your accounts from hackers. Example: MyDog@2026!";
                }

                else if (currentTopic == "phishing")
                {
                    return "Phishing attacks trick users into revealing passwords or banking details using fake emails and websites.";
                }

                else if (currentTopic == "malware")
                {
                    return "Malware is harmful software that damages devices or steals information.";
                }

                else if (currentTopic == "safe browsing")
                {
                    return "Safe browsing means visiting trusted websites and protecting your private information online.";
                }

                else
                {
                    return "Cybersecurity helps protect computers, accounts and personal information from hackers.";
                }
            }

            // ---------------- PRIVACY ----------------
            else if (input.Contains("privacy"))
            {
                user.Interest = "privacy";

                return "Privacy means protecting your personal information online. Example: Do not share passwords publicly.";
            }

            // ---------------- PURPOSE ----------------
            else if (input.Contains("purpose") ||
                     input.Contains("what do you do"))
            {
                return "My purpose is to teach users about cybersecurity awareness and online safety.";
            }

            // ---------------- GOODBYE ----------------
            else if (input.Contains("bye") ||
                     input.Contains("goodbye"))
            {
                return "Goodbye. Stay safe online.";
            }

            // ---------------- DEFAULT RESPONSE ----------------
            // Bot always responds instead of saying "I don't understand"
            else
            {
                return "Cybersecurity is important for protecting passwords, devices and personal information online. You can ask me about phishing, malware, scams, passwords or internet safety.";
            }
        }
    }
}