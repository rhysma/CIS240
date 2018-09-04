using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Views;

namespace ListDemo
{
    [Activity(Label = "ListDemo", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
        string[] items = new string[5];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            //SetContentView(Resource.Layout.Main);

            items[0] = "Item 1";
            items[1] = "Item 2";
            items[2] = "Item 3";
            items[3] = "Item 4";
            items[4] = "Item 5";

            ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);

        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            base.OnListItemClick(l, v, position, id);

            string item = items[position];
            Toast.MakeText(this, "They clicked: " + item, ToastLength.Long).Show();

            if (position == 3)
            {
                var uri = Android.Net.Uri.Parse("http://www.otc.edu");
                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            }

        }
    }
}

