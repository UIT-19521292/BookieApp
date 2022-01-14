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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            dob.MaximumDate = DateTime.Now;
        }

        private async void toLogin_Clicked(object sender, EventArgs e)
        {
            await this.Navigation.PopAsync(); 
        }
        private void ShowPassword_Touch(object sender, CheckedChangedEventArgs e)
        {
            if(ShowPassword.IsToggled)
            {
                password.IsPassword = false;
                confirmPassword.IsPassword = false;
                ShowPassword.ThumbColor = Color.Blue;
            }
            else
            {
                password.IsPassword = true;
                confirmPassword.IsPassword = true;
                ShowPassword.ThumbColor = Color.Default;
            }
        }
        private async void SignUp_Clicked(object sender, EventArgs e)
        {
            bool isValid = true;
            if (username.Text == null || username.Text.Trim() == "" ||
                password.Text == null || password.Text == "" ||
                confirmPassword.Text == null || confirmPassword.Text == "" ||
                phonenumber.Text == null || phonenumber.Text.Trim() == "" ||
                email.Text == null || email.Text.Trim() == "" ||
                dob.Date.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") ||
                gender.SelectedIndex == -1)
            {    
                await DisplayAlert("Error", "Input missing information", "OK");
                isValid = false;
            }

            if (isValid && password.Text != confirmPassword.Text)
            {
                await DisplayAlert("Error", "Confirm password does not match", "OK");
                isValid = false;
            }

            if (isValid)
                if (password.Text.Length<8)
                {
                    await DisplayAlert("Error", "Password must be at least 8 characters", "OK");
                    isValid = false;
                }

            if (isValid)
            {
                HttpClient httpClient = new HttpClient();
                var userList = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetUserList");
                var userListConvert = JsonConvert.DeserializeObject<List<User>>(userList);

                foreach (var user in userListConvert)
                {
                    if (user.UserEmail == email.Text || user.UserPhone == phonenumber.Text)
                    {
                        await DisplayAlert("Error", "Phone number or email already exists", "OK");
                        isValid = false;
                        break;
                    }
                }
            }
            if(isValid)
            {
                HttpClient httpClient = new HttpClient();
                await httpClient.PostAsync("http://bkstore.somee.com/api/ServiceController/AddUser?UserName=" +username.Text+ "&UserPhone=" + phonenumber.Text + "&UserEmail=" + email.Text + "&UserGender=" + gender.Items[gender.SelectedIndex] + "&UserDob=" + dob.Date.ToString("MM/dd/yyyy") + " &UserPassword=" + password.Text,null);
                await DisplayAlert("Registered successfully", "Please log in to continue!", "OK");
                await this.Navigation.PopAsync();
            }
        }
    }
}