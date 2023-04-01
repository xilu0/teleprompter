using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Interop;
using System.Runtime.InteropServices;

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

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private void ResizeGrip_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            const int WM_SYSCOMMAND = 0x112;
            const int SC_SIZE = 0xF000;

            int sizeCode = 0;
            FrameworkElement grip = sender as FrameworkElement;

            if (grip == null)
                return;

            if (grip.Name == "BottomGrip")
                sizeCode = 6;
            else if (grip.Name == "RightGrip")
                sizeCode = 8;
            else if (grip.Name == "BottomRightGrip")
                sizeCode = 9;
            if (sizeCode != 0)
            {
                IntPtr hWnd = new WindowInteropHelper(this).Handle;
                SendMessage(hWnd, WM_SYSCOMMAND, (IntPtr)(SC_SIZE + sizeCode), IntPtr.Zero);
                e.Handled = true;
            }
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb == null) return;

            if (thumb.Cursor == Cursors.SizeNS || thumb.Cursor == Cursors.SizeNWSE)
            {
                double height = this.Height + e.VerticalChange;
                this.Height = Math.Max(this.MinHeight, Math.Min(height, this.MaxHeight));
            }

            if (thumb.Cursor == Cursors.SizeWE || thumb.Cursor == Cursors.SizeNWSE)
            {
                double width = this.Width + e.HorizontalChange;
                this.Width = Math.Max(this.MinWidth, Math.Min(width, this.MaxWidth));
            }
        }

    }
}