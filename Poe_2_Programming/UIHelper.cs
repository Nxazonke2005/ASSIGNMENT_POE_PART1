using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace CyberSecurityAwarenessBot
{
    public class UIHelper
    {
        public static Border CreateBotMessage(string message)
        {
            Border border = new Border();

            border.Background = Brushes.DarkSlateBlue;
            border.CornerRadius = new CornerRadius(10);
            border.Padding = new Thickness(10);
            border.Margin = new Thickness(5);

            TextBlock text = new TextBlock();

            text.Text = message;
            text.Foreground = Brushes.White;
            text.FontSize = 15;
            text.TextWrapping = TextWrapping.Wrap;

            border.Child = text;

            return border;
        }

        public static Border CreateUserMessage(string message)
        {
            Border border = new Border();

            border.Background = Brushes.DarkGray;
            border.CornerRadius = new CornerRadius(10);
            border.Padding = new Thickness(10);
            border.Margin = new Thickness(5);

            TextBlock text = new TextBlock();

            text.Text = message;
            text.Foreground = Brushes.Black;
            text.FontSize = 15;
            text.TextWrapping = TextWrapping.Wrap;

            border.Child = text;

            return border;
        }
    }
}