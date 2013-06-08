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
    public sealed partial class RouteSearchofBusLine : UserControl
    {
        AMap map = new AMap();
        MapLayer markLayer;
        public RouteSearchofBusLine()
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



        private async void BusRouteSearchTest(string cityCode, string searchName, string searchEndName)
        {

            POISearchOption ps = new POISearchOption();
            ps.SearchName = searchName;
            ps.CityCode = cityCode;

            POISearchOption pe = new POISearchOption();
            pe.SearchName = searchEndName;
            pe.CityCode = cityCode;

            POISearchResult iopStart = await POISearch.PoiSearchWithOption(ps);//查询起点坐标
            POISearchResult iopEnd = await POISearch.PoiSearchWithOption(pe);//查询终点坐标
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
            BusRouteSearchOption rgo = new BusRouteSearchOption();
            rgo.X1 = poist.X;
            rgo.Y1 = poist.Y;
            rgo.X2 = poied.X;
            rgo.Y2 = poied.Y;
            rgo.CityCode = cityCode;
            BusRouteSearchResult rgcs = await BusRouteSearch.BusRouteSearchWithOption(rgo);//根据起点重点坐标查公交

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
          markLayer.Children.Clear();
          ALngLat lnglat = new ALngLat(poist.X, poist.Y);
          AMarker poiMarker = new AMarker();
          poiMarker.IconURI = new Uri("/Samples/qd.png", UriKind.Relative);
          poiMarker.LngLat = lnglat;
          ATip tip = new ATip();
          tip.Title = poist.Name;
          tip.ContentText = poist.Address;
          poiMarker.TipFrameworkElement = tip;
          markLayer.Children.Add(poiMarker);

          ALngLat lnglated = new ALngLat(poied.X, poied.Y);
          AMarker poiMarkered = new AMarker();
          poiMarkered.IconURI = new Uri("/Samples/zd.png", UriKind.Relative);
          poiMarkered.LngLat = lnglated;
          ATip tiped = new ATip();
          tiped.Title = poied.Name;
          tiped.ContentText = poied.Address;
          poiMarkered.TipFrameworkElement = tiped;
          markLayer.Children.Add(poiMarkered);


          ObservableCollection<ALngLat> lnglatRoute = new ObservableCollection<ALngLat>(); //线路坐标
          APolyline pol = new APolyline();
          pol.LineThickness = 5;

          IEnumerable<Bus> busInfo = rgcs.Routes;
          IEnumerable<String> lnglatstring;
          IEnumerable<String> passdeportstring;
          //画公交线路和公交站点
          foreach (Bus bus in busInfo)
          {
              IEnumerable<BusSegment> bussegmentInfo = bus.SegmentArray;
              foreach (BusSegment bs in bussegmentInfo)
              {

                  lnglatstring = bs.CoordinateList.Split(new Char[] { ';' });
                  passdeportstring = bs.PassDeportCoordinate.Split(new Char[] { ';' });
                  String[] passport = bs.PassDeportName.Split(' ');
                  int i = 0;
                  foreach (String ss in lnglatstring)
                  {
                      String[] lnglatds = ss.Split(new Char[] { ',' });
                      lnglatRoute.Add(new ALngLat(Double.Parse(lnglatds[0]), Double.Parse(lnglatds[1])));
                  }

                  foreach (String ss in passdeportstring)
                  {

                      String[] lnglatds = ss.Split(new Char[] { ',' });
                      ALngLat lnglatpassdeport = new ALngLat(Double.Parse(lnglatds[0]), Double.Parse(lnglatds[1]));
                      ATip tipStart = new ATip();
                      // tipStart.Anchor = new Point(0,1);
                      tipStart.Title = passport[i];
                      AMarker marker = new AMarker();
                      marker.Anchor = new Point(0.46, 1);
                      marker.IconURI = new Uri("http://api.amap.com/webapi/static/Images/bx11.png");
                      marker.LngLat = lnglatpassdeport;
                      marker.TipFrameworkElement = tipStart;
                      markLayer.Children.Add(marker);
                      //  Canvas.SetTop(marker,100);
                      marker.TipAnchor = new Point(0.35, 1);
                  }
              }
              break;
          }


          pol.LngLats = lnglatRoute;
          markLayer.Children.Insert(0, pol);

          map.SetOverlaysFitView();
      });
            }

        }


        public Windows.UI.Xaml.Visibility Visible { get; set; }

        private void BusRouteSearchClick(object sender, RoutedEventArgs e)
        {
            string searchName = startInput.Text;
            string cityCode = cityInput.Text;
            string searchEndName = endInput.Text;
            ThreadPool.RunAsync(
       (timer) =>
       {
           BusRouteSearchTest(cityCode, searchName, searchEndName);
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
