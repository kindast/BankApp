using System.Windows.Controls;
using System.Windows.Media;

namespace BankApp.UserControls
{
    /// <summary>
    /// Interaction logic for MenuButton.xaml
    /// </summary>
    public partial class MenuButton : UserControl
    {
        public MenuButton()
        {
            InitializeComponent();
            DataContext = this;
        }
        public string Text { get; set; }
        public ImageSource Source { get; set; }
        //public bool Active
        //{
        //    get => activeBar.Visibility == Visibility.Visible ? true : false;
        //    set
        //    {
        //        if (value)
        //            activeBar.Visibility = Visibility.Visible;
        //        else
        //        {
        //            activeBar.Visibility = Visibility.Collapsed;
        //            Opacity = 0.5;
        //        }
        //    }
        //}
    }
}
