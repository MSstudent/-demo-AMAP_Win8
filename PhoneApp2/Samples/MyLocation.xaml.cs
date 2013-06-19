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
using Windows.UI.Core;
using System.Diagnostics;
using System.Windows.Media;

namespace PhoneApp2.Samples
{
    /// <summary>
    /// 定位功能
    /// </summary>
    public partial class MyLocation : PhoneApplicationPage
    {
        AMap amap;
        //标注点
        AMapMarker marker;
        //圆
        AMapCircle circle;
        AMapGeolocator mylocation;

        public MyLocation()
        {
            InitializeComponent();
            //添加地图控件
            this.ContentPanel.Children.Add(amap = new AMap());

            amap.Loaded += amap_Loaded;
        }

        private void amap_Loaded(object sender, RoutedEventArgs e)
        {
            //设置地图默认的经纬度和缩放级别
            amap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(39.90923, 116.397428), 13));

            //实例化高德地图的定位接口类
            mylocation = new AMapGeolocator();
            //触发位置改变事件
            mylocation.PositionChanged += mylocation_PositionChanged;
       
        }

        void mylocation_PositionChanged(AMapGeolocator sender, AMapPositionChangedEventArgs args)
        {
            this.Dispatcher.BeginInvoke(() =>
            {
                if (marker == null)
                {
                    //添加圆
                    circle = amap.AddCircle(new AMapCircleOptions()
                     {
                         Center = args.LngLat,//圆点位置
                         Radius = (float)args.Accuracy,//半径
                         FillColor = Color.FromArgb(80, 100, 150, 255),
                         StrokeWidth = 2,//边框粗细
                         StrokeColor = Color.FromArgb(80, 0, 0, 255),//边框颜色

                     });

                    //添加点标注，用于标注地图上的点
                    marker = amap.AddMarker(
                   new AMapMarkerOptions()
                   {
                       Position = args.LngLat,//图标的位置
                       Title = "我的位置",
                       IconUri = new Uri("Images/marker_gps_no_sharing.png", UriKind.Relative),//图标的URL
                       Anchor = new Point(0.5, 0.5),//图标中心点

                   });
                }
                else
                {
                    //点标注和圆的位置在当前经纬度
                    marker.Position = args.LngLat;
                    circle.Center = args.LngLat;
                    circle.Radius = (float)args.Accuracy;//圆半径
                }

                //设置当前地图的经纬度和缩放级别
                amap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(args.LngLat, 15));
                Debug.WriteLine("定位精度：" + args.Accuracy + "米");
                Debug.WriteLine("定位经纬度：" + args.LngLat);
            });
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            mylocation.PositionChanged -= mylocation_PositionChanged;
            base.OnBackKeyPress(e);

        }
    }
}