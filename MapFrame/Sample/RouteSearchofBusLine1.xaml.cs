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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Win8.Map.Demo.Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RouteSearchofBusLine : UserControl
    {
        AMap map = new AMap();
        public RouteSearchofBusLine()
        {
            this.InitializeComponent();
            searchContent.Children.Add(map);

            // map.Children.Add(new TextBox() { Text="dfadsaafasfdasfasfasdfsadfasdfasfasdafasasfasf"});

            map.Loaded += map_Loaded;
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
     

        private async void BusRouteSearchClick(object sender, RoutedEventArgs e)
        {
            String startPosition = startInput.Text;
            POISearchOption p = new POISearchOption();
            p.SearchName = startInput.Text;
            p.CityCode = cityInput.Text;
            POISearchResult iopStart = await POISearch.PoiSearchWithOption(p);
            Double Startx=new double();
               Double Endx=new double();
               Double Starty = new double();
               Double Endy = new double(); 
            
            ALngLat lnglatStart;
            if (iopStart.Erro == null)
            {
                foreach (POI poi in iopStart.POIs)
                {
                    poi.Name = poi.Name;
                    Startx = poi.X;
                    Starty = poi.Y;
                    ALngLat lnglat = new ALngLat(poi.X, poi.Y);
                    AMarker poiMarker = new AMarker();
                    poiMarker.LngLat = lnglat;
                    ATip tip = new ATip();
                    tip.Title = poi.Name;
                    tip.ContentText = poi.Address;
                    poiMarker.TipFrameworkElement = tip;
                    map.Children.Add(poiMarker);
                    break;
                }
            }
            else {
                MessageDialog msd = new MessageDialog("起点输入有误，请重新输入！", "提示");
                msd.ShowAsync();          
            }

            String endPosition = endInput.Text;
            p.SearchName = endInput.Text;
            p.CityCode = cityInput.Text;
            POISearchResult iopEnd = await POISearch.PoiSearchWithOption(p);
            if (iopEnd.Erro == null)
            {

                foreach (POI poi in iopEnd.POIs)
                {
                    poi.Name = poi.Name;
                    Endx = poi.X;
                    Endy = poi.Y;
                    ALngLat lnglat = new ALngLat(poi.X, poi.Y);
                    AMarker poiMarker = new AMarker();
                    poiMarker.LngLat = lnglat;
                    ATip tip = new ATip();
                    tip.Title = poi.Name;
                    tip.ContentText = poi.Address;
                    poiMarker.TipFrameworkElement = tip;
                    map.Children.Add(poiMarker);
                    break;
                }   

            }
            else
            {
                MessageDialog msd = new MessageDialog("终点输入有误，请重新输入！", "提示");
                msd.ShowAsync();
            }

            BusRouteSearchOption rgo = new BusRouteSearchOption();

            rgo.X1 = Startx;
            rgo.Y1 = Starty;
            rgo.X2 = Endx;
            rgo.Y2 = Endy;
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

       

       
    }
}
