using System;
using System.Collections.Generic;

namespace CyberSecurityAwarenessBot
{
    public class Chatbot
    {
        private User user = new User();
        private Random random = new Random();

        private Dictionary<string, List<string>> intentKeywords = new Dictionary<string, List<string>>
        {
            ["task"] = new List<string> { "task", "todo", "to do", "remind", "reminder", "add task", "new task", "create task" },
            ["quiz"] = new List<string> { "quiz", "test", "question", "trivia", "start quiz", "take quiz" },
            ["activity"] = new List<string> { "activity", "log", "history", "what have you done", "show log" },
            ["phishing"] = new List<string> { "phishing", "scam", "fake email", "fake website" },
            ["password"] = new List<string> { "password", "pin", "login", "account", "strong password" },
            ["malware"] = new List<string> { "malware", "virus", "trojan", "worm", "ransomware" },
            ["browsing"] = new List<string> { "browsing", "internet safety", "online safety", "privacy" }
        };

        private Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>
        {
            ["phishing"] = new List<string> {
                "Phishing is a cyberattack where criminals impersonate trusted sources to steal your data.",
                "Always check the sender's email address - scammers use fake domains like paypa1.com.",
                "Never click links in suspicious emails. Type the URL directly into your browser.",
                "Look for urgency in messages - 'Your account will be closed' is a common phishing tactic.",
                "Report phishing emails to your email provider or report@phishing.gov."
            },
            ["password"] = new List<string> {
                "Use strong passwords with uppercase, lowercase, numbers, and symbols.",
                "Never reuse passwords across different accounts - one breach puts all at risk.",
                "Use a password manager like Bitwarden or LastPass to store and generate passwords.",
                "Change passwords immediately if a service you use announces a data breach.",
                "Enable Two-Factor Authentication (2FA) on all important accounts."
            },
            ["malware"] = new List<string> {
                "Malware includes viruses, ransomware, and trojans that can damage your devices.",
                "Install antivirus software and keep it updated - Windows Defender is built-in.",
                "Never download from untrusted sites or open suspicious email attachments.",
                "Back up your important files to an external drive or cloud storage.",
                "Show file extensions in Windows to spot Invoice.pdf.exe - a common malware trick."
            },
            ["browsing"] = new List<string> {
                "Safe browsing means visiting trusted websites with https:// and padlock icons.",
                "Use a VPN on public Wi-Fi to protect your data from eavesdroppers.",
                "Clear your browser cache and cookies regularly to protect your privacy.",
                "Avoid clicking on pop-up ads or downloading from untrusted sources.",
                "Use ad-blockers like uBlock Origin to prevent malicious ads."
            }
        };

        public string ProcessInput(string input)
        {
            input = input.ToLower().Trim();
            string intent = DetectIntent(input);

            switch (intent)
            {
                case "task": return "TASK_ADD";
                case "quiz": return "QUIZ_START";
                case "activity": return "ACTIVITY_SHOW";
                case "phishing": return GetTopicResponse("phishing", input);
                case "password": return GetTopicResponse("password", input);
                case "malware": return GetTopicResponse("malware", input);
                case "browsing": return GetTopicResponse("browsing", input);
                default: return GetGeneralResponse(input);
            }
        }

        private string DetectIntent(string input)
        {
            foreach (var intent in intentKeywords)
            {
                foreach (var keyword in intent.Value)
                {
                    if (input.Contains(keyword))
                        return intent.Key;
                }
            }
            return "general";
        }

        private string GetTopicResponse(string topic, string input)
        {
            if (topicResponses.ContainsKey(topic))
            {
                var responses = topicResponses[topic];

                if (input.Contains("example"))
                    return GetExample(topic);

                if (input.Contains("protect") || input.Contains("avoid"))
                    return GetProtectionTips(topic);

                if (input.Contains("tell me more") || input.Contains("more information"))
                    return GetAdvancedInfo(topic);

                if (input.Contains("tip"))
                    return GetSpecificTip(topic);

                return responses[random.Next(responses.Count)];
            }
            return "I can help with cybersecurity topics. Try: phishing, passwords, malware, or safe browsing.";
        }

        private string GetGeneralResponse(string input)
        {
            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
                return "Hello. I can help with cybersecurity topics, tasks, and quizzes. What would you like to know?";

            if (input.Contains("help") || input.Contains("what can i ask"))
            {
                return "I can help with:" + Environment.NewLine +
                       "- Cybersecurity topics: phishing, passwords, malware, safe browsing" + Environment.NewLine +
                       "- Tasks: 'Add task to enable 2FA' or 'Remind me to update my password'" + Environment.NewLine +
                       "- Quizzes: 'Start a quiz' to test your knowledge" + Environment.NewLine +
                       "- Activity: 'Show activity log' to see what I've done for you";
            }

            if (input.Contains("bye") || input.Contains("goodbye") || input.Contains("exit"))
                return "Goodbye. Stay safe online. Remember: Think before you click.";

            if (input.Contains("tell me more"))
                return GetAdvancedInfo("general");

            if (input.Contains("another tip"))
                return GetSpecificTip("general");

            if (input.Contains("example"))
                return GetExample("general");

            return "I'm not sure about that. Try asking about: phishing, passwords, malware, safe browsing, tasks, or quizzes.";
        }

