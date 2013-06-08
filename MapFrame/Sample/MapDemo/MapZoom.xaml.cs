using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.BaseTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class MapZoom : UserControl
    {
        AMap map = new AMap();
        public MapZoom()
        {
            this.InitializeComponent();
            contentGrid.Children.Add(map);
            Canvas.SetZIndex(showMapBorder,10);
        }

        /// <summary>
        /// 设置地图级别
        /// </summary>
        public void SetMapZoom(object sender, RoutedEventArgs e)
        {
            map.Zoom = map.Zoom - 1;
        }
        /// <summary>
        /// 获取地图级别
        /// </summary>
        public async void GetMapZoom(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("地图级别：" + map.Zoom);
            await messageDialog.ShowAsync();
        }

    }
}
