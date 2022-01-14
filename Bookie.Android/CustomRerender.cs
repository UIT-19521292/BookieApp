using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using Bookie.Custom;
using Bookie.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(myEntry), typeof(MyEntryRenderer))]
[assembly: ExportRenderer(typeof(myButton), typeof(MyButtonRenderer))]
[assembly: ExportRenderer(typeof(myDatePicker), typeof(MyDatePickerRenderer))]
[assembly: ExportRenderer(typeof(myPicker), typeof(MyPickerRenderer))]
[assembly: ExportRenderer(typeof(myLabel), typeof(MyLabelRenderer))]

namespace Bookie.Droid
{
    public class MyButtonRenderer : ButtonRenderer
    {
        public MyButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            var button = this.Control;
            button.SetAllCaps(false);               
        }
    }
    public class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                //Control.SetBackgroundResource(Resource.Layout.rounded_shape);
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(10f);
                gradientDrawable.SetStroke(3, Android.Graphics.Color.Black);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gradientDrawable);
                Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }
        }
    }
    public class MyDatePickerRenderer : DatePickerRenderer
    {
        public MyDatePickerRenderer(Context context) : base(context)
        {
        }
        myDatePicker element;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {

            base.OnElementChanged(e);

            element = (myDatePicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))    
            {
                Control.Background = AddPickerStyles(element.Image);
            }
        }
        public LayerDrawable AddPickerStyles(string imagePath)
        {
            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(10f);
            gradientDrawable.SetStroke(3, Android.Graphics.Color.Black);
            gradientDrawable.SetColor(Android.Graphics.Color.White);
            Drawable[] layers = { gradientDrawable, GetDrawable(imagePath) };
            Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            myDatePicker element = Element as myDatePicker;

            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
                Control.SetTextColor(Android.Graphics.Color.Gray);
            }
            this.Control.TextChanged += (sender, arg) => {
                Control.SetTextColor(Android.Graphics.Color.Black);
            };

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            var drawable = Resources.GetDrawable(imagePath);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 40, 30, true));
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }
    }
    public class MyPickerRenderer : Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer
    {
        public MyPickerRenderer(Context context) : base(context)
        {
        }
        myPicker element;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (myPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
            {                
                Control.Background = AddPickerStyles(element.Image);
            }

        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(10f);
            gradientDrawable.SetStroke(3, Android.Graphics.Color.Black);
            gradientDrawable.SetColor(Android.Graphics.Color.White);
            Drawable[] layers = { gradientDrawable, GetDrawable(imagePath) };
            Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            myPicker element = Element as myPicker;

            if (!string.IsNullOrWhiteSpace(element.Placeholder))
            {
                Control.Text = element.Placeholder;
                Control.SetTextColor(Android.Graphics.Color.Gray);
            }
            this.Control.TextChanged += (sender, arg) => {
                Control.SetTextColor(Android.Graphics.Color.Black);
            };

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            var drawable = Resources.GetDrawable(imagePath);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 40, 30, true));
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }
    }
    public class MyLabelRenderer : LabelRenderer
    {
        public MyLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var el = (Element as myLabel);

            if (el != null && el.JustifyText)
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                {
                    Control.JustificationMode = Android.Text.JustificationMode.InterWord;
                }

            }
        }
    }
}