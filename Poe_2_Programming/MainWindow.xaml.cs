using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberSecurityAwarenessBot
{
    public partial class MainWindow : Window
    {
        private Chatbot chatbot = new Chatbot();
        private User currentUser = new User();

        public MainWindow()
        {
            InitializeComponent();

            AudioPlayer.PlayGreeting();

            AddBotMessage("Hello! What is your name?");
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string userInput = txtMessage.Text.Trim();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                return;
            }

            AddUserMessage(userInput);

            if (string.IsNullOrWhiteSpace(currentUser.Name))
            {
                currentUser.Name = userInput;

                AddBotMessage("Welcome " + currentUser.Name + ".");
                AddBotMessage("Ask me about phishing, malware, passwords, safe browsing or my purpose.");

                txtMessage.Clear();
                return;
            }

            string response = chatbot.GetResponse(userInput);

            AddBotMessage(response);

            txtMessage.Clear();
        }

        private void AddBotMessage(string message)
        {
            Border border = new Border();

            border.Background = new SolidColorBrush(Color.FromRgb(44, 62, 80));
            border.CornerRadius = new CornerRadius(10);
            border.Padding = new Thickness(10);
            border.Margin = new Thickness(5);

            TextBlock text = new TextBlock();

            text.Text = "BOT: " + message;
            text.Foreground = Brushes.LightBlue;
            text.FontSize = 16;
            text.TextWrapping = TextWrapping.Wrap;

            border.Child = text;

            ChatPanel.Children.Add(border);
        }

        private void AddUserMessage(string message)
        {
            Border border = new Border();

            border.Background = new SolidColorBrush(Color.FromRgb(60, 60, 60));
            border.CornerRadius = new CornerRadius(10);
            border.Padding = new Thickness(10);
            border.Margin = new Thickness(5);

            TextBlock text = new TextBlock();

            text.Text = "YOU: " + message;
            text.Foreground = Brushes.White;
            text.FontSize = 16;
            text.TextWrapping = TextWrapping.Wrap;

            border.Child = text;

            ChatPanel.Children.Add(border);
        }
    }
}