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
    public partial class MapMarkerPage : PhoneApplicationPage
    {
        AMap amap;
        AMapMarker marker;
        public MapMarkerPage()
        {
            InitializeComponent();
            this.ContentPanel.Children.Add(amap = new AMap());
            this.btnVisible.IsEnabled = false;
        }
        private void Button_DrawCircle_Click(object sender, RoutedEventArgs e)
        {
            marker = amap.AddMarker(new AMapMarkerOptions()
            {
                Position=amap.Center,
                Title="Title",
                Snippet="Snippet",
                IconUri=new Uri ("Images/AZURE.png",UriKind.Relative),
            });
            this.btnVisible.IsEnabled = true;
        }

        private void Button_Destroy_Click(object sender, RoutedEventArgs e)
        {
            if (marker!=null)
            {
                marker.Destroy();
            }
            this.btnVisible.IsEnabled = false;
        }

        private void Button_Visible_Click(object sender, RoutedEventArgs e)
        {
            if (marker!=null)
            {
                marker.Visible = false;
            }
            this.btnVisible.IsEnabled = false;
        }

      
    }
}