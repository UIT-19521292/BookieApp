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
    public partial class MenuPage : Shell
    {
        public MenuPage()
        {
            InitializeComponent();

            Routing.RegisterRoute("userdetail", typeof(UserDetailPage));
            Routing.RegisterRoute("bookdetail", typeof(BookDetailPage));
            Routing.RegisterRoute("orderhistory", typeof(OrderHistoryPage));
            Routing.RegisterRoute("register", typeof(RegisterPage));
            Routing.RegisterRoute("checkout", typeof(CheckOutPage));
        }
    }
}