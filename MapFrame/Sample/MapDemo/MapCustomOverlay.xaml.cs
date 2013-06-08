using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.BaseTypes;
using Com.AMap.Maps.Api.Layers;
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
    /// 添加自动容器为覆盖物
    /// </summary>
    public sealed partial class MapCustomOverlay : UserControl
    {
        AMap map = new AMap();
        public MapCustomOverlay()
        {
            this.InitializeComponent();
            contentGrid.Children.Add(map);
            Canvas.SetZIndex(showMapBorder,10);
        }

        /// <summary>
        /// 添加自动容器为覆盖物
        /// </summary>
        public void AddCustomOverlay(object sender, RoutedEventArgs e)
        {
            //自定义容器
            CustomOverlayControl coo = new CustomOverlayControl();
            coo.SetValue(MapLayer.LngLatProperty,map.Center);//设置依赖属性 经纬度
            coo.SetValue(MapLayer.AnchorProperty, new Point(0.5, 1));//设置锚点为中下部 // 锚点值 取值0->1 左上为Point(0,0)  右下为中心点为Point(1,1)
            map.Children.Add(coo);
        }
      

    }
}
