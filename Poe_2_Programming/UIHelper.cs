using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace CyberSecurityAwarenessBot
{
    public class UIHelper
    {
        // Creates chatbot message design
        public static Border CreateBotMessage(string message)
        {
            // Creates border
            Border border = new Border();

            // Bot message background color
            border.Background = Brushes.DarkSlateBlue;

            // Rounded corners
            border.CornerRadius = new CornerRadius(10);

            // Inner spacing
            border.Padding = new Thickness(10);

            // Outer spacing
            border.Margin = new Thickness(5);

            // Creates text block
            TextBlock text = new TextBlock();

            // Adds BOT label
            text.Text = "BOT: " + message;

            // Text color
            text.Foreground = Brushes.White;

            // Font size
            text.FontSize = 15;

            // Wraps long text
            text.TextWrapping = TextWrapping.Wrap;

            // Places text inside border
            border.Child = text;

            return border;
        }

        // Creates user message design
        public static Border CreateUserMessage(string message)
        {
            Border border = new Border();

            // User background color
            border.Background = Brushes.DarkGray;

            border.CornerRadius = new CornerRadius(10);

            border.Padding = new Thickness(10);

            border.Margin = new Thickness(5);

            TextBlock text = new TextBlock();

            // Adds YOU label
            text.Text = "YOU: " + message;

            // Text color
            text.Foreground = Brushes.Black;

            text.FontSize = 15;

            text.TextWrapping = TextWrapping.Wrap;

            border.Child = text;

            return border;
        }
    }
}