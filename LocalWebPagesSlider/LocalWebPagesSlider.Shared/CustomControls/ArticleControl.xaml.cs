using LocalWebPagesSlider.DataModel;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace LocalWebPagesSlider.CustomControls
{
    public sealed partial class ArticleControl : UserControl
    {
        public ArticleControl()
        {
            this.InitializeComponent();
        }


        #region Properties

        public static readonly DependencyProperty WebPageProperty =
            DependencyProperty.Register("WebPage", typeof(WebPageItem), typeof(ArticleControl), new PropertyMetadata(default(WebPageItem), WebPagePropertyChanged));

        private static void WebPagePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ArticleControl currentControl = d as ArticleControl;
            currentControl.Start();
        }

        public WebPageItem WebPage
        {
            get { return (WebPageItem)GetValue(WebPageProperty); }
            set { SetValue(WebPageProperty, value); }
        }

        #endregion


        private void Start()
        {
            ShowPage();
        }

        private void ShowPage()
        {
            SwitchLoading(true);

            LoadPage();
        }

        private void LoadPage()
        {
            if (WebPage == null) return;

            if (DataLoader.WebStream == null)
            {
                return;
            }

            Uri url = web.BuildLocalStreamUri(StreamUriWinRTResolver.WEBVIEW_TAG, "/" + WebPage.FileName);
            web.NavigateToLocalStreamUri(url, DataLoader.WebStream);
        }

        private void SwitchLoading(bool isLoading)
        {
            if (isLoading)
            {
                pageLoading.Visibility = Windows.UI.Xaml.Visibility.Visible;
                web.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }
            else
            {
                pageLoading.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                web.Visibility = Windows.UI.Xaml.Visibility.Visible;
            }
        }

        private void web_LoadCompleted(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            SwitchLoading(false);
        }
    }
}
