using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberSecurityAwarenessBot
{
    public partial class MainWindow : Window
    {
        // Creates chatbot object
        private Chatbot chatbot = new Chatbot();

        // Creates user object
        private User currentUser = new User();

        // Constructor
        public MainWindow()
        {
            InitializeComponent();

            // Plays greeting sound
            AudioPlayer.PlayGreeting();

            // Bot asks for name ONLY ONCE here
            AddBotMessage("Hello! What is your name?");
        }

        // Runs when SEND MESSAGE button is clicked
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            // Gets text from textbox
            string userInput = txtMessage.Text.Trim();

            // Prevent empty messages
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return;
            }

            // Shows user message
            AddUserMessage(userInput);

            // ------------------------------------------------
            // ASK FOR NAME ONLY ONCE
            // ------------------------------------------------
            // If name is empty, save first message as username
            if (string.IsNullOrWhiteSpace(currentUser.Name))
            {
                // Save name
                currentUser.Name = userInput;

                // Welcome user
                AddBotMessage("Welcome " + currentUser.Name + ".");

                // Explain chatbot purpose
                AddBotMessage(
                    "You can ask me anything about passwords, phishing, malware, scams or online safety."
                );

                // Clear textbox
                txtMessage.Clear();

                // STOP method here
                return;
            }

            // ------------------------------------------------
            // NORMAL CHATBOT RESPONSES
            // ------------------------------------------------

            // Gets chatbot response
            string response = chatbot.GetResponse(userInput);

            // Displays bot response
            AddBotMessage(response);

            // Clears textbox
            txtMessage.Clear();
        }

        // ------------------------------------------------
        // BOT MESSAGE DESIGN
        // ------------------------------------------------
        private void AddBotMessage(string message)
        {
            // Creates border
            Border border = new Border();

            // Background color
            border.Background = new SolidColorBrush(Color.FromRgb(44, 62, 80));

            // Rounded corners
            border.CornerRadius = new CornerRadius(10);

            // Inner spacing
            border.Padding = new Thickness(10);

            // Outer spacing
            border.Margin = new Thickness(5);

            // Creates text
            TextBlock text = new TextBlock();

            // Adds BOT label
            text.Text = "BOT: " + message;

            // Text color
            text.Foreground = Brushes.LightBlue;

            // Text size
            text.FontSize = 16;

            // Wrap long text
            text.TextWrapping = TextWrapping.Wrap;

            // Places text inside border
            border.Child = text;

            // Adds message to panel
            ChatPanel.Children.Add(border);
        }

        // ------------------------------------------------
        // USER MESSAGE DESIGN
        // ------------------------------------------------
        private void AddUserMessage(string message)
        {
            // Creates border
            Border border = new Border();

            // Background color
            border.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));

            // Rounded corners
            border.CornerRadius = new CornerRadius(10);

            // Inner spacing
            border.Padding = new Thickness(10);

            // Outer spacing
            border.Margin = new Thickness(5);

            // Creates text
            TextBlock text = new TextBlock();

            // Adds YOU label
            text.Text = "YOU: " + message;

            // Text color
            text.Foreground = Brushes.White;

            // Text size
            text.FontSize = 16;

            // Wrap long text
            text.TextWrapping = TextWrapping.Wrap;

            // Places text inside border
            border.Child = text;

            // Adds message to panel
            ChatPanel.Children.Add(border);
        }
    }
}