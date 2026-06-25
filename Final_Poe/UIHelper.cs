using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberSecurityAwarenessBot
{
    public class UIHelper
    {
        public static Border CreateBotMessage(string message)
        {
            Border border = new Border();
            border.Background = new SolidColorBrush(Color.FromRgb(45, 79, 108));
            border.CornerRadius = new CornerRadius(10);
            border.Padding = new Thickness(10);
            border.Margin = new Thickness(5);

            TextBlock text = new TextBlock();
            text.Text = "BOT: " + message;
            text.Foreground = Brushes.White;
            text.FontSize = 15;
            text.TextWrapping = TextWrapping.Wrap;

            border.Child = text;
            return border;
        }

        public static Border CreateUserMessage(string message)
        {
            Border border = new Border();
            border.Background = new SolidColorBrush(Color.FromRgb(100, 100, 100));
            border.CornerRadius = new CornerRadius(10);
            border.Padding = new Thickness(10);
            border.Margin = new Thickness(5);

            TextBlock text = new TextBlock();
            text.Text = "YOU: " + message;
            text.Foreground = Brushes.Black;
            text.FontSize = 15;
            text.TextWrapping = TextWrapping.Wrap;

            border.Child = text;
            return border;
        }
    }
}