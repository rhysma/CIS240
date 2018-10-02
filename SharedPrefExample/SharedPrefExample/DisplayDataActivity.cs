using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SharedPrefExample
{
    [Activity(Label = "DisplayDataActivity")]
    public class DisplayDataActivity : Activity
    {
        private ISharedPreferences prefs =
            Android.App.Application.Context.GetSharedPreferences("APP_DATA", FileCreationMode.Private);

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.DisplayData);

            //display in the text field
            TextView displayText = FindViewById<TextView>(Resource.Id.DisplayText);

            //get info from Shared Preferences 
            string name = prefs.GetString("name", "No name found");
            string email = prefs.GetString("email", "No email found");

            //put name and email into displaytext
            displayText.Text = "Name: " + name + "\nEmail: " + email;
        }
    }
}