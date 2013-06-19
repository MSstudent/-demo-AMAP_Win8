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
using System.Text;
using System.Diagnostics;
using Windows.Devices.Geolocation;
using System.Windows.Media;

namespace PhoneApp2.Samples
{
    public partial class MapPage : PhoneApplicationPage
    {
        AMap amap;
        StringBuilder sb = new StringBuilder();
        AMapGeolocator amapgelocator;
      

        public MapPage()
        {
            InitializeComponent();
            this.ContentPanel.Children.Add(amap = new AMap());
            // this.Loaded += MapPage_Loaded;
            amap.Loaded += amap_Loaded;
           
        }

        void amap_Loaded(object sender, RoutedEventArgs e)
        {
           // amap.MoveCamera(CameraUpdateFactory.NewLatLng(amap.Center));
            CameraUpdate cu = CameraUpdateFactory.ZoomTo(13);
           
            Debug.WriteLine(amap.Center);
            
        }

        void MapPage_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private void btnMethods_Click(object sender, RoutedEventArgs e)
        {
           
        }

    }
}