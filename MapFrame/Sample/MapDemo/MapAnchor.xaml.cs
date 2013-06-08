using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.BaseTypes;
using Com.AMap.Maps.Api.Overlays;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    /// 地图锚点概念
    /// </summary>
    public sealed partial class MapAnchor : UserControl
    {
        AMap map = new AMap();
        AMarker lbmk;
        AMarker cbmk;
        AMarker rbmk;
        ALngLat center = new ALngLat(116.397428, 39.90923);
        public MapAnchor()
        {
            this.InitializeComponent();
            contentGrid.Children.Add(map);
            Canvas.SetZIndex(showMapBorder,10);
            map.MapLoaded += map_MapLoaded;
        }
        /// <summary>
        /// 添加基准点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void map_MapLoaded(object sender, RoutedEventArgs e)
        {
            map.Children.Add(new AEllipse()
            {
                Anchor = new Point(0.5,0.5),
                RadiusX = 8,
                RadiusY = 8,
                FillColor = Colors.Brown,
                LngLat = center,
            });
        }

        /// <summary>
        /// 添加左下角点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            map.Children.Clear();
            lbmk = null;
            map.Children.Add(new AEllipse()
            {
                Anchor = new Point(0.5, 0.5),
                RadiusX = 8,
                RadiusY = 8,
                FillColor = Colors.Brown,
                LngLat = center,
            });

            if (lbmk == null)
            {
                map.Children.Add(lbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/01.png", UriKind.RelativeOrAbsolute),
                    Anchor = new Point(0, 1),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "左下角点", ContentText = "这是左下角点" }
                });
            }
            else 
            {

                map.Children.Remove(lbmk);
                map.Children.Add(lbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/01.png", UriKind.RelativeOrAbsolute),
                    Anchor = new Point(0, 1),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "左下角点", ContentText = "这是左下角点" }
                });
            }
        }
      
      
        /// <summary>
        /// 添加右下角
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            map.Children.Clear();
            map.Children.Add(new AEllipse()
            {
                Anchor = new Point(0.5, 0.5),
                RadiusX = 8,
                RadiusY = 8,
                FillColor = Colors.Brown,
                LngLat = center,
            });
            if (rbmk == null)
            {
                map.Children.Add(rbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/11.png", UriKind.RelativeOrAbsolute),
                    Anchor = new Point(1, 1),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "右下角点", ContentText = "这是右下角点" }
                });
            }
            else 
            {
                map.Children.Remove(rbmk);
                map.Children.Add(rbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/11.png", UriKind.RelativeOrAbsolute),
                    Anchor = new Point(1, 1),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "右下角点", ContentText = "这是右下角点" }
                });
            
            }
        }
 
        /// <summary>
        /// 左下角点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
          map.Children.Clear();
            map.Children.Add(new AEllipse()
            {
                Anchor = new Point(0.5, 0.5),
                RadiusX = 8,
                RadiusY = 8,
                FillColor = Colors.Brown,
                LngLat = center,
            });
            if (lbmk == null)
            {
                map.Children.Add(lbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/01.png", UriKind.RelativeOrAbsolute),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "默认点", ContentText = "这是默认点" }
                });
            }
            else 
            {
                map.Children.Remove(lbmk);
                map.Children.Add(lbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/01.png", UriKind.RelativeOrAbsolute),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "默认点", ContentText = "这是默认点" }
                });
            
            }
        }
        /// <summary>
        /// 中下角点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
           map.Children.Clear();
            map.Children.Add(new AEllipse()
            {
                Anchor = new Point(0.5, 0.5),
                RadiusX = 8,
                RadiusY = 8,
                FillColor = Colors.Brown,
                LngLat = center,
            });
          
            
                map.Children.Add(cbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/051.png", UriKind.RelativeOrAbsolute),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "中下角点", ContentText = "这是中下角点" }
                });
            
        }
        /// <summary>
        /// 右下角点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            map.Children.Clear();
            map.Children.Add(new AEllipse()
            {
                Anchor = new Point(0.5, 0.5),
                RadiusX = 8,
                RadiusY = 8,
                FillColor = Colors.Brown,
                LngLat = center,
            });
            if (rbmk == null)
            {
                map.Children.Add(rbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/11.png", UriKind.RelativeOrAbsolute),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "默认点", ContentText = "这是默认点" }
                });
            }
            else 
            {
                map.Children.Remove(rbmk);
                map.Children.Add(rbmk = new AMarker()
                {
                    IconURI = new Uri("/Samples/11.png", UriKind.RelativeOrAbsolute),
                    LngLat = center,
                    TipFrameworkElement = new ATip() { Title = "默认点", ContentText = "这是默认点" }
                });
            
            }
        }

    }
}
