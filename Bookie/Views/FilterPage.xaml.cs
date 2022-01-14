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
	public partial class FilterPage : ContentPage
	{
		public string UserID { get; set; }
		public List<Book> BooksGenre { get; set; }
		public FilterPage ()
		{
			InitializeComponent ();
		}
		public FilterPage(string title, string ID)
		{
			InitializeComponent();
			Title = title;
			UserID = ID;
			initData(title);
		}
		async void initData(string title)
        {
			HttpClient httpClient = new HttpClient();
			var bookList = await httpClient.GetStringAsync("http://bkstore.somee.com/api/ServiceController/GetBookList");
			var bookListConvert = JsonConvert.DeserializeObject<List<Book>>(bookList);

			BooksGenre = new List<Book>();

			foreach(var book in bookListConvert)
            {
				if(book.BookGenre == title)
                {
					BooksGenre.Add(book);
                }
            }

			Src.ItemsSource = BooksGenre;
		}
		private async void Book_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var Click = (sender as CollectionView);

			if (Click.SelectedItem != null)
			{
				string BookID = (e.CurrentSelection.FirstOrDefault() as Book).BookID.ToString();

				await Shell.Current.GoToAsync($"bookdetail?UserID={UserID}&BookID={BookID}");

				Click.SelectedItem = null;
			}
		}
	}
}