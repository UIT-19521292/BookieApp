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
    public partial class UserDetailPage : ContentPage
    {
        public User UserProfile { get; set; }
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
        public UserDetailPage()
        {
            InitializeComponent();
        }
        async void init()
        {
            HttpClient httpClient = new HttpClient();
            var UserListAPI = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetUserList");
            var UserListAPICovert = JsonConvert.DeserializeObject<List<User>>(UserListAPI);
            

            var found = UserListAPICovert.FirstOrDefault(x => x.UserID.ToString() == userID);         
            fName.Text = found.UserName;
            dob.Text = String.Format("{0:dd/MM/yyyy}", found.UserDob);
            gender.Text = found.UserGender;
            email.Text = found.UserEmail;
            phone.Text = found.UserPhone;
            UserProfile = found;
        }

        private async void EditProfile_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new EditProfilePage(UserProfile));
        }
    }
}