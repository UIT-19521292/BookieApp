using Bookie.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(UserID), "UserID")]
    public partial class LibraryPage : TabbedPage
    {
        public string userID { get; set; }
        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
                initData();
            }
        }
        public ObservableCollection<Book> FavoriteList { get; set; }
        public ObservableCollection<Book> InteractedList { get; set; }
        public LibraryPage()
        {
            InitializeComponent();
        }
        async void initData()
        {
            if (FavoriteList==null&& InteractedList==null)
            {
                HttpClient httpClient = new HttpClient();

                var FavoriteListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetLikedByUserID?UserID=" + userID);
                var FavoriteListAPIConvert = JsonConvert.DeserializeObject<ObservableCollection<Book>>(FavoriteListAPI);

                var InteractedListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetViewedByUserID?UserID=" + userID);
                var InteractedListAPIConvert = JsonConvert.DeserializeObject<ObservableCollection<Book>>(InteractedListAPI);
                FavoriteList = new ObservableCollection<Book>(FavoriteListAPIConvert);
                InteractedList = new ObservableCollection<Book>(InteractedListAPIConvert);
                Children[0].BindingContext = this;
                Children[1].BindingContext = this;
            }
            
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (userID != null)
            {
                InteractedList.Clear();
                FavoriteList.Clear();

                HttpClient httpClient = new HttpClient();

                var FavoriteListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetLikedByUserID?UserID=" + userID);
                var FavoriteListAPIConvert = JsonConvert.DeserializeObject<ObservableCollection<Book>>(FavoriteListAPI);

                var InteractedListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetViewedByUserID?UserID=" + userID);
                var InteractedListAPIConvert = JsonConvert.DeserializeObject<ObservableCollection<Book>>(InteractedListAPI);

                foreach (var item in InteractedListAPIConvert)
                {
                    InteractedList.Add(item);
                }
                foreach (var item in FavoriteListAPIConvert)
                {
                    FavoriteList.Add(item);
                }
            }   
        }
    }
}