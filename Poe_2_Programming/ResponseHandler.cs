using System;

namespace CyberSecurityAwarenessBot
{
    public class ResponseHandler
    {
        static Random random = new Random();

        // Phishing responses — all different
        static string[] phishingResponses =
        {
            "Phishing emails often have spelling mistakes and generic greetings like 'Dear Customer'.",
            "Never enter your password after clicking a link in an email — go directly to the website instead.",
            "Scammers use fake login pages that look identical to real ones. Always check the URL bar.",
            "If you receive a suspicious email, forward it to report@phishing.gov (in the US) or your country's anti-phishing center.",
            "OTP (one-time password) codes should NEVER be shared — not even with 'bank employees' who call you."
        };

        static string[] passwordResponses =
        {
            "Use a passphrase: 4 random words like 'Horse-Battery-Staple-Correct' — long and easy to remember.",
            "Change your password immediately if a website you use announces a data breach.",
            "Avoid using your pet's name, birth year, or favorite sports team — hackers check social media for those.",
            "Most hacked passwords: 123456, password, qwerty, admin. Don't use these!",
            "Enable 2FA everywhere — especially email and banking."
        };

        static string[] malwareResponses =
        {
            "Malware can arrive via email attachments named 'Invoice.pdf.exe' — always show file extensions.",
            "Ransomware gangs often target businesses, but individuals get hit too. Back up your files offline.",
            "Fake 'Your PC is infected' pop-ups are scams — don't call the number or download their 'fix'.",
            "Keep your browser updated — many malware attacks exploit old browser vulnerabilities.",
            "Use ad-blockers to avoid malicious ads that install malware without clicking (drive-by downloads)."
        };

        public static string GetPhishingResponse() => phishingResponses[random.Next(phishingResponses.Length)];
        public static string GetPasswordResponse() => passwordResponses[random.Next(passwordResponses.Length)];
        public static string GetMalwareResponse() => malwareResponses[random.Next(malwareResponses.Length)];
    }
}