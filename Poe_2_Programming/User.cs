namespace CyberSecurityAwarenessBot
{
    public class User
    {
        // Stores the user's name
        public string Name { get; set; }

        // Stores the user's interests
        public string Interest { get; set; }

        // Constructor
        public User()
        {
            // Default values
            Name = "";
            Interest = "";
        }
    }
}