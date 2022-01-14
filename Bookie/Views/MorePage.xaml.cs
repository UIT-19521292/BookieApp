using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(UserID), "UserID")]
    public partial class MorePage : ContentPage
    {
        private string userID { get; set; }
        public string UserID
        {
            get { return userID; }
            set
            {
                userID = value;
            }
        }
        public MorePage()
        {
            InitializeComponent();
        }

        private async void UserDetail_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"userdetail?UserID={userID}");
        }

        private async void OrderHistory_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"orderhistory?UserID={userID}");
        }

        private async void LogOut_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//login");
        }

        private async void myButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePWPage(UserID));
        }
    }
}