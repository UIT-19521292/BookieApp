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
    public partial class ChangePWPage : ContentPage
    {
        private string UserID { get; set; }
        public ChangePWPage()
        {
            InitializeComponent();
        }
        public ChangePWPage(string ID)
        {
            InitializeComponent();
            UserID = ID;
        }

        private async void myButton_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Update")
            {
                HttpClient httpClient = new HttpClient();
                var ListUserAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetUserList");
                var ListUser = JsonConvert.DeserializeObject<List<User>>(ListUserAPI);

                User myAccount = new User();

                foreach (var item in ListUser)
                {
                    if (item.UserID.ToString() == UserID)
                    {
                        myAccount = item;
                        break;
                    }
                }

                if (oldPass.Text == myAccount.UserPassword && newPass.Text != null && newPass.Text == CFnewPass.Text && newPass.Text.Length > 7)
                {
                    await httpClient.PutAsync("http://bkstore.somee.com/api/ServiceController/UpdatePassword?UserID="+UserID+"&UserPassword="+newPass.Text,null);
                    await DisplayAlert("Password Changed", "Your password has been changed successfully!", "OK");
                    await this.Navigation.PopAsync();
                }
                else
                {
                    if (oldPass.Text == null || oldPass.Text != myAccount.UserPassword)
                    {
                        await DisplayAlert("Error", "Your password is incorrect!\nPlease try again.", "OK");
                    }
                    else
                    {
                        if (newPass.Text == null || newPass.Text != CFnewPass.Text)
                        {
                            await DisplayAlert("Error", "Confirmed password doesn't match!\nPlease try again.", "OK");
                        }
                        else
                        {
                            if(newPass.Text.Length <= 7)
                                await DisplayAlert("Error", "Password must be at least 8 characters", "OK");
                            else
                                await DisplayAlert("Error", "Information is incorrect", "OK");
                        }
                    }
                }
            }
            else
            {
                await this.Navigation.PopAsync();
            }
        }
    }
}