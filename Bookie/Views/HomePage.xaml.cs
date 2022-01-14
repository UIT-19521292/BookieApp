using Bookie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Newtonsoft.Json;
using System.Net.Http;
using Bookie.Custom;

namespace Bookie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(UserID), "UserID")]
    public partial class HomePage : ContentPage
    {
        private string userID { get; set; }
        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                Search.UserID = userID;
            }
        }
        public HomePage()
        {
            InitializeComponent();
            initData();
        }

        async void initData()
        {
            HttpClient httpClient = new HttpClient();
            var bookList = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetBookList");
            var bookListConvert = JsonConvert.DeserializeObject<List<Book>>(bookList);

            Search.Book = new List<Book>(bookListConvert);

            foreach (var book in bookListConvert)
            {
                book.BookDescription = book.BookDescription.Replace("\\n", " \n");
            }

            List<BookGroup> BookGroups = new List<BookGroup>();

            foreach (var book in bookListConvert)
            {
                bool isExist = false;
                foreach (var bookGroupEle in BookGroups)
                {
                    if (bookGroupEle.Title == book.BookGenre)
                        isExist = true;
                }
                if (!isExist)
                {
                    BookGroups.Add(new BookGroup(book.BookGenre));
                }
            }

            foreach (var bookGroupEle in BookGroups)
            {
                var bookCollect = new BookCollection();
                foreach (var book in bookListConvert)
                {

                    if (bookGroupEle.Title == book.BookGenre)
                        bookCollect.Books.Add(book);
                }
                bookGroupEle.Add(bookCollect);
            }
            Lst.ItemsSource = BookGroups;
        }

        private async void Book_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as CollectionView;

            if (item.SelectedItem != null)
            {
                string BookID = (e.CurrentSelection.FirstOrDefault() as Book).BookID.ToString();

                await Shell.Current.GoToAsync($"bookdetail?UserID={userID}&BookID={BookID}");

                item.SelectedItem = null;
            }
        }

        private async void ViewBooksGenre_Clicked(object sender, EventArgs e)
        {
            var Click = (((sender as ImageButton).Parent as StackLayout).Children[0]) as Label;
            await Navigation.PushAsync(new FilterPage(Click.Text,UserID));
        }
    }
}