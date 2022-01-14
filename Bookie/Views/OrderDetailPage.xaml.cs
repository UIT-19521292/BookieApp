using Bookie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDetailPage : ContentPage
    {
        int OrderID { get; set; }   
        public OrderDetailPage()
        {
            InitializeComponent();
        }
        public OrderDetailPage(Order OrderSelect)
        {
            InitializeComponent();

            OrderID = OrderSelect.OrderID;

            BindingContext = OrderSelect;

            initData(OrderSelect.OrderID.ToString());
            if (OrderSelect.isComplete == true || OrderSelect.isCancelled == true)
            {
                check.IsVisible = false;
            }
        }

        async void initData(string OrderID)
        {
            HttpClient httpClient = new HttpClient();
            var YourOrder = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetOrderDetailByOrderID?OrderID=" + OrderID);
            var YourOrderConvert = JsonConvert.DeserializeObject<List<Cart>>(YourOrder);

            Lst.ItemsSource = YourOrderConvert;
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Cancellation", "Are you sure to cancel this order?", "Yes", "No");
            if (answer)
            {
                HttpClient httpClient = new HttpClient();
                await httpClient.PutAsync("http://bkstore.somee.com/api/ServiceController/UpdateCancelledOrderByOrderID?OrderID=" + OrderID, null);
                await this.Navigation.PopAsync();
            }
        }

        private async void Delivered_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Confirmation", "Is your order delivered?", "Yes", "No");

            if (answer)
            {
                HttpClient httpClient = new HttpClient();
                await httpClient.PutAsync("http://bkstore.somee.com/api/ServiceController/UpdateCompleteOrderByOrderID?OrderID=" + OrderID, null);
                await this.Navigation.PopAsync();
            }
        }
    }
}