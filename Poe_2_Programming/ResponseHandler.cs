using System;

namespace CyberSecurityAwarenessBot
{
    public class ResponseHandler
    {
        static Random random = new Random();

        static string[] phishingResponses =
        {
            "Be careful of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
            "Never click suspicious links in emails or messages.",
            "Always verify the sender before sharing personal details online."
        };

        static string[] passwordResponses =
        {
            "Use strong passwords with symbols, numbers and uppercase letters.",
            "Never use your name or birthdate in your password.",
            "Use a different password for every account."
        };

        static string[] malwareResponses =
        {
            "Install antivirus software to protect against malware.",
            "Do not download files from unknown websites.",
            "Keep your software updated to avoid malware attacks."
        };

        static string[] safeBrowsingResponses =
        {
            "Only visit trusted websites.",
            "Avoid downloading suspicious files online.",
            "Always check if a website uses HTTPS."
        };

        public static string GetPhishingResponse()
        {
            return phishingResponses[random.Next(phishingResponses.Length)];
        }

        public static string GetPasswordResponse()
        {
            return passwordResponses[random.Next(passwordResponses.Length)];
        }

        public static string GetMalwareResponse()
        {
            return malwareResponses[random.Next(malwareResponses.Length)];
        }

        public static string GetSafeBrowsingResponse()
        {
            return safeBrowsingResponses[random.Next(safeBrowsingResponses.Length)];
        }

        public static string GetPurposeResponse()
        {
            return "My purpose is to educate users about cybersecurity awareness and help people stay safe online.";
        }

        public static string GetMorePhishingInfo()
        {
            return "Phishing attacks usually trick users into revealing passwords, banking details or personal information through fake emails and websites.";
        }

        public static string GetMorePasswordInfo()
        {
            return "Strong passwords reduce the chances of hackers accessing your accounts.";
        }
    }
}