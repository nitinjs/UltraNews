using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Nitin.News.BL;
using Nitin.News.Controls;

namespace Nitin.News.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Cursor = Cursors.Wait;
            this.ContentRendered += MainWindow_ContentRendered;
            //new LocationManager().GetLocation();
        }

        void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            HomeUserControl hp = new HomeUserControl(pageTransitionControl);
            pageTransitionControl.ShowPage(hp);           
        }
    }
}
