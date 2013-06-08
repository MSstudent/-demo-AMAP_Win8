using Com.AMap.Maps.Api;
using Com.AMap.Maps.Api.BaseTypes;
using Com.AMap.Maps.Api.Overlays;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
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
    /// 适用于高德地图的定位接口
    /// </summary>
    public sealed partial class MapAGeolocator : UserControl
    {
        AMap map = new AMap();
        AMarker marker = new AMarker();
        public MapAGeolocator()
        {
            this.InitializeComponent();
            contentGrid.Children.Add(map);
            map.MapLoaded += map_MapLoaded;
        }

        void map_MapLoaded(object sender, RoutedEventArgs e)
        {
            ///适用于高德地图的定位接口
            AGeolocator ageol = new AGeolocator();
            ageol.PositionChanged += ageol_PositionChanged;
        }

        void ageol_PositionChanged(AGeolocator sender, APositionChangedEventArgs args)
        {
            this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                if (!map.Children.Contains(marker))
                {
                    marker.LngLat = args.LngLat;
                    map.Children.Add(marker);
                }
                else
                {
                    marker.LngLat = args.LngLat;
                }
                map.SetZoomAndCenter(15, args.LngLat);
                Debug.WriteLine("定位精度：" + args.Accuracy);//单位米
            });
        }

    }
}
