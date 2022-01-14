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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void toRegister_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//login/register");
        }

        private async void SignIn_Clicked(object sender, EventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            var userList = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetUserList");
            var userListConvert = JsonConvert.DeserializeObject<List<User>>(userList);

            bool isUser = false;

            foreach (var user in userListConvert)
            {
                if ((user.UserPhone == username.Text || user.UserEmail == username.Text) && user.UserPassword == password.Text)
                {
                    isUser = true;

                    await Shell.Current.GoToAsync($"//main/more?UserID={user.UserID.ToString()}");
                    await Shell.Current.GoToAsync($"//main/cart?UserID={user.UserID.ToString()}");
                    await Shell.Current.GoToAsync($"//main/library?UserID={user.UserID.ToString()}");
                    await Shell.Current.GoToAsync($"//main/home?UserID={user.UserID.ToString()}");

                    break;
                }
            }

            if (!isUser)
            {
                await DisplayAlert("Error", "Incorrect login information", "OK");
            }
        }
        private void ShowPassword_Touch(object sender, CheckedChangedEventArgs e)
        {
            if (ShowPassword.IsToggled)
            {
                password.IsPassword = false;
                ShowPassword.ThumbColor = Color.Blue;
            }
            else
            {
                password.IsPassword = true;
                ShowPassword.ThumbColor = Color.Default;
            }
        }
    }
}