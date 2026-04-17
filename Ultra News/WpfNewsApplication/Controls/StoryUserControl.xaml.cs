using Nitin.News.DAL.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WpfPageTransitions;

namespace Nitin.News.Controls
{
    /// <summary>
    /// Interaction logic for StoryUserControl.xaml
    /// </summary>
    public partial class StoryUserControl : UserControl
    {
        public void LoadArticle(Article article)
        {
            if (article != null)
            {
                //get favicon from the URL

                //set title
                try
                {
                    txtTitle.Text = article.title.Substring(0, article.title.LastIndexOf('('));
                }
                catch
                {
                    txtTitle.Text = article.title;
                }

                //set content
                txtDescription.Text = article.summary;

                //just for passing URL to other control
                brdMain.Tag = article;
                //spSeparator.Background = new SolidColorBrush(Colors.Black);
                this.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        public WpfPageTransitions.PageTransition ParentControl
        {
            get;
            set;
        }

        public StoryUserControl()
        {
            InitializeComponent();
            this.MouseDoubleClick += StoryUserControl_MouseDoubleClick;
            this.MouseEnter += StoryUserControl_MouseEnter;
            this.MouseLeave += StoryUserControl_MouseLeave;
        }

        void StoryUserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            BorderBrush = new SolidColorBrush(Colors.White);
            Background = new SolidColorBrush(Colors.White);
        }

        void StoryUserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            //  <Style.Triggers>
            //        <Trigger Property="local:StoryUserControl.IsFilled" Value="True">
            //            <Setter Property="local:StoryUserControl.BorderThickness" Value="1"></Setter>
            //            <Setter Property="local:StoryUserControl.BorderBrush" Value="LightBlue"></Setter>
            //            <Setter Property="local:StoryUserControl.Background" Value="WhiteSmoke"/>
            //        </Trigger>
            //   </Style.Triggers>
            if (!string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                BorderThickness = new Thickness(1);
                BorderBrush = new SolidColorBrush(Colors.LightBlue);
                Background = new SolidColorBrush(Colors.WhiteSmoke);
            }
        }

        void StoryUserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Tag != null)
            {
                this.Cursor = Cursors.Wait;
                ReadStoryUserControl ctrlStory = new ReadStoryUserControl(ParentControl, (Article)brdMain.Tag);
                ParentControl.ShowPage(ctrlStory);
            }
        }

        public double HeightX { get { return brdMain.Height; } set { brdMain.Height = value; } }
        public double WidthX { get { return brdMain.Width; } set { brdMain.Width = value; } }

        private DateTime _TapTime = DateTime.Now;
        private DateTime TapTime
        {
            get
            {
                return _TapTime;
            }
            set
            {
                PreviousTapTime = _TapTime;
                _TapTime = value;
            }
        }

        private DateTime PreviousTapTime { get; set; }

        public bool IsDoubleTap
        {
            get
            {
                TimeSpan t = TapTime - PreviousTapTime;
                TimeSpan t2 = new TimeSpan(2743100);
                //txtDescription.Text += TapTime.ToLongTimeString() + "- Interval:"+t.Ticks.ToString() + "\n";
                return (t <= t2);
            }
        }

        private void UserControl_TouchDown(object sender, TouchEventArgs e)
        {
            base.OnTouchDown(e);

            //check the diff between taps
            TapTime = DateTime.Now;

            if (IsDoubleTap)
            {
                //MessageBox.Show("Double Tap");
                StoryUserControl_MouseDoubleClick(this, null);
            }
        }
    }
}
