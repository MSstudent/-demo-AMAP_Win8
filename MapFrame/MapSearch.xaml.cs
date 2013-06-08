using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace Win8.Map.Demo
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MapSearch : Win8.Map.Demo.Common.LayoutAwarePage
    {
        public ObservableCollection<MapFrame.MainPage.Recording> MyMap = new ObservableCollection<MapFrame.MainPage.Recording>();
        public MapSearch()
        {
            this.InitializeComponent();     
            MyMap.Add(new MapFrame.MainPage.Recording("PoiSearch", "POI 搜索示例"));
            MyMap.Add(new MapFrame.MainPage.Recording("CenterLngLatAroundSearch", "中心点坐标周边搜索"));
            MyMap.Add(new MapFrame.MainPage.Recording("CenterWordAroundSearch", "中心点关键字周边搜索"));
            MyMap.Add(new MapFrame.MainPage.Recording("RegionkeywordSearch", "区域关键字搜索"));
            MyMap.Add(new MapFrame.MainPage.Recording("Geocode", "地理编码"));
            MyMap.Add(new MapFrame.MainPage.Recording("RegeocodeDemo", "逆地理编码"));
            MyMap.Add(new MapFrame.MainPage.Recording("RouteSearchofBusLine", "公交导航"));
            MyMap.Add(new MapFrame.MainPage.Recording("BusIDSearch", "公交ID 查询"));
            MyMap.Add(new MapFrame.MainPage.Recording("BusSearchofLine", "公交线路名称查询"));
            MyMap.Add(new MapFrame.MainPage.Recording("BusSearchofStation", "公交站点名称查询"));
            MyMap.Add(new MapFrame.MainPage.Recording("RouteSearchofDriving", "驾车导航"));
            ItemListView.DataContext = MyMap;
            ItemListView.SelectedIndex = 0;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                String mapTitle = "Win8.Map.Demo.";

                //   int i = (sender as ListView).SelectedIndex;
                mapTitle += ((MapFrame.MainPage.Recording)ItemListView.SelectedItem).Author;
                Object o = Activator.CreateInstance(Type.GetType(mapTitle));
                //  MapSample1 sp1 = new MapSample1();
                mapsample.Children.Add(o as UserControl);

            }
            else
            {
                // If the item was de-selected, clear the WebView.
                // ContentView.NavigateToString("");
            }

        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            var rootFrame = new Frame();
            rootFrame.Navigate(typeof(MapStarter));

            // Place the frame in the current Window and ensure that it is active
            Window.Current.Content = rootFrame;
            Window.Current.Activate();
        }
    }
}
