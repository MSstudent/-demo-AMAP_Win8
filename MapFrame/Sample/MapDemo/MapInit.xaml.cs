using Com.AMap.Maps.Api;
using Com.AMap.Search.API.Options;
using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Win8.Map.Demo
{
    /// <summary>
    /// 地图初始化
    /// </summary>
    public sealed partial class MapInit : UserControl
    {
        /// <summary>
        /// 地图初始化
        /// </summary>
        AMap map = new AMap();
        public MapInit()
        {
            this.InitializeComponent();
            mapGrid.Children.Add(map);
        }

        private void SatelliteLayer(object sender, RoutedEventArgs e)
        {
            map.MapType = MapType.Aerial;
        }
        private void RouteLayer(object sender, RoutedEventArgs e)
        {
            map.MapType = MapType.Road;
        }

    }
}