        private string GetExample(string topic)
        {
            Dictionary<string, string> examples = new Dictionary<string, string>
            {
                ["phishing"] = "Example: You receive 'Your Netflix payment failed - Update now' with a fake link to steal your credit card.",
                ["password"] = "Example: BlueSky2024! is weak. B!u3S*ky$2@ is strong - length and variety matter.",
                ["malware"] = "Example: Free PDF converter download actually contains a trojan that steals passwords.",
                ["browsing"] = "Example: http:// sites are unsafe - always use https:// for sensitive information.",
                ["general"] = "Example: When shopping online, always check for https:// and the padlock icon."
            };
            return examples.ContainsKey(topic) ? examples[topic] : examples["general"];
        }

        private string GetProtectionTips(string topic)
        {
            Dictionary<string, string> tips = new Dictionary<string, string>
            {
                ["phishing"] = "To protect yourself:" + Environment.NewLine +
                               "- Check sender addresses" + Environment.NewLine +
                               "- Hover over links to see real URL" + Environment.NewLine +
                               "- Enable 2FA on all accounts" + Environment.NewLine +
                               "- Never share OTP codes",
                ["password"] = "Password Protection:" + Environment.NewLine +
                               "- Use unique passwords for each account" + Environment.NewLine +
                               "- Enable 2FA everywhere" + Environment.NewLine +
                               "- Use a password manager" + Environment.NewLine +
                               "- Change passwords after breaches",
                ["malware"] = "Malware Protection:" + Environment.NewLine +
                              "- Keep software updated" + Environment.NewLine +
                              "- Use antivirus software" + Environment.NewLine +
                              "- Don't open suspicious attachments" + Environment.NewLine +
                              "- Backup your data regularly",
                ["browsing"] = "Safe Browsing Tips:" + Environment.NewLine +
                               "- Use https:// websites" + Environment.NewLine +
                               "- Clear cookies regularly" + Environment.NewLine +
                               "- Use ad-blockers" + Environment.NewLine +
                               "- Avoid suspicious downloads"
            };
            return tips.ContainsKey(topic) ? tips[topic] : "Stay safe online by following security best practices.";
        }

        private string GetAdvancedInfo(string topic)
        {
            Dictionary<string, string> advanced = new Dictionary<string, string>
            {
                ["phishing"] = "Advanced: Hackers use 'spear phishing' - personalized attacks targeting you specifically.",
                ["password"] = "Advanced: Use hardware security keys (YubiKey) for the strongest 2FA protection.",
                ["malware"] = "Advanced: Ransomware gangs now use 'double extortion' - stealing data AND encrypting it.",
                ["browsing"] = "Advanced: Use a DNS filtering service to block malicious websites automatically.",
                ["general"] = "Advanced: Consider using a VPN for all your internet traffic, especially on public Wi-Fi."
            };
            return advanced.ContainsKey(topic) ? advanced[topic] : advanced["general"];
        }

        private string GetSpecificTip(string topic)
        {
            Dictionary<string, string[]> tips = new Dictionary<string, string[]>
            {
                ["phishing"] = new string[] {
                    "Tip: If an email creates urgency like 'Your account will close today', that is a red flag.",
                    "Tip: Forward suspicious emails to report@phishing.gov.",
                    "Tip: Check email headers for Reply-To addresses that don't match the company's real domain."
                },
                ["password"] = new string[] {
                    "Tip: A 12-character random password takes centuries to crack. '123456' takes less than 1 second.",
                    "Tip: Use a passphrase like Correct-Horse-Battery-Staple - long and easy to remember.",
                    "Tip: Never use personal information like birthdays or pet names in passwords."
                },
                ["malware"] = new string[] {
                    "Tip: Do not plug in unknown USB drives - attackers leave infected USBs as bait.",
                    "Tip: Show file extensions in Windows by turning on 'File name extensions'.",
                    "Tip: Fake 'Your PC is infected' pop-ups are scams - do not call the number."
                },
                ["browsing"] = new string[] {
                    "Tip: Use different browsers for different activities to limit tracking.",
                    "Tip: Enable 'Do Not Track' in your browser settings.",
                    "Tip: Use private/incognito mode for sensitive browsing sessions."
                },
                ["general"] = new string[] {
                    "Tip: Enable automatic updates on all your devices and software.",
                    "Tip: Use a different password for every account - use a password manager.",
                    "Tip: Never share OTP codes with anyone - not even 'bank employees'."
                }
            };

            if (tips.ContainsKey(topic))
            {
                var topicTips = tips[topic];
                return topicTips[random.Next(topicTips.Length)];
            }
            return "Tip: Always think before you click on any link or attachment.";
        }

        public void SetUserName(string name)
        {
            user.Name = name;
        }

        public string GetUserName()
        {
            return user.Name;
        }
    }
}