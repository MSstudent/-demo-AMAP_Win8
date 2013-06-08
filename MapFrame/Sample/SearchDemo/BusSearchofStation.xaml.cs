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
    public sealed partial class BusSearchofStation : UserControl
    {
        AMap map = new AMap();
        MapLayer markLayer;
        public BusSearchofStation()
        {
            markLayer = new MapLayer(map);
            map.Children.Add(markLayer);

            this.InitializeComponent();
            searchContent.Children.Add(map);

            // map.Children.Add(new TextBox() { Text="dfadsaafasfdasfasfasdfsadfasdfasfasdafasasfasf"});
            Canvas.SetZIndex(PoiListView, 1000);
            map.Loaded += map_Loaded;

        }
        void map_Loaded(object sender, RoutedEventArgs e)
        {
        }
        public ObservableCollection<AMarker> markerList = new ObservableCollection<AMarker>();
        private async void BusLineSearchTest(string stationName, string cityCode)
        {
            BusLineSearchOption rgo = new BusLineSearchOption();
            rgo.StationName = stationName;
            rgo.CityCode = cityCode;
            //     服务编码 默认8085-根据线路ID查询Ids不能为空 8004-根据线路名称查询 8086-根据站点名称查询
            rgo.Sid = "8086";

           BusLineSearchResult rgcs = await BusLineSearch.BusLineSearchWithOption(rgo);
           this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
         {
             
             //    ObservableCollection<ALngLat> lnglatRoute = new ObservableCollection<ALngLat>();   //线路坐标
             //    IEnumerable<String> lnglatstring;

             if (rgcs.Erro == null)
             {
                 SearchTextGrid.Visibility = Visibility.Collapsed;
                 IEnumerable<BusLine> bs = rgcs.BusLineList;
                 PoiListView.DataContext = bs;
                 PoiListView.Visibility = Visible;
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





        private void PoiListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public Windows.UI.Xaml.Visibility Visible { get; set; }

        private void PoiSearchClick(object sender, RoutedEventArgs e)
        {
            string stationName = PoiInput.Text;
            string cityCode = cityInput.Text;
            ThreadPool.RunAsync(
                      (timer) =>
                      {
                          BusLineSearchTest(stationName, cityCode);
                      },
                      WorkItemPriority.Low, WorkItemOptions.None);
        }
    }
}
