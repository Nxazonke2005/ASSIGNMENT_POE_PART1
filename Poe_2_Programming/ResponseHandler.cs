using System;

namespace CyberSecurityAwarenessBot
{
    public class ResponseHandler
    {
        // Creates a random object for random responses
        static Random random = new Random();

        // Stores phishing responses
        static string[] phishingResponses =
        {
            "Be careful of emails asking for personal information.",
            "Never click suspicious links in emails or messages.",
            "Always verify the sender before sharing personal details online.",
            "Scammers create fake websites to steal passwords.",
            "Do not open attachments from unknown senders."
        };

        // Stores password responses
        static string[] passwordResponses =
        {
            "Use strong passwords with symbols, numbers and uppercase letters.",
            "Never use your name or birthdate in your password.",
            "Use a different password for every account.",
            "Enable two-factor authentication for extra security.",
            "Avoid short passwords because they are easy to hack."
        };

        // Stores malware responses
        static string[] malwareResponses =
        {
            "Install antivirus software to protect against malware.",
            "Do not download files from unknown websites.",
            "Keep your software updated to avoid malware attacks.",
            "Scan USB devices before opening files.",
            "Avoid clicking pop-up advertisements."
        };

        // Stores safe browsing responses
        static string[] safeBrowsingResponses =
        {
            "Only visit trusted websites.",
            "Avoid downloading suspicious files online.",
            "Always check if a website uses HTTPS.",
            "Never save passwords on public computers.",
            "Log out after using shared devices."
        };

        // Returns a random phishing response
        public static string GetPhishingResponse()
        {
            return phishingResponses[random.Next(phishingResponses.Length)];
        }

        // Returns a random password response
        public static string GetPasswordResponse()
        {
            return passwordResponses[random.Next(passwordResponses.Length)];
        }

        // Returns a random malware response
        public static string GetMalwareResponse()
        {
            return malwareResponses[random.Next(malwareResponses.Length)];
        }

        // Returns a random safe browsing response
        public static string GetSafeBrowsingResponse()
        {
            return safeBrowsingResponses[random.Next(safeBrowsingResponses.Length)];
        }

        // Explains the chatbot purpose
        public static string GetPurposeResponse()
        {
            return "My purpose is to educate users about cybersecurity awareness and online safety.";
        }

        // Gives detailed phishing information
        public static string GetMorePhishingInfo()
        {
            return "Phishing attacks trick users into revealing passwords or banking information using fake emails and websites.";
        }

        // Gives detailed password information
        public static string GetMorePasswordInfo()
        {
            return "Strong passwords should contain uppercase letters, numbers and symbols to improve security.";
        }
    }
}