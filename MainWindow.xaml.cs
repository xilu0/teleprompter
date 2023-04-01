using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Teleprompter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void OpacitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                Opacity = OpacitySlider.Value;
            }
        }

        private void FontSizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsInitialized)
            {
                TeleprompterText.FontSize = FontSizeSlider.Value;
            }
        }

        private void TextColorComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (!IsInitialized) return;

            switch (TextColorComboBox.SelectedIndex)
            {
                case 0:
                    TeleprompterText.Foreground = new SolidColorBrush(Colors.White);
                    break;
                case 1:
                    TeleprompterText.Foreground = new SolidColorBrush(Colors.Black);
                    break;
                case 2:
                    TeleprompterText.Foreground = new SolidColorBrush(Colors.Red);
                    break;
                case 3:
                    TeleprompterText.Foreground = new SolidColorBrush(Colors.Blue);
                    break;
                case 4:
                    TeleprompterText.Foreground = new SolidColorBrush(Colors.Green);
                    break;
            }
        }

    }
}
