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

namespace WindowsPhoneDemo.Samples
{
    /// <summary>
    /// 展示地图
    /// </summary>
    public partial class DisplayMap : PhoneApplicationPage
    {
        AMap amap;
        public DisplayMap()
        {
            InitializeComponent();
            //添加地图控件
            this.ContentPanel.Children.Add(amap = new AMap());
            //触发地图加载事件
            amap.Loaded += amap_Loaded;
        }

        /// <summary>
        /// 地图加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void amap_Loaded(object sender, RoutedEventArgs e)
        {
            //设置地图默认的经纬度和缩放级别
            amap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(39.90923, 116.397428), 13));
           
        }

    }
}