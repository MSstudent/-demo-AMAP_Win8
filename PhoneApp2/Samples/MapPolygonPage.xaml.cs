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
using System.Windows.Media;

namespace PhoneApp2.Samples
{
    public partial class MapPolygonPage : PhoneApplicationPage
    {
        AMap amap;
        AMapPolygon polygon;
        public MapPolygonPage()
        {
            InitializeComponent();
            this.ContentPanel.Children.Add(amap = new AMap());
            this.btnVisible.IsEnabled = false;
        }

        private void Button_DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            //绘多边形
            List<LatLng> lnglats1 = new List<LatLng>();
            lnglats1.Add(new LatLng(amap.Center.latitude + 0.02, amap.Center.longitude + 0.03));
            lnglats1.Add(new LatLng(amap.Center.latitude + 0.03, amap.Center.longitude - 0.03));
            lnglats1.Add(new LatLng(amap.Center.latitude - 0.026, amap.Center.longitude - 0.03));
            lnglats1.Add(amap.Center);
            lnglats1.Add(new LatLng(amap.Center.latitude - 0.04, amap.Center.longitude + 0.035));
            polygon= amap.AddPolygon(new AMapPolygonOptions()
            {
                FillColor = Color.FromArgb(30, 255, 0, 255),
                Points = lnglats1,
                StrokeColor = Color.FromArgb(255, 102, 136, 255),
                StrokeWidth = 2
            });
            this.btnVisible.IsEnabled = true;
        }

        private void Button_Destroy_Click(object sender, RoutedEventArgs e)
        {
            if (polygon != null)
            {
                polygon.Destroy();
            }
            this.btnVisible.IsEnabled = false;
        }

        private void Button_Visible_Click(object sender, RoutedEventArgs e)
        {
            if (polygon != null)
            {
                polygon.Visible = false;
            }
            this.btnVisible.IsEnabled = false;
        }

       

      
    }
}