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
using Windows.UI.Popups;
using Com.AMap.Maps.Api.Layers;
using Com.AMap.Search.API.Result;
using Windows.System.Threading;
using Windows.UI.Core;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Win8.Map.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RouteSearchofDriving : UserControl
    {
        AMap map = new AMap();
        MapLayer markLayer;
        public RouteSearchofDriving()
        {
            markLayer = new MapLayer(map);
            map.Children.Add(markLayer);
            this.InitializeComponent();
            searchContent.Children.Add(map);
            map.Loaded += map_Loaded;

        }

        void map_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private async void DriveRouteSearchTest(string cityCode, string searchName, string searchEndName)
        {
            POISearchOption pst = new POISearchOption();
            pst.SearchName = searchName;
            pst.CityCode = cityCode;
            POISearchOption ped = new POISearchOption();
            ped.SearchName = searchEndName;
            ped.CityCode = cityCode;

            POISearchResult iopStart = await POISearch.PoiSearchWithOption(pst);//查起点坐标
            POISearchResult iopEnd = await POISearch.PoiSearchWithOption(ped);//查终点坐标

            if (iopStart.Erro != null || iopStart.POIs == null)
            {
                MessageDialog msd = new MessageDialog(iopStart.Erro.Message);
                this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                {
                    msd.ShowAsync();
                });
                return;
            }
            if (iopEnd.Erro != null || iopEnd.POIs == null)
            {
                MessageDialog msd = new MessageDialog(iopStart.Erro.Message);
                this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                {
                    msd.ShowAsync();
                });
                return;
            }
            POI poist = Enumerable.First<POI>(iopStart.POIs);
            POI poied = Enumerable.First<POI>(iopEnd.POIs);


            RouteSearchOption rgo = new RouteSearchOption();
            rgo.X1 = poist.X;
            rgo.Y1 = poist.Y;
            rgo.X2 = poied.X;
            rgo.Y2 = poied.Y;
            RouteSearchResult rgcs = await RouteSearch.RouteSearchWithOption(rgo);

            if (rgcs.Erro != null)
            {
                MessageDialog msd = new MessageDialog("查询公交失败！" + rgcs.Erro.Message, "提示");
                this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                {
                    msd.ShowAsync();
                });
                return;
            }
            else
            {
                this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
      {
          ALngLat lnglatst = new ALngLat(poist.X, poist.Y);
          AMarker poiMarkerst = new AMarker();
          poiMarkerst.IconURI = new Uri("/Samples/qd.png", UriKind.Relative);
          poiMarkerst.LngLat = lnglatst;
          ATip tipst = new ATip();
          tipst.Title = poist.Name;
          tipst.ContentText = poist.Address;
          poiMarkerst.TipFrameworkElement = tipst;
          map.Children.Add(poiMarkerst);


          ALngLat lnglated = new ALngLat(poied.X, poied.Y);
          AMarker poiMarkered = new AMarker();
          poiMarkered.IconURI = new Uri("/Samples/zd.png", UriKind.Relative);
          poiMarkered.LngLat = lnglated;
          ATip tiped = new ATip();
          tiped.Title = poied.Name;
          tiped.ContentText = poied.Address;
          poiMarkered.TipFrameworkElement = tiped;
          map.Children.Add(poiMarkered);

          APolyline pol = new APolyline();
          pol.LineThickness = 5;
          ObservableCollection<ALngLat> lnglatRoute = new ObservableCollection<ALngLat>(); //线路坐标

          IEnumerable<String> lnglatstring;
          IEnumerable<RouteSegment> rsegment = rgcs.Segment;
          foreach (RouteSegment rs in rsegment)
          {
              lnglatstring = rs.Coor.Split(new Char[] { ';' });
              foreach (String ss in lnglatstring)
              {
                  String[] lnglatds = ss.Split(new Char[] { ',' });
                  lnglatRoute.Add(new ALngLat(Double.Parse(lnglatds[0]), Double.Parse(lnglatds[1])));
              }
          }
          pol.LngLats = lnglatRoute;
          markLayer.Children.Add(pol);
          map.SetOverlaysFitView();
      });

            }
        }

        public Windows.UI.Xaml.Visibility Visible { get; set; }

        private void RouteSearchClick(object sender, RoutedEventArgs e)
        {

            string searchName = startInput.Text;
            string cityCode = cityInput.Text;
            string searchEndName = endInput.Text;
            ThreadPool.RunAsync(
       (timer) =>
       {
           DriveRouteSearchTest(cityCode, searchName, searchEndName);
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
