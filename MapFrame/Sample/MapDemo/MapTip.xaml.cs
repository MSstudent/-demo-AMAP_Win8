using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.BaseTypes;
using Com.AMap.Maps.Api.Overlays;
using Demo.CustomControls;
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
    /// <summary>
    /// 信息窗口示例
    /// </summary>
    public sealed partial class MapTip : UserControl
    {
        AMap map = new AMap();
        public MapTip()
        {
            this.InitializeComponent();
            contentGrid.Children.Add(map);
            Canvas.SetZIndex(showMapBorder,10);
        }

        /// <summary>
        /// 在地图上添加信息窗口
        /// </summary>
        public void AddTipOnMap(object sender, RoutedEventArgs e)
        {
            ATip tip = new ATip();
            tip.Title = "我是标题";
            tip.ContentText = "我是内容";
            map.OpenTip(map.Center,tip);
        }
        /// <summary>
        /// 在Marker上添加信息窗口
        /// </summary>
        public void AddTipOnMarker(object sender, RoutedEventArgs e)
        {
            ATip tip = new ATip();
            tip.Title = "我是标题";
            tip.ContentText = "我是内容";
            AMarker marker = new AMarker();
            marker.LngLat = map.Center;
            marker.TipFrameworkElement = tip;
            map.Children.Add(marker);
            marker.OpenTip();
        }
        /// <summary>
        /// 地图上添加自定义TIP容器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddCustomTipOnMap(object sender, RoutedEventArgs e)
        {
            //自定义容器
            CustomOverlayControl coo = new CustomOverlayControl();
            map.OpenTip(map.Center, coo);
        }

        /// <summary>
        /// MarkerTIP上添加自定义容器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddCustomTipOnMarker(object sender, RoutedEventArgs e)
        {
            //自定义容器
            CustomOverlayControl coo = new CustomOverlayControl();
            AMarker marker = new AMarker();
            marker.LngLat = map.Center;
            marker.TipFrameworkElement = coo;
            map.Children.Add(marker);
            marker.OpenTip();
        }

    }
}
