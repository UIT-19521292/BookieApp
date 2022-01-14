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
    public partial class EditProfilePage : ContentPage
    {
        public User UserProfile { get; set; }
        public EditProfilePage()
        {
            InitializeComponent();
        }
        public EditProfilePage(User user)
        {
            InitializeComponent();

            UserProfile = user;

            fName.Text = UserProfile.UserName;
            username.Placeholder = UserProfile.UserName;
            dob.Date = UserProfile.UserDob;
            dob.MaximumDate = DateTime.Now;
            dob.Placeholder = String.Format("{0:dd/MM/yyyy}", UserProfile.UserDob);

            gender.Placeholder = UserProfile.UserGender;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "Update")
            {
                if (username.Text != null && username.Text.Trim() != "")
                {
                    UserProfile.UserName = username.Text;
                }

                if (gender.SelectedIndex != -1)
                {
                    UserProfile.UserGender = gender.SelectedIndex == 0 ? "Male" : "Female";
                }
                
                string newDob = String.Format("{0:MM/dd/yyyy}", dob.Date);

                HttpClient httpClient = new HttpClient();
                await httpClient.PutAsync("http://bkstore.somee.com/api/ServiceController/UpdateUser?UserID=" + UserProfile.UserID.ToString() + "&UserName=" + UserProfile.UserName + "&UserGender=" + UserProfile.UserGender + "&UserDob=" + newDob, null);
                await DisplayAlert("Update Successfully", "Your profile has changed!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                if ((username.Text == null || username.Text.Trim() == "" )&& dob.Date == UserProfile.UserDob && gender.SelectedIndex == -1)
                {
                    await Navigation.PopAsync();
                }
                else
                {
                    bool ans = await DisplayAlert("Leave without saving?", "Your information will be lost", "OK", "Save");
                    if (ans)
                    {
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        if (username.Text != null && username.Text.Trim() != "")
                        {
                            UserProfile.UserName = username.Text;
                        }

                        UserProfile.UserGender = gender.SelectedIndex == 0 ? "Male" : "Female";
                        string newDob = String.Format("{0:MM/dd/yyyy}", dob.Date);

                        HttpClient httpClient = new HttpClient();
                        await httpClient.PutAsync("http://bkstore.somee.com/api/ServiceController/UpdateUser?UserID=" + UserProfile.UserID.ToString() + "&UserName=" + UserProfile.UserName + "&UserGender=" + UserProfile.UserGender + "&UserDob=" + newDob, null);
                        await DisplayAlert("Update Successfully", "Your profile has changed!", "OK");
                        await Navigation.PopAsync();
                    }
                }
            }
        }
    }

}