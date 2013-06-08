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
    public sealed partial class CenterWordAroundSearch : UserControl
    {
        AMap map = new AMap();
        public CenterWordAroundSearch()
        {

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
        String GetValue = "POI";
        async void CenterWordAroundPOISearch(string searchName, string cityCode, string centerName, int number, string searchType)
        {
            DateTime st = DateTime.Now;
            //Debug.WriteLine(DateTime.Now.Ticks);
            POISearchOption p = new POISearchOption();
            p.CenName = centerName;
            p.SearchName = searchName;
            p.CityCode = cityCode;
            //  p.SrcType = dataTypeInput.SelectedItem;
            p.SrcType = GetValue;
            p.Number = number;
            p.SearchType = searchType;
            p.Sid = "1001";   //服务编码 默认为1000-关键字查询 1001-根据中心点关键字查询周边兴趣点 1002-根据中心点坐标查询周边兴趣点 1005-以拉框方式查询图形区域内的关键字信息.
            //  p.Number = dataNumber.Value;
            POISearchResult iop = await POISearch.PoiSearchWithOption(p);
            // POISearchResult iop = await POISearch.PoiSearchByRegionAndKeyword("中关村", "116.204,39.9938;116.456,39.8833", "rectangle", 10);
            this.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
        {
           

            if (iop.Erro == null)
            {

                PoiListView.DataContext = iop.POIs;
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
            string centerName = centerInput.Text;
            int number = int.Parse(slidervalue.Text);
            string searchType = TypeInput.Text;
            ThreadPool.RunAsync(
                     (timer) =>
                     {
                         CenterWordAroundPOISearch(searchName, cityCode, centerName, number, searchType);
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


    }
}
