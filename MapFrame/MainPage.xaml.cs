using Com.AMap.Maps.Api;
using Com.AMap.Search.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Win8.Map.Demo;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MapFrame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Recording> MyMap = new ObservableCollection<Recording>();
        public MainPage()
        {
            this.InitializeComponent();
           
            MyMap.Add(new Recording("MapInit", "地图显示"));

            MyMap.Add(new Recording("MapCenter",
                "设置地图中心点"));
            MyMap.Add(new Recording("MapZoom",
               "修改地图当前级别"));

            MyMap.Add(new Recording("AddOverlays",
             "添加覆盖物"));

            MyMap.Add(new Recording("MapTip",
             "添加信息窗口"));

            MyMap.Add(new Recording("MapCustomOverlay",
             "添加自定义容器"));

            MyMap.Add(new Recording("MapAnchor",
             "地图锚点概念"));

            MyMap.Add(new Recording("MapAGeolocator",
             "适用于高德地图的定位"));

            MyMap.Add(new Recording("CurrentTraffic",
              "实时交通"));
          
            ItemListView.DataContext = MyMap;

            ItemListView.SelectedIndex = 0;

        }

        public class Recording
        {
            public Recording() { }

            public Recording(string artistName, string mapName)
            {
                Author = artistName;
                Title = mapName;
            }

            public string Author { get; set; }
            public string Title { get; set; }

            // Override the ToString method.
            public override string ToString()
            {
                return Title + " by " + Author + ", Released: " ;
            }
        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void ItemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                String mapTitle = "Win8.Map.Demo.";

             //   int i = (sender as ListView).SelectedIndex;
                mapTitle += ((Recording)ItemListView.SelectedItem).Author;
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
