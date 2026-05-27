namespace CyberSecurityAwarenessBot
{
    public class Chatbot
    {
        private string currentTopic = "";

        private User user = new User();

        public string GetResponse(string input)
        {
            input = input.ToLower();

            // Greetings
            if (input.Contains("hello") || input.Contains("hi"))
            {
                return "Hello. How can I help you today?";
            }

            // Sentiment Detection
            if (input.Contains("worried"))
            {
                currentTopic = "scams";

                return "It is completely understandable to feel that way. Scammers can be very convincing. Here is a tip: Never share banking details with unknown people online.";
            }

            if (input.Contains("confused"))
            {
                return "Do not worry. I will explain it more simply for you.";
            }

            // Memory Feature
            if (input.Contains("interested in privacy"))
            {
                user.Interest = "privacy";

                return "Great. I will remember that you are interested in privacy. It is a very important part of staying safe online.";
            }

            // Recall memory
            if (input.Contains("privacy"))
            {
                return "As someone interested in privacy, you should review your account security settings regularly.";
            }

            // Phishing
            if (input.Contains("phishing"))
            {
                currentTopic = "phishing";

                return ResponseHandler.GetPhishingResponse();
            }

            // Malware
            else if (input.Contains("malware"))
            {
                currentTopic = "malware";

                return ResponseHandler.GetMalwareResponse();
            }

            // Passwords
            else if (input.Contains("password"))
            {
                currentTopic = "password";

                return ResponseHandler.GetPasswordResponse();
            }

            // Safe Browsing
            else if (input.Contains("safe browsing"))
            {
                currentTopic = "safe browsing";

                return ResponseHandler.GetSafeBrowsingResponse();
            }

            // Purpose
            else if (input.Contains("purpose"))
            {
                return ResponseHandler.GetPurposeResponse();
            }

            // Follow-up Questions
            else if (input.Contains("tell me more") || input.Contains("explain more"))
            {
                if (currentTopic == "phishing")
                {
                    return ResponseHandler.GetMorePhishingInfo();
                }

                else if (currentTopic == "password")
                {
                    return ResponseHandler.GetMorePasswordInfo();
                }

                else
                {
                    return "Please choose a cybersecurity topic first.";
                }
            }

            // Another Tip
            else if (input.Contains("another tip"))
            {
                if (currentTopic == "phishing")
                {
                    return ResponseHandler.GetPhishingResponse();
                }

                else if (currentTopic == "password")
                {
                    return ResponseHandler.GetPasswordResponse();
                }

                else if (currentTopic == "malware")
                {
                    return ResponseHandler.GetMalwareResponse();
                }

                else
                {
                    return "Please ask about a cybersecurity topic first.";
                }
            }

            // Goodbye
            else if (input.Contains("bye"))
            {
                return "Goodbye. Stay safe online.";
            }

            else
            {
                return "I do not understand your request. Please ask about phishing, malware, passwords or safe browsing.";
            }
        }
    }
}