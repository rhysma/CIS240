using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using Android.Content.Res;
using Android.Text;

namespace HTMLLayout
{
    [Activity(Label = "HTMLLayout", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            //get a reference to the textview
            TextView tv = FindViewById<TextView>(Resource.Id.htmlText);

            //the string that holds the HTML we are reading in
            string htmlString = "";

            //define the asset manager we are using
            AssetManager assets = this.Assets;

            //read in the html file and add that data to the string
            using (StreamReader sr = new StreamReader(assets.Open("doc.htm")))
            {
                htmlString = sr.ReadToEnd();
            }

            //apply what is the html string to the textview
            tv.SetText(Html.FromHtml(htmlString), TextView.BufferType.Spannable);
        }
    }
}

