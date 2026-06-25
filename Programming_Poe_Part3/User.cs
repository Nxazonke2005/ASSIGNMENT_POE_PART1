namespace CyberSecurityAwarenessBot
{
    public class User
    {
        public string Name { get; set; }
        public string Interest { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        public User()
        {
            Name = "";
            Interest = "";
            Email = "";
            Age = 0;
        }
        // Constructor to initialize user with a name
        public User(string name)
        {
            Name = name;
            Interest = "";
            Email = "";
            Age = 0;
        }
        // Method to get a greeting message based on the user's name

        public string GetGreeting()
        {
            if (!string.IsNullOrEmpty(Name))
                return "Hello, " + Name + "!";
            return "Hello!";
        }

        public void SetUserInfo(string name, string interest = "", int age = 0, string email = "")
        {
            Name = name;
            Interest = interest;
            Age = age;
            Email = email;
        }
        // Method to get user information as a formatted string

        public string GetUserInfo()
        {
            string info = "User Information:" + Environment.NewLine;
            info += "Name: " + Name + Environment.NewLine;
            if (!string.IsNullOrEmpty(Interest))
                info += "Interest: " + Interest + Environment.NewLine;
            if (Age > 0)
                info += "Age: " + Age + Environment.NewLine;
            if (!string.IsNullOrEmpty(Email))
                info += "Email: " + Email;
            return info;
        }
    }
}