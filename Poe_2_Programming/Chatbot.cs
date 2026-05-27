namespace CyberSecurityAwarenessBot
{
    public class Chatbot
    {
        public string GetResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("hello"))
            {
                return "Hello. How can I help you today?";
            }

            else if (input.Contains("phishing"))
            {
                return ResponseHandler.GetPhishingResponse();
            }

            else if (input.Contains("malware"))
            {
                return ResponseHandler.GetMalwareResponse();
            }

            else if (input.Contains("password "))
            {
                return ResponseHandler.GetPasswordResponse();
            }

            else if (input.Contains("safe browsing"))
            {
                return ResponseHandler.GetSafeBrowsingResponse();
            }

            else if (input.Contains("purpose"))
            {
                return ResponseHandler.GetPurposeResponse();
            }

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