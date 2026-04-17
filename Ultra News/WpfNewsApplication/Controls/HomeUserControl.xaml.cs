using Nitin.News.BL;
using Nitin.News.DAL.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfPageTransitions;
using System.Device;
using Nitin.News.UI;

namespace Nitin.News.Controls
{
    /// <summary>
    /// Interaction logic for HomeUserControl.xaml
    /// </summary>
    public partial class HomeUserControl : UserControl
    {
        private Timer clockTimer;
        LocationManager loc;

        public WpfPageTransitions.PageTransition ParentControl
        {
            get;
            set;
        }


        public HomeUserControl(PageTransition _ParentControl)
        {
            InitializeComponent();
            this.Cursor = Cursors.Wait;
            loc = new LocationManager();

            this.ParentControl = _ParentControl;
            try
            {
                //location
                if (Properties.Settings.Default.LocationMode == "gps")
                {
                    Properties.Settings.Default.Location = string.Format("{0}, {1}",
                    loc.CurrentLocation.Latitude,
                    loc.CurrentLocation.Longitude);

                    if (Properties.Settings.Default.LocationName == "")
                    {
                        if (loc.CurrentLocation.IsUnknown)
                        {
                            //load defaults
                            //set mode to manual
                            Properties.Settings.Default.LocationMode = "manual";
                            Properties.Settings.Default.Location = "NaN, NaN";
                            Properties.Settings.Default.LocationName = "New York, USA (default)";
                        }
                        else
                        {
                            Properties.Settings.Default.LocationMode = "gps";
                            Properties.Settings.Default.Location = string.Format("{0}, {1}",
                            loc.CurrentLocation.Latitude,
                            loc.CurrentLocation.Longitude);

                            //get location name from web
                            Properties.Settings.Default.LocationName = loc.LocationName;

                            MessageBox.Show("Detected location: " + Properties.Settings.Default.LocationName);

                        }
                        Properties.Settings.Default.Save();
                    }
                }
                lblLocation.Content = Properties.Settings.Default.LocationName;

                //datetime
                lblDateTime.Content = DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss");

                //cultures
                //cmbCulture.ItemsSource = null;
                //var cultures = Feedzilla.Cultures();
                //cmbCulture.ItemsSource = cultures;
                //cmbCulture.SelectedIndex = Properties.Settings.Default.cmbCulture_SelectedIndex;

                //categories
                lstCategories.ItemsSource = null;
                var cats = Feedzilla.Categories("en_all", Feedzilla.CultureOrder.Popular);
                lstCategories.ItemsSource = cats;


                //each storycontrol
                var x = this.FindChildren<StoryUserControl>(i => i.GetType() == typeof(StoryUserControl));
                foreach (StoryUserControl ctrl in x)
                {
                    ctrl.ParentControl = _ParentControl;
                }

                clockTimer = new Timer(1000);
                clockTimer.Elapsed += new ElapsedEventHandler(clockTimer_Elapsed);
                clockTimer.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please ensure working internet connection and GPS device!\n" + ex.Message);
                Application.Current.Shutdown();
            }
            this.Cursor = Cursors.Arrow;
        }

        void clockTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(UpdateCurrentDateTime));
        }

        //public string CurrentDateTime
        //{
        //    get { return (string)GetValue(CurrentDateTimeProperty); }
        //    set { SetValue(CurrentDateTimeProperty, value); }
        //}

        public static readonly DependencyProperty CurrentDateTimeProperty =
            DependencyProperty.Register("CurrentDateTime", typeof(string), typeof(MainWindow), new UIPropertyMetadata(string.Empty));

        private void UpdateCurrentDateTime()
        {
            lblDateTime.Content = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
            //CurrentDateTime = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
        }

        private void lstCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            if (lstCategories.SelectedIndex != -1)
            {
                //load 34 articles + 2 top articles in right pane

                //load from feedzila
                Category cat = (Category)lstCategories.SelectedItem;

                //each storycontrol
                IEnumerable<Article> articles = null;
                if (lstCategories.SelectedIndex != 0)
                {
                    articles = Feedzilla.Articles(cat, DateTime.Now.AddDays(-2), "", Feedzilla.ArticleOrder.Relevance, 44, false);

                }
                else
                {
                    articles = Feedzilla.Articles(lblLocation.Content.ToString(), DateTime.Now.AddDays(-2), "", Feedzilla.ArticleOrder.Relevance, 44, false);

                }
                var stories = (from p in this.FindChildren<StoryUserControl>(i => i.GetType() == typeof(StoryUserControl))
                               orderby Convert.ToInt32(p.Tag) ascending
                               select p).Distinct(new StoryComparer()).ToList<StoryUserControl>();

                //foreach (var x in stories) { Console.WriteLine(x.Tag); }

                for (int count = 0; count < stories.Count(); count++)
                {
                    StoryUserControl ctrl = stories.ElementAt(count);
                    //foreach(Article a in articles)
                    //{
                    if (count >= articles.Count())
                    {
                        ctrl.LoadArticle(null);
                        //ctrl.Visibility = System.Windows.Visibility.Hidden;
                        //break;
                    }
                    else
                    {
                        var a = articles.ElementAt(count);
                        ctrl.LoadArticle(a);
                        Console.WriteLine(count + "-" + a.title);
                        this.InvalidateVisual();
                        //ctrl.Visibility = System.Windows.Visibility.Visible;
                    }
                    //}
                }

            }
            this.Cursor = Cursors.Arrow;
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow wn = new SettingsWindow(loc);
            wn.ShowDialog();
            if (wn.IsCancelled == false)
            {
                lblLocation.Content = Properties.Settings.Default.LocationName;
                if (MessageBox.Show("Refresh news from the location?", "Refresh News", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    this.Cursor = Cursors.Wait;
                    lstCategories_SelectionChanged(sender, null);
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        //private void cmbCulture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        Properties.Settings.Default.cmbCulture_SelectedIndex = cmbCulture.SelectedIndex;
        //        Properties.Settings.Default.Save();

        //        Culture c = (Culture)cmbCulture.SelectedItem;
        //        lstCategories.ItemsSource = null;
        //        var cats = Feedzilla.Categories(c.culture_code, Feedzilla.CultureOrder.Popular);
        //        lstCategories.ItemsSource = cats;

        //        lstCategories_SelectionChanged(sender, e);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }

    public class StoryComparer : IEqualityComparer<StoryUserControl>
    {

        public bool Equals(StoryUserControl x, StoryUserControl y)
        {
            return Convert.ToInt32(x.Tag) == Convert.ToInt32(y.Tag);
        }

        public int GetHashCode(StoryUserControl obj)
        {
            return obj.GetHashCode();
        }
    }
}
