using Bookie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(UserID), "UserID")]
    public partial class OrderHistoryPage : ContentPage
    {
        private string userID { get; set; }
        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                initData();
            }
        }
        public ObservableCollection<Order> orders { get; set; }
        public OrderHistoryPage()
        {
            InitializeComponent();

        }
        Order status(Order idx)
        {
            if (idx.isCancelled)
            {
                idx.StatusCoverColor = "#F5B7AE";
                idx.StatusTitleColor = "#921314";
                idx.StatusTitle = "Cancelled";
            }
            if (idx.isComplete)
            {
                idx.StatusCoverColor = "#B2EEB1";
                idx.StatusTitleColor = "#4BD350";
                idx.StatusTitle = "Delivered";
            }
            if (idx.isTransported)
            {
                idx.StatusCoverColor = "#FEF4E8";
                idx.StatusTitleColor = "#FF971A";
                idx.StatusTitle = "Pending";
            }
            return idx;
        }
        async void initData()
        {
            if (orders == null)
            {
                HttpClient httpClient = new HttpClient();
                var OrderList = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetOrderByUserID?UserID=" + userID);
                var OrderListConvert = JsonConvert.DeserializeObject<ObservableCollection<Order>>(OrderList);

                orders = new ObservableCollection<Order>();

                foreach (var order in OrderListConvert)
                {
                    orders.Add(status(order));
                }
                src.ItemsSource = orders;
            }
            else
            {
                pk_SelectedIndexChanged(null, null);
            }
        }

        private async void pk_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idx = pk.SelectedIndex == -1 ? 0 : pk.SelectedIndex;

            HttpClient httpClient = new HttpClient();
            var OrderList = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetOrderByUserID?UserID=" + userID);
            var OrderListConvert = JsonConvert.DeserializeObject<ObservableCollection<Order>>(OrderList);

            orders.Clear();

            foreach (var order in OrderListConvert)
            {
                if (idx == 0)
                {
                    orders.Add(status(order));
                }
                if (idx == 1)
                {
                    if (order.isTransported)
                    {
                        orders.Add(status(order));
                    }
                }
                if (idx == 2)
                {
                    if (order.isComplete)
                    {
                        orders.Add(status(order));
                    }
                }
                if (idx == 3)
                {
                    if (order.isCancelled)
                    {
                        orders.Add(status(order));
                    }
                }
            }
        }

        private async void src_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as CollectionView;

            if (item.SelectedItem != null)
            {
                Order OrderSelect = (e.CurrentSelection.FirstOrDefault() as Order);

                await Navigation.PushAsync(new OrderDetailPage(OrderSelect));

                item.SelectedItem = null;
            }

        }
    }
}