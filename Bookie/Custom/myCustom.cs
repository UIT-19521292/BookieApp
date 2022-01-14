using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bookie.Custom
{
    public class myButton : Button
    {
    }
    public class myEntry : Entry
    {
    }
    public class myDatePicker : DatePicker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(myDatePicker), string.Empty);

        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string), declaringType: typeof(myDatePicker), defaultValue: default(string));
        public string Placeholder
        {
            get;
            set;
        }
        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
    public class myPicker : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(myPicker), string.Empty);

        public static readonly BindableProperty EnterTextProperty = BindableProperty.Create(propertyName: "Placeholder", returnType: typeof(string), declaringType: typeof(myPicker), defaultValue: default(string));
        public string Placeholder
        {
            get;
            set;
        }
        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
    public class myLabel : Label
    {
        public static readonly BindableProperty JustifyTextProperty =
            BindableProperty.Create(
                propertyName: nameof(JustifyText),
                returnType: typeof(Boolean),
                declaringType: typeof(myLabel),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay
         );

        public bool JustifyText
        {
            get { return (Boolean)GetValue(JustifyTextProperty); }
            set { SetValue(JustifyTextProperty, value); }
        }
    }
}
