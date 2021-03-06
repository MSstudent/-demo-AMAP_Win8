﻿using System;
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
using System.Windows.Media;

namespace PhoneApp2.Samples
{
    public partial class MapPolylinePage : PhoneApplicationPage
    {
        AMap amap;
        AMapPolyline polyline;

        public MapPolylinePage()
        {
            InitializeComponent();
            this.ContentPanel.Children.Add(amap = new AMap());
            this.btnVisible.IsEnabled = false;
            this.Loaded += MapPolylinePage_Loaded;
        }

        void MapPolylinePage_Loaded(object sender, RoutedEventArgs e)
        {
            amap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(amap.Center,12));
        }

        private void Button_DrawPolyline_Click(object sender, RoutedEventArgs e)
        {
            List<LatLng> lnglats = new List<LatLng>();
            lnglats.Add(new LatLng(amap.Center.latitude - 0.02, amap.Center.longitude - 0.02));
            lnglats.Add(new LatLng(amap.Center.latitude + 0.03, amap.Center.longitude - 0.02));
            polyline = amap.AddPolyline(new AMapPolylineOptions()
            {
                Points=lnglats,
                Color=Color.FromArgb(255,0,0,255),
                Width=4,
            });
            this.btnVisible.IsEnabled = true;
        }

        private void Button_Destroy_Click(object sender, RoutedEventArgs e)
        {
            if (polyline!=null)
            {
                polyline.Destroy();
            }
            this.btnVisible.IsEnabled = false;
        }

        private void Button_Visible_Click(object sender, RoutedEventArgs e)
        {
            if (polyline!=null)
            {
                polyline.Visible = false;
            }
            this.btnVisible.IsEnabled = false;
        }
    }
}