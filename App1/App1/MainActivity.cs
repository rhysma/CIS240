using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Content;

namespace App1
{
    [Activity(Label = "App1", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

           
            //define the button
            Button submitButton = FindViewById<Button>(Resource.Id.submitButton);

            //define the second button
            Button secondButton = FindViewById<Button>(Resource.Id.activityButton);

            //define the method that will handle the button OnClick Event
            secondButton.Click += SecondButton_Click;

            //create the button click event
            submitButton.Click += delegate
            {
                //define the edit text 
                EditText username = FindViewById<EditText>(Resource.Id.username);

                //get the value in the box and put it into a variable
                string usernameValue = username.Text;

                //define the text view
                TextView output = FindViewById<TextView>(Resource.Id.outputText);

                //take the value from the edit text and put it into the text view
                output.Text = usernameValue;
            };
        }

        /// <summary>
        /// This is the method that handles the click event for the second button
        /// It opens the second activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SecondButton_Click(object sender, EventArgs e)
        {
            var secondIntent = new Intent(this, typeof(SecondActivity));
            StartActivity(secondIntent);
        }
    }
}

