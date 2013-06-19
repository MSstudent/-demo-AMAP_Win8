using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Com.AMap.Api.Maps;
using Com.AMap.Api.Maps.Model;

namespace PhoneApp2.Samples
{
    /// <summary>
    /// 自定义信息窗体
    /// </summary>
    public partial class CustomerInfoWindow : PhoneApplicationPage
    {
        AMap amap;
        AMapMarker marker;

        public CustomerInfoWindow()
        {
            InitializeComponent();
            this.ContentPanel.Children.Add(amap = new AMap());
            amap.Loaded += amap_Loaded;
        }

        private void amap_Loaded(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(() =>
           {
               //设置默认的地图经纬度和缩放级别
               amap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(39.987326, 116.48236), 13));

               ////实例化标注点
               AMapMarkerOptions opt = new AMapMarkerOptions()
               {
                   Position = new LatLng(39.987326, 116.48236),
                   Title = "这是一个marker",
                   IconUri = new Uri("Images/AZURE.png", UriKind.Relative),
                   Anchor = new Point(0.5, 0.5),//图标中心点
               };

               //添加点
               marker = amap.AddMarker(opt);
               amap.MarkerClickListener += amap_MarkerClickListener;
           });
           
        }

        void amap_MarkerClickListener(object sender, AMapEventArgs e)
        {
            //显示化弹出信息
            AInfoWindow info = new AInfoWindow();
            info.Title = "这是自定义信息窗口";
            info.ContentText = "高德软件有限公司";
            marker.ShowInfoWindow(info, new Point(0, 0));
        }

    }
}