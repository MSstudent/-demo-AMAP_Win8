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
using Windows.UI.Core;
using Windows.System.Threading;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Win8.Map.Demo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PoiSearch : UserControl
    {
        AMap map = new AMap();
        public PoiSearch()
        {

            this.InitializeComponent();
            searchContent.Children.Add(map);
            Canvas.SetZIndex(PoiListView, 1000);
            map.Loaded += map_Loaded;

        }

        void map_Loaded(object sender, RoutedEventArgs e)
        {

        }




        private async void BusSearchTest()
        {
            BusRouteSearchOption rgo = new BusRouteSearchOption();
            rgo.X1 = 116.4604213;
            rgo.Y1 = 39.9204703;
            rgo.X2 = 116.2883602;
            rgo.Y2 = 39.8236433;
            rgo.CityCode = "010";

            BusRouteSearchResult rgcs = await BusRouteSearch.BusRouteSearchWithOption(rgo);

            if (rgcs.Erro == null)
            {
                Debug.WriteLine(rgcs);
            }
            else
            {
                Debug.WriteLine(rgcs.Erro.Message);
            }
        }

        public ObservableCollection<AMarker> markerList = new ObservableCollection<AMarker>();
        String GetValue = "POI";
        async void POISearchTest(string searchName, string cityCode, string srcType, int num)
        {
            DateTime st = DateTime.Now;
            //Debug.WriteLine(DateTime.Now.Ticks);
            POISearchOption p = new POISearchOption();
            p.SearchName = searchName;
            p.CityCode = cityCode;
            p.SrcType = srcType;
            p.Number = num;
            //  p.Number = dataNumber.Value;
            POISearchResult iop = await POISearch.PoiSearchWithOption(p);
            this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                if (iop.Erro == null)
                {
                    // POISearchResult iop = await POISearch.PoiSearchByRegionAndKeyword("中关村", "116.204,39.9938;116.456,39.8833", "rectangle", 10);
                    TimeSpan elapsedSpan = new TimeSpan(DateTime.Now.Ticks - st.Ticks);
                    int i = 0;
                    SearchTextGrid.Visibility = Visibility.Collapsed;
                    foreach (POI poi in iop.POIs)
                    {
                        i++;
                        poi.Name = "  " + i + "、" + poi.Name;
                        ALngLat lnglat = new ALngLat(poi.X, poi.Y);
                        AMarker poiMarker = new AMarker();
                        poiMarker.LngLat = lnglat;
                        ATip tip = new ATip();
                        tip.Title = poi.Name;
                        tip.ContentText = poi.Address;
                        poiMarker.TipFrameworkElement = tip;
                        markerList.Add(poiMarker);
                        map.Children.Add(poiMarker);
                    }
                    PoiListView.ItemsSource = iop.POIs;
                    
                    map.SetOverlaysFitView();
                    PoiListView.SelectedIndex = 0;
                    markerList[0].OpenTip();
                    PoiListView.Visibility = Visible;
                }
                else
                {
                    MessageDialog msd = new MessageDialog(iop.Erro.Message);
                    this.Dispatcher.RunAsync(CoreDispatcherPriority.Low, () =>
                    {
                        msd.ShowAsync();
                    });
                    Debug.WriteLine(iop.Erro.Message);
                }
            });
        }

        private void PoiListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            markerList[PoiListView.SelectedIndex].OpenTip();
            map.SetZoomAndCenter(13, markerList[PoiListView.SelectedIndex].LngLat);
        }


        public Windows.UI.Xaml.Visibility Visible { get; set; }

        private void PoiSearchClick(object sender, RoutedEventArgs e)
        {
            string searchName = PoiInput.Text;
            string cityCode = cityInput.Text;
            string srcType = GetValue;
            int number = int.Parse(slidervalue.Text);
            ThreadPool.RunAsync(
                         (timer) =>
                         {
                             POISearchTest(searchName, cityCode, srcType, number);
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

        private void dataType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataTypeInput == null || dataTypeInput.SelectedItem == null)
            {
                return;
            }
            object o = ((ComboBoxItem)dataTypeInput.SelectedItem).Tag;
            GetValue = o.ToString();
        }




    }
}
