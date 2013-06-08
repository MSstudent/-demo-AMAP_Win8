using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.BaseTypes;
using Com.AMap.Maps.Api.Overlays;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class AddOverlays : UserControl
    {
        /// <summary>
        /// 地图初始化
        /// </summary>
        AMap map = new AMap();
        public AddOverlays()
        {
            this.InitializeComponent();
            this.InitializeComponent();
            contentGrid.Children.Add(map);
            Canvas.SetZIndex(showMapBorder, 10);
        }

        /// <summary>
        /// 添加椭圆
        /// </summary>
        public void AddPoint(object sender, RoutedEventArgs e)
        {
            map.Children.Clear();
            AEllipse poi = new AEllipse();
            poi.LngLat = map.Center;
            poi.RadiusX = 10;//半径
            poi.RadiusY = 10;//半径
            poi.FillOpacity = 0.5;//填充透明度
            poi.StrokeColor = Colors.Red;//边框颜色
            poi.FillColor = Colors.Yellow;//填充颜色
            map.Children.Add(poi);
        }
        /// <summary>
        /// 添加标记物
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddMarker(object sender, RoutedEventArgs e)
        {
            map.Children.Clear();
            AMarker poi = new AMarker();
            poi.LngLat = map.Center;
            poi.IconURI = new Uri("http://api.amap.com/Home/Tpl/default/Public/Images/wp7.png");//也可以读本地图片
            map.Children.Add(poi);
        }
        /// <summary>
        /// 添加线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddPolyline(object sender, RoutedEventArgs e)
        {
            map.Children.Clear();
            APolyline pol = new APolyline();
            ObservableCollection<ALngLat> lnglats = new ObservableCollection<ALngLat>();
            lnglats.Add(map.Center);
            lnglats.Add(new ALngLat(map.Center.LngX + 0.02, map.Center.LatY + 0.03));

            lnglats.Add(new ALngLat(map.Center.LngX + 0.05, map.Center.LatY + 0.03));

            pol.LngLats = lnglats;
            map.Children.Add(pol);
        }
        /// <summary>
        /// 添加多边形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddPolygon(object sender, RoutedEventArgs e)
        {
            map.Children.Clear();
            APolygon pol = new APolygon();
            ObservableCollection<ALngLat> lnglats = new ObservableCollection<ALngLat>();
            lnglats.Add(map.Center);
            lnglats.Add(new ALngLat(map.Center.LngX + 0.02, map.Center.LatY + 0.03));
            lnglats.Add(new ALngLat(map.Center.LngX + 0.02, map.Center.LatY + 0.04));
            lnglats.Add(new ALngLat(map.Center.LngX + 0.05, map.Center.LatY + 0.03));
            pol.LngLats = lnglats;
            map.Children.Add(pol);
        }
    }
}
