using Bookie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bookie.Custom
{
    public class BookearchHandler : SearchHandler
    {
        public string UserID { get; set; }
        public IList<Book> Book { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = Book
                    .Where(book => book.BookTitle.ToLower().Contains(newValue.ToLower()))
                    .ToList<Book>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            // Let the animation complete
            await Task.Delay(500);

            ShellNavigationState state = (App.Current.MainPage as Shell).CurrentState;
            await Shell.Current.GoToAsync($"bookdetail?UserID={UserID}&BookID={((Book)item).BookID}");
        }

    }
}
