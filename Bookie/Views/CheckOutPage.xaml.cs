using Bookie.Models;
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
    public partial class CheckOutPage : ContentPage
    {
        public string UserID { get; set; }
        public List<Cart> carts { get; set; }
        public double Total { get; set; }
        public CheckOutPage()
        {
            InitializeComponent();
        }
        public CheckOutPage(List<Cart> Lst, string userID)
        {
            InitializeComponent();

            this.carts = new List<Cart>(Lst);
            this.UserID = userID;
            Total = 0;

            foreach (Cart item in carts)
            {
                Total += item.Quantity * item.BookPrice;
            }
            BindingContext = this;
        }
        private async void Confirm_Clicked(object sender, EventArgs e)
        {
            if (adr.Text == null || adr.Text.Trim() == "")
            {
                await DisplayAlert("Error", "Address can not be empty!\nPlease try again.", "Ok");
            }
            else
            {
                HttpClient httpClient = new HttpClient();
                var mess = await httpClient.PostAsync("http://bkstore.somee.com/api/ServiceController/AddOrder?ShipAddress=" + adr.Text + "&OrderTotal=" + Total.ToString().Replace(',','.') + "&UserID=" + UserID, null);
                var OrderID = await mess.Content.ReadAsStringAsync();
                foreach (Cart item in carts)
                {
                    await httpClient.PostAsync("http://bkstore.somee.com/api/ServiceController/AddOrderDetail?OrderID=" + OrderID + "&BookID=" + item.BookID.ToString() + "&Quantity=" + item.Quantity.ToString(), null);
                    await httpClient.DeleteAsync("http://bkstore.somee.com/api/ServiceController/RemoveFromCart?UserID=" + UserID + "&BookID=" + item.BookID.ToString());
                }

                await DisplayAlert("Congratulation!", "Your order has been placed.", "OK");

                await Shell.Current.GoToAsync($"//main/cart?UserID={UserID}");
                await Shell.Current.GoToAsync($"//main/more?UserID={UserID}");
                await Shell.Current.GoToAsync($"orderhistory?UserID={UserID}");
            }
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync();
        }
    }
}