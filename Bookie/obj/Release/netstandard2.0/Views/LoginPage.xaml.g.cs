//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("Bookie.Views.LoginPage.xaml", "Views/LoginPage.xaml", typeof(global::Bookie.Views.LoginPage))]

namespace Bookie.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\LoginPage.xaml")]
    public partial class LoginPage : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Bookie.Custom.myEntry username;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Bookie.Custom.myEntry password;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.Switch ShowPassword;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Bookie.Custom.myButton SignIn;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Bookie.Custom.myButton toRegister;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(LoginPage));
            username = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Bookie.Custom.myEntry>(this, "username");
            password = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Bookie.Custom.myEntry>(this, "password");
            ShowPassword = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.Switch>(this, "ShowPassword");
            SignIn = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Bookie.Custom.myButton>(this, "SignIn");
            toRegister = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Bookie.Custom.myButton>(this, "toRegister");
        }
    }
}