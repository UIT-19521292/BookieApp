using Bookie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(UserID), "UserID")]
    public partial class CartPage : ContentPage
    {
        public ObservableCollection<Cart> CartList { get; set; }
        private string userID { get; set; }
        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                init();
            }
        }
        public CartPage()
        {
            InitializeComponent();
        }

        async void init()
        {
            if (CartList == null)
            {
                HttpClient httpClient = new HttpClient();
                var CartListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetCartByUserID?UserID=" + userID);
                var CartListAPIConvert = JsonConvert.DeserializeObject<ObservableCollection<Cart>>(CartListAPI);

                CartList = new ObservableCollection<Cart>(CartListAPIConvert);
                Lst.ItemsSource = CartList;

                BindingContext = this;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (userID != null)
            {
                CartList.Clear();
                HttpClient httpClient = new HttpClient();
                var CartListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetCartByUserID?UserID=" + userID);
                var CartListAPIConvert = JsonConvert.DeserializeObject<ObservableCollection<Cart>>(CartListAPI);

                foreach (var cart in CartListAPIConvert)
                    CartList.Add(cart);

                Lst.SelectedItems.Clear();
            }
        }

        private void Book_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            double sum = 0;

            foreach (Cart item in Lst.SelectedItems)
            {
                sum += item.Quantity * item.BookPrice;
            }

            Total.Text = String.Format("${0}", sum);
        }

        private async void Plus_Clicked(object sender, EventArgs e)
        {
            var Click = ((sender as ImageButton).Parent as Grid);
            var BookID = (Click.Children[0] as Label).Text;

            HttpClient httpClient = new HttpClient();
            var CartListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetCartByUserID?UserID=" + userID);
            var CartListAPIConvert = JsonConvert.DeserializeObject<List<Cart>>(CartListAPI);

            foreach (var item in CartListAPIConvert)
            {
                if (item.BookID.ToString() == BookID)
                {
                    if (item.Quantity < item.BookQuantity)
                    {
                        var found = CartList.FirstOrDefault(x => x.BookID.ToString() == BookID);

                        int idx = CartList.IndexOf(found);
                        int idxZ = Lst.SelectedItems.IndexOf(found);

                        item.Quantity += 1;

                        CartList[idx] = item;
                        if (idxZ >= 0)
                            Lst.SelectedItems[idxZ] = item;

                        await httpClient.PutAsync("http://bkstore.somee.com/api/ServiceController/UpdateBookQuantity?UserID=" + userID + "&BookID=" + item.BookID + "&Quantity=" + item.Quantity, null);
                        Book_CheckedChanged(null, null);
                    }
                    else
                    {
                        await DisplayAlert("Message", "Maximum quantity", "OK");
                    }
                    break;
                }
            }
        }
        private async void Minus_Clicked(object sender, EventArgs e)
        {
            var Click = ((sender as ImageButton).Parent as Grid);
            var BookID = (Click.Children[0] as Label).Text;

            HttpClient httpClient = new HttpClient();
            var CartListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetCartByUserID?UserID=" + userID);
            var CartListAPIConvert = JsonConvert.DeserializeObject<List<Cart>>(CartListAPI);

            foreach (var item in CartListAPIConvert)
            {
                if (item.BookID.ToString() == BookID)
                {
                    if (item.Quantity > 1)
                    {
                        var found = CartList.FirstOrDefault(x => x.BookID.ToString() == BookID);

                        int idx = CartList.IndexOf(found);
                        int idxZ = Lst.SelectedItems.IndexOf(found);

                        item.Quantity -= 1;

                        CartList[idx] = item;
                        if (idxZ >= 0)
                            Lst.SelectedItems[idxZ] = item;

                        await httpClient.PutAsync("http://bkstore.somee.com/api/ServiceController/UpdateBookQuantity?UserID=" + userID + "&BookID=" + item.BookID + "&Quantity=" + item.Quantity, null);
                        Book_CheckedChanged(null, null);
                    }
                    break;
                }
            }
        }
        private async void deleteCart_Clicked(object sender, EventArgs e)
        {
            var Click = (((sender as ImageButton).Parent as Grid).Children[3] as FlexLayout).Children[0] as Grid;
            var BookID = (Click.Children[0] as Label).Text;

            bool answer = await DisplayAlert("Confirm", "Are you sure to delete this item?", "Yes", "No");

            if (answer)
            {
                var found = CartList.FirstOrDefault(x => x.BookID.ToString() == BookID);

                int idx = CartList.IndexOf(found);
                int idxZ = Lst.SelectedItems.IndexOf(found);

                CartList.RemoveAt(idx);
                if (idxZ >= 0)
                    Lst.SelectedItems.RemoveAt(idxZ);

                HttpClient httpClient = new HttpClient();
                await httpClient.DeleteAsync("http://bkstore.somee.com/api/ServiceController/RemoveFromCart?UserID=" + userID + "&BookID=" + BookID);
            }
        }

        private async void CheckOut_Clicked(object sender, EventArgs e)
        {
            if (Lst.SelectedItems.Count == 0)
            {
                await DisplayAlert("Your order is empty", "Select at least one item", "OK");
            }
            else
            {
                List<Cart> books = new List<Cart>();
                foreach (Cart book in Lst.SelectedItems)
                    books.Add(book);
                await Navigation.PushAsync(new CheckOutPage(books, userID));
            }
        }
    }
}