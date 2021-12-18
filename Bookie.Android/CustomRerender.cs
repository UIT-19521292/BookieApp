using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
                gradientDrawable.SetStroke(5, Android.Graphics.Color.Black);
                gradientDrawable.SetColor(Android.Graphics.Color.White);
                Control.SetBackground(gradientDrawable);
                Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
            }
        }
    }
}