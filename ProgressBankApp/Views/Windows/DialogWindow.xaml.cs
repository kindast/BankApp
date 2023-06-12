using System.Windows;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public bool Result { get; set; } = false;

        public static void Show(string text, string title = "Внимание")
        {
            DialogWindow window = new DialogWindow();
            window.Title = title;
            window.tbText.Text = text;
            window.ShowDialog();
        }

        public static bool ShowYesNo(string text, string title = "Внимание")
        {
            DialogWindow window = new DialogWindow();
            window.Title = title;
            window.tbText.Text = text;
            window.spYesNo.Visibility = Visibility.Visible;
            window.ShowDialog();

            return window.Result;
        }

        public static void ShowDevelopmentMessage()
        {
            DialogWindow window = new DialogWindow();
            window.Title = "Внимание";
            window.tbText.Text = "Данный функционал находится в разработке";
            window.ShowDialog();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }
    }
}
