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
    /// 手势操作
    /// </summary>
    public partial class Gestures  : PhoneApplicationPage
    {
        AMap amap;
        UiSettings uiset;
        public Gestures()
        {
            InitializeComponent();
            this.ContentPanel.Children.Add(amap = new AMap());
            amap.Loaded += amap_Loaded;
            uiset = amap.GetUiSettings();
            uiset.AllGesturesEnabled = false;//禁用所有交互
            uiset.ZoomControlsEnabled = false;//隐藏缩放图标
            chkBoxScroll.IsChecked = true;
            chkBoxZoomicon.IsChecked = true;

        }

        private void amap_Loaded(object sender, RoutedEventArgs e)
        {
            //设置默认的地图经纬度和缩放级别
            amap.MoveCamera(CameraUpdateFactory.NewCameraPosition(new LatLng(39.90923, 116.397428), 13, 45, 30));
        }

        private void rotateEnable(object sender, RoutedEventArgs e)
        {
            //允许旋转
            uiset.RotateGesturesEnabled = true;
        }

        private void scrollEnable(object sender, RoutedEventArgs e)
        {
            //允许移动
            uiset.ScrollGesturesEnabled = true;
        }

        private void tiltEnable(object sender, RoutedEventArgs e)
        {
            //允许倾斜
            uiset.TiltGesturesEnabled = true;

        }

        private void zoomEnable(object sender, RoutedEventArgs e)
        {
            //允许缩放
            uiset.ZoomGesturesEnabled = true;
        }

        private void allEnable(object sender, RoutedEventArgs e)
        {
            //允许所有手势操作
            uiset.AllGesturesEnabled = true;
        }

        private void unrotateEnable(object sender, RoutedEventArgs e)
        {
            //不允许旋转
            uiset.RotateGesturesEnabled = false;
        }

        private void unscrollEnable(object sender, RoutedEventArgs e)
        {
            //不允许移动
            uiset.ScrollGesturesEnabled = false;
        }

        private void untiltEnable(object sender, RoutedEventArgs e)
        {
            //不允许倾斜
            uiset.TiltGesturesEnabled = false;
        }

        private void unzoomEnable(object sender, RoutedEventArgs e)
        {
            //不允许缩放
            uiset.ZoomGesturesEnabled = false;
        }

        private void zoomiconEnable(object sender, RoutedEventArgs e)
        {
            //显示缩放按钮
            uiset.ZoomControlsEnabled = true;
        }

        private void zoomiconDisenable(object sender, RoutedEventArgs e)
        {
            //隐藏缩放按钮
            uiset.ZoomControlsEnabled = false;

        }
       
    }
}