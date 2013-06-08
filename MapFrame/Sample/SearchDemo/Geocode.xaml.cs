using Com.AMap.Maps.Api;
using Com.AMap.Search.API;
using Com.AMap.Search.API.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Markup;
using Com.AMap.Maps.Api.Overlays;
using Com.AMap.Maps.Api.BaseTypes;
using System.Collections.ObjectModel;
using Com.AMap.Search.API.Result;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Win8.Map.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Geocode : UserControl
    {
        AMap map = new AMap();
        public Geocode()
        {

            this.InitializeComponent();
            searchContent.Children.Add(map);
            map.Loaded += map_Loaded;

        }

        void map_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private async void GeoCodeTest()
        {
            GeoCodingOption rgo = new GeoCodingOption();
            rgo.Address = "北京市朝阳区望京阜通东大街方恒国际中心";
            GeoCodingResult rgcs = await GeoCode.AddressToGeoCodeWithOption(rgo);
            this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
        {
            if (rgcs.Erro == null && rgcs.GeoCodingList!=null)
            {
                IEnumerable<GeoPOI> pois = rgcs.GeoCodingList;
                int i = 0;
                foreach (GeoPOI poi in pois)
                {
                    i++;
                    ATip tip = new ATip();
                    tip.Title = poi.Name;
                    tip.ContentText = poi.Address;
                    AMarker marker = new AMarker();
                    marker.LngLat = new ALngLat(poi.X, poi.Y);
                    marker.TipFrameworkElement = tip;
                    map.Children.Add(marker);
                    marker.OpenTip();
                    map.SetZoomAndCenter(18, marker.LngLat);

                }
                Debug.WriteLine(rgcs);

            }
            else
            {
                MessageDialog msd = new MessageDialog(rgcs.Erro.Message);
                this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                {
                    msd.ShowAsync();
                });
            }
        });
        }





        public Windows.UI.Xaml.Visibility Visible { get; set; }

        private void PoiSearchClick(object sender, RoutedEventArgs e)
        {
            ThreadPool.RunAsync(
                      (timer) =>
                      {
                          GeoCodeTest();
                      },
                      WorkItemPriority.Low, WorkItemOptions.None);

        }

        private void cityInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void dataInput_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }


        private void dataNumber_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        {

        }



        public double Convertor { get; set; }
    }
}
