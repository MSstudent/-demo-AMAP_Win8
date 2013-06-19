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
    public partial class MapCirclePage : PhoneApplicationPage
    {
        AMap amap;
        AMapCircle circle;
        public MapCirclePage()
        {
            InitializeComponent();
            this.ContentPanel.Children.Add(amap = new AMap());

            this.btnVisible.IsEnabled = false;
        }


        private void Button_DrawCircle_Click(object sender, RoutedEventArgs e)
        {
            circle = amap.AddCircle(new AMapCircleOptions()
            {
                 Center=amap.Center,
                 Radius=2000,
            });
            this.btnVisible.IsEnabled = true;
        }

       
        private void Button_Destroy_Click(object sender, RoutedEventArgs e)
        {
            if (circle != null)
            {
                circle.Destroy();
            }
            this.btnVisible.IsEnabled = false;
        }

        private void Button_Visible_Click(object sender, RoutedEventArgs e)
        {
            if (circle!= null)
            {
                circle.Visible = false;
            }
            this.btnVisible.IsEnabled = false;
        }
    }
}