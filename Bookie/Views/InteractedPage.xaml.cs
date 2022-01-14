using Bookie.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bookie.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InteractedPage : ContentPage
    {
        public InteractedPage()
        {
            InitializeComponent();
        }
        private async void Book_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string UserID = (BindingContext as LibraryPage).UserID;
            var Click = (sender as CollectionView);

            if (Click.SelectedItem != null)
            {
                Book bookDetail = (Book)Click.SelectedItem;

                await Shell.Current.GoToAsync($"bookdetail?UserID={UserID}&BookID={bookDetail.BookID.ToString()}");

                ((CollectionView)sender).SelectedItem = null;           
            }
        }

    }
}