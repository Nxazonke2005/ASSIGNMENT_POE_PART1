using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CyberSecurityAwarenessBot
{
    public partial class MainWindow : Window
    {
        // Creates chatbot instance
        private Chatbot chatbot;

        public MainWindow()
        {
            InitializeComponent();
            chatbot = new Chatbot();

            // Shows welcome message when application starts
            ShowWelcomeMessage();

            // Sets focus to input box
            UserInputTextBox.Focus();
        }

        // Displays welcome message with available commands
        private void ShowWelcomeMessage()
        {
            string welcomeMessage = "Hello! I'm your cybersecurity assistant." + Environment.NewLine + Environment.NewLine +
                                   "You can ask me about:" + Environment.NewLine +
                                   "- Passwords (strong passwords, examples, tips)" + Environment.NewLine +
                                   "- Phishing (scams, fake emails, protection)" + Environment.NewLine +
                                   "- Malware (viruses, trojans, protection)" + Environment.NewLine +
                                   "- Online safety (safe browsing, privacy)" + Environment.NewLine + Environment.NewLine +
                                   "Try asking:" + Environment.NewLine +
                                   "- 'Tell me about phishing'" + Environment.NewLine +
                                   "- 'I'm worried about malware'" + Environment.NewLine +
                                   "- 'Give me a password tip'" + Environment.NewLine +
                                   "- 'Tell me more' (after any topic)";

            AddBotMessage(welcomeMessage);
        }

        // Handles send button click event
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendUserMessage();
        }

        // Handles Enter key press in text box
        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendUserMessage();
                e.Handled = true;
            }
        }

        // Processes user input and gets bot response
        private void SendUserMessage()
        {
            string userInput = UserInputTextBox.Text.Trim();

            // Prevents sending empty messages
            if (string.IsNullOrWhiteSpace(userInput))
                return;

            // Adds user message to chat display
            AddUserMessage(userInput);

            // Clears input box for next message
            UserInputTextBox.Clear();

            // Gets response from chatbot logic
            string botResponse = chatbot.GetResponse(userInput);

            // Adds bot response to chat display
            AddBotMessage(botResponse);

            // Scrolls to show latest messages
            ScrollToBottom();

            // Returns focus to input box for next message
            UserInputTextBox.Focus();
        }

        // Creates and adds user message bubble
        private void AddUserMessage(string message)
        {
            Border userBorder = UIHelper.CreateUserMessage(message);
            ChatStackPanel.Children.Add(userBorder);
        }

        // Creates and adds bot message bubble
        private void AddBotMessage(string message)
        {
            Border botBorder = UIHelper.CreateBotMessage(message);
            ChatStackPanel.Children.Add(botBorder);
        }

        // Scrolls chat viewer to bottom
        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToBottom();
        }
    }
}