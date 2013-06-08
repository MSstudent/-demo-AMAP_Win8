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
using Com.AMap.Maps.Api.Layers;
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
    public sealed partial class BusIDSearch : UserControl
    {
        AMap map = new AMap();
        MapLayer markLayer;
        public BusIDSearch()
        {
            markLayer = new MapLayer(map);
            map.Children.Add(markLayer);

            this.InitializeComponent();
            searchContent.Children.Add(map);
            Canvas.SetZIndex(PoiListView, 1000);
            map.Loaded += map_Loaded;

        }
        void map_Loaded(object sender, RoutedEventArgs e)
        {
        }
        public ObservableCollection<AMarker> markerList = new ObservableCollection<AMarker>();
        private async void BusLineSearchTest(string ids, string cityCode)
        {

            BusLineSearchOption rgo = new BusLineSearchOption();
            rgo.Ids = ids;
            rgo.CityCode = cityCode;
            BusLineSearchResult rgcs = await BusLineSearch.BusLineSearchWithOption(rgo);

            this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
           {
               IEnumerable<BusLine> bl = rgcs.BusLineList;
              
               if (rgcs.Erro == null && bl!=null)
               {
                   SearchTextGrid.Visibility = Visibility.Collapsed;
                  
                   ObservableCollection<ALngLat> lnglatRoute = new ObservableCollection<ALngLat>();   //线路坐标
                   IEnumerable<String> lnglatstring;
                   foreach (BusLine bs in bl)
                   {
                       IEnumerable<Station> ss = bs.StationList;

                       foreach (Station s in ss)
                       {
                           s.Name = s.StationNum + " " + s.Name;

                       }
                       PoiListView.DataContext = ss;
                       lnglatstring = bs.XYs.Split(new Char[] { ';' });

                       foreach (String ssss in lnglatstring)
                       {
                           String[] lnglatds = ssss.Split(new Char[] { ',' });
                           lnglatRoute.Add(new ALngLat(Double.Parse(lnglatds[0]), Double.Parse(lnglatds[1])));
                       }
                       int i = 0;
                       foreach (Station st in ss)
                       {
                           i++;
                           String[] lnglatds = st.XY.Split(new Char[] { ',' });
                           ALngLat markerlnglat = new ALngLat(Double.Parse(lnglatds[0]), Double.Parse(lnglatds[1]));
                           AMarker poiMarker = new AMarker();
                           poiMarker.Anchor = new Point(0.5, 0.5);
                           poiMarker.LngLat = markerlnglat;
                           ATip tip = new ATip();
                           tip.Title = st.Name;
                           tip.ContentText = "站点序号： " + st.StationNum;
                           poiMarker.TipFrameworkElement = tip;
                           markerList.Add(poiMarker);
                       }
                       PoiListView.Visibility = Visible;
                       APolyline pol = new APolyline();
                       pol.LineThickness = 5;
                       pol.LngLats = lnglatRoute;
                       markLayer.Children.Add(pol);
                       markLayer.SetOverlaysFitView();
                       try
                       {
                           PoiListView.SelectedIndex = 0;
                       }
                       catch
                       {
                       }
                   }
               }
               else
               {
                   MessageDialog msd = new MessageDialog(rgcs.Erro.Message);
                   this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                   {
                       msd.ShowAsync();
                   });
                   Debug.WriteLine(rgcs.Erro.Message);
               }
             
           });
        }





        AMarker pointma;
        private void PoiListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pointma != null)
            {
                markLayer.Children.Remove(pointma);
            }
            markerList[PoiListView.SelectedIndex].IconURI = new Uri("http://api.amap.com/webapi/static/Images/point.png", UriKind.RelativeOrAbsolute);
            pointma = markerList[PoiListView.SelectedIndex];
            markLayer.Children.Add(pointma);
            markerList[PoiListView.SelectedIndex].OpenTip();
        }

        public Windows.UI.Xaml.Visibility Visible { get; set; }

        private void PoiSearchClick(object sender, RoutedEventArgs e)
        {
            string ids = PoiInput.Text;
            string cityCode = cityInput.Text;

            ThreadPool.RunAsync(
                         (timer) =>
                         {
                             BusLineSearchTest(ids, cityCode);
                         },
                         WorkItemPriority.Low, WorkItemOptions.None);
        }
    }
}
