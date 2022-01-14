using Bookie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(BookID), "BookID")]
    [QueryProperty(nameof(UserID), "UserID")]
    public partial class BookDetailPage : ContentPage
    {
        public List<Book> ViewedList { get; set; }
        private int amountBook { get; set; }
        private string userID { get; set; }
        public string UserID
        {
            get => userID;
            set
            {
                userID = value;
                initData();
            }
        }
        private string bookID { get; set; }
        public string BookID
        {
            get => bookID;
            set
            {
                bookID = value;
            }
        }
        public BookDetailPage()
        {
            InitializeComponent();
        }

        async void initData()
        {
            amountBook = 1;
            HttpClient httpClient = new HttpClient();

            var x = await httpClient.PostAsync("http://bkstore.somee.com/api/ServiceController/AddToViewed?UserID=" + UserID + "&BookID=" + BookID, null);

            var book = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetBookByBookID?BookID=" + BookID);
            var bookCovert = JsonConvert.DeserializeObject<List<Book>>(book);

            bookCovert[0].IsStock = bookCovert[0].BookQuantity > 0 ? true : false;

            if(bookCovert[0].IsStock)
            {
                stock.IsVisible = false;
            }
            else
            {
                stock.IsVisible = true;
                btn_AtC.Opacity = 0.4;
            }

            bookCovert[0].BookDescription = "\r\r\r\r\r" + bookCovert[0].BookDescription.Replace("\\n", "\n\n\r\r\r\r\r");

            BindingContext = bookCovert[0];

            var LikedBook = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetLikedByUserID?UserID=" + UserID);
            var LikedBookCovert = JsonConvert.DeserializeObject<List<Book>>(LikedBook);

            bool isTym = false;
            foreach(Book item in LikedBookCovert)
            {
                if(item.BookID.ToString() == BookID)
                {
                    tym.Source = "likedIcon";
                    isTym = true;
                    break;
                }
            }
            if(!isTym)
            {
                tym.Source = "likeIcon";
            }

            var ViewedBook = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetViewedByUserID?UserID=" + UserID);
            var ViewedBookCovert = JsonConvert.DeserializeObject<List<Book>>(ViewedBook);

            ViewedList = new List<Book>(ViewedBookCovert);

            viewed.ItemsSource = ViewedBookCovert;
        }

        private void myButton_Clicked(object sender, EventArgs e)
        {
            if (stock.IsVisible == false && fs_numBook.IsVisible == false)
            {
                fs_numBook.IsVisible = true;
                btn_AtC.IsVisible = false;
                amountBook = 1;
                numberBook.Text = "1";
            }
            else
            {
                fs_numBook.IsVisible = false;
                btn_AtC.IsVisible = true;
            }
        }
        private async void plus_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var book = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetBookByBookID?BookID=" + BookID);
            var bookCovert = JsonConvert.DeserializeObject<List<Book>>(book);

            if (amountBook < bookCovert[0].BookQuantity)
            {
                ++amountBook;
                numberBook.Text = amountBook.ToString();
            }
            else
            {
                await DisplayAlert("Message", "Maximum quantity", "OK");
            }
        }
        private  void minus_Clicked(object sender, EventArgs e)
        {
            if (amountBook > 0)
            {
                --amountBook;
                numberBook.Text = amountBook.ToString();
            }
        }
        private async void handleFS_Clicked(object sender, EventArgs e)
        {
            fs_numBook.IsVisible = false;
            btn_AtC.IsVisible = true;
            if(((Button)sender).Text == "Confirm")
            {
                HttpClient httpClient = new HttpClient();
                await httpClient.PostAsync("http://bkstore.somee.com/api/ServiceController/AddBookToCart?UserID=" + UserID + "&BookID=" + BookID+ "&Quantity="+amountBook.ToString(), null);

                await DisplayAlert("Added successfully", "Item has been added to your cart", "OK");

                while (this.Navigation.NavigationStack.Count >1)
                {
                    this.Navigation.RemovePage(this.Navigation.NavigationStack[1]);
                }           
                await Shell.Current.GoToAsync($"//main/cart?UserID={UserID}");
            }
        }
        private async void Tym_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            await httpClient.PostAsync("http://bkstore.somee.com/api/ServiceController/AddToLiked?UserID="+UserID+"&BookID=" + BookID,null);
            if (tym.Source.ToString() == "File: likeIcon")
            {
                tym.Source = "likedIcon";
            }
            else
            {
                tym.Source = "likeIcon";
            }
        }

    }
}