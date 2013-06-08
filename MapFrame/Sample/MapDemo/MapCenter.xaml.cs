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
    public sealed partial class MapCenter : UserControl
    {
        AMap map = new AMap();
        public MapCenter()
        {
            this.InitializeComponent();
            contentGrid.Children.Add(map);
            Canvas.SetZIndex(showMapBorder,10);
        }

        /// <summary>
        /// 设置地图中心点
        /// </summary>
        public void SetMapCenter(object sender, RoutedEventArgs e)
        {
            map.Center = new ALngLat(116.397428, 39.90923);
        }
        /// <summary>
        /// 获取地图中心点
        /// </summary>
        public async void GetMapCenter(object sender, RoutedEventArgs e)
        {
            var messageDialog = new MessageDialog("地图中心点坐标" + map.Center);
            await messageDialog.ShowAsync();
        }

    }
}
