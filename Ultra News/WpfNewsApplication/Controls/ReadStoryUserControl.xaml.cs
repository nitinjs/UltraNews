using Nitin.News.DAL.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
using mshtml;

namespace Nitin.News.Controls
{
    /// <summary>
    /// Interaction logic for ReadStoryUserControl.xaml
    /// </summary>
    public partial class ReadStoryUserControl : UserControl
    {
        public WpfPageTransitions.PageTransition ParentControl
        {
            get;
            set;
        }

        public ReadStoryUserControl(PageTransition transitioncontrol, Article article)
        {
            InitializeComponent();
            this.ParentControl = transitioncontrol;

            wbArticleViewer.Navigated += new NavigatedEventHandler(wbMain_Navigated);

            //LEFT
            //Story_Left_1.ParentControl = ParentControl;
            //Story_Left_2.ParentControl = ParentControl;
            //Story_Left_3.ParentControl = ParentControl;
            //Story_Left_4.ParentControl = ParentControl;
            //Story_Left_5.ParentControl = ParentControl;
            //Story_Left_6.ParentControl = ParentControl;
            //Story_Left_7.ParentControl = ParentControl;
            //Story_Left_8.ParentControl = ParentControl;

            if (article.url != null)
                wbArticleViewer.Source = new Uri(article.url);

            txtTitle.Text = article.title.Substring(0, article.title.LastIndexOf('('));
            txtDate.Text = "Date: " + Convert.ToDateTime(article.publish_date).ToString("dd-MM-yyyy, hh:mm:ss");
            this.Cursor = Cursors.Arrow;
            lblLocation.Content = Properties.Settings.Default.LocationName;

        }

        void wbMain_Navigated(object sender, NavigationEventArgs e)
        {
            SetSilent(wbArticleViewer, true); // make it silent
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            ParentControl.pages.Clear();
            this.Cursor = Cursors.Wait;
            HomeUserControl hp = new HomeUserControl(ParentControl);
            ParentControl.ShowPage(hp);
        }

        [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IOleServiceProvider
        {
            [PreserveSig]
            int QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
        }

        public static void SetSilent(WebBrowser browser, bool silent)
        {
            if (browser == null)
                throw new ArgumentNullException("browser");

            // get an IWebBrowser2 from the document
            IOleServiceProvider sp = browser.Document as IOleServiceProvider;
            if (sp != null)
            {
                Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");

                object webBrowser;
                sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out webBrowser);
                if (webBrowser != null)
                {
                    webBrowser.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser, new object[] { silent });
                }
            }
        }
    }
}
