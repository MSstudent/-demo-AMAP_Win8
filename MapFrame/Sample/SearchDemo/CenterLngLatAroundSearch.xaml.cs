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
    public sealed partial class CenterLngLatAroundSearch : UserControl
    {
        AMap map = new AMap();
        public CenterLngLatAroundSearch()
        {

            this.InitializeComponent();
            searchContent.Children.Add(map);
            Canvas.SetZIndex(PoiListView, 1000);
            map.Loaded += map_Loaded;

        }

        void map_Loaded(object sender, RoutedEventArgs e)
        {

        }



        public ObservableCollection<AMarker> markerList1 = new ObservableCollection<AMarker>();
        String GetValue = "POI";
        async void CenterlnglatAroundPOISearch(string searchName, string cityCode, double x, double y, int range, int number)
        {
            DateTime st = DateTime.Now;
            POISearchOption p = new POISearchOption();
            p.CityCode = cityCode;
            //  p.SrcType = dataTypeInput.SelectedItem;
            p.SrcType = GetValue;
            p.CenX = x;
            p.CenY = y;
            p.Range = range;
            p.Number = number;
            p.Sid = "1002";    //服务编码 默认为1000-关键字查询 1001-根据中心点关键字查询周边兴趣点 1002-根据中心点坐标查询周边兴趣点 1005-以拉框方式查询图形区域内的关键字信息.
            p.SearchName = searchName;


            POISearchResult iop = await POISearch.PoiSearchWithOption(p);

            this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
        {
            
            if (iop.Erro == null)
            {
                TimeSpan elapsedSpan = new TimeSpan(DateTime.Now.Ticks - st.Ticks);
                int i = 0;
                SearchTextGrid.Visibility = Visibility.Collapsed;
                Debug.WriteLine(iop);

                PoiListView.DataContext = iop.POIs;
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
                    markerList1.Add(poiMarker);
                    map.Children.Add(poiMarker);
                }
                map.SetOverlaysFitView();
                PoiListView.SelectedIndex = 0;
                markerList1[0].OpenTip();
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
            markerList1[PoiListView.SelectedIndex].OpenTip();
            map.SetZoomAndCenter(13, markerList1[PoiListView.SelectedIndex].LngLat);
        }


        public Windows.UI.Xaml.Visibility Visible { get; set; }

        private void PoiSearchClick(object sender, RoutedEventArgs e)
        {
            string searchName = PoiInput.Text;
            string cityCode = cityInput.Text;
            double x = double.Parse(centerLng.Text);
            double y = double.Parse(centerLat.Text);
            int range = int.Parse(rangevalue.Text);
            int number = int.Parse(slidervalue.Text);


            ThreadPool.RunAsync(
                     (timer) =>
                     {
                         CenterlnglatAroundPOISearch(searchName, cityCode, x, y, range, number);
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

        private void dataNumber_ValueChanged_1(object sender, RangeBaseValueChangedEventArgs e)
        {

        }



        public double Convertor { get; set; }
    }
}
