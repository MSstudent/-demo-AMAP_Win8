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
    public sealed partial class RegeocodeDemo : UserControl
    {
        AMap map = new AMap();
        public RegeocodeDemo()
        {

            this.InitializeComponent();
            searchContent.Children.Add(map);
            map.Loaded += map_Loaded;

        }

        void map_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public ObservableCollection<AMarker> markerList = new ObservableCollection<AMarker>();
        private async void ReGeoCodeTest(double lng, double lat)
        {
            ReverseGeocodingOption rgo = new ReverseGeocodingOption();
            rgo.XCoors = new double[] { lng };
            rgo.YCoors = new double[] { lat };
            ReverseGeoCodingResult rgcs = await ReGeoCode.GeoCodeToAddressWithOption(rgo);
            this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
      {
          if (rgcs.Erro == null && rgcs.resultList != null)
          {
              IEnumerable<ReverseGeocodingInfo> reverseGeocodeResult = rgcs.resultList;
              foreach (ReverseGeocodingInfo poi in reverseGeocodeResult)
              {
                  int i = 0;
                  foreach (POI poilist in poi.Pois)
                  {
                      i++;
                      AMarker marker = new AMarker();
                      marker.LngLat = new ALngLat(poilist.X, poilist.Y);
                      ATip tip = new ATip();
                      tip.Title = i + ": " + poilist.Name;
                      tip.ContentText = poilist.Address;
                      marker.TipFrameworkElement = tip;
                      markerList.Add(marker);
                      map.Children.Add(marker);

                  }

              }
              markerList[0].OpenTip();
              map.SetOverlaysFitView();

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


        private async void RGCSearchTest()
        {
            RGCSearchOption rgo = new RGCSearchOption();
            rgo.Glong = 116.3544845;
            rgo.Glat = 39.98882653;

            RGCSearchResult rgcs = await RGCSearch.GPSToOffsetWithOption(rgo);
            if (rgcs.Erro == null)
            {
                Debug.WriteLine(rgcs);
            }
            else
            {
                Debug.WriteLine(rgcs.Erro.Message);
            }
        }

        public ObservableCollection<AMarker> markerList1 = new ObservableCollection<AMarker>();
        String GetValue = "POI";
        async void CenterlnglatAroundPOISearch()
        {
            ReverseGeocodingOption rgo = new ReverseGeocodingOption();

            rgo.XCoors = new double[] { double.Parse(centerLng.Text) };
            rgo.YCoors = new double[] { double.Parse(centerLat.Text) };
            ReverseGeoCodingResult rgcs = await ReGeoCode.GeoCodeToAddressWithOption(rgo);
            if (rgcs.Erro == null)
            {
                Debug.WriteLine(rgcs);
            }
            else
            {
                Debug.WriteLine(rgcs.Erro.Message);
            }
        }




        public Windows.UI.Xaml.Visibility Visible { get; set; }

        private void PoiSearchClick(object sender, RoutedEventArgs e)
        {
            double lng = double.Parse(centerLng.Text);
            double lat = double.Parse(centerLat.Text);
            ThreadPool.RunAsync(
                     (timer) =>
                     {
                         ReGeoCodeTest(lng, lat);
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
