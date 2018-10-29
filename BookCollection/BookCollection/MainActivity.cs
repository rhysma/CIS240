using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Android.Content;
using SQLite;


namespace BookCollection
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //get the instance of the submit button
            Button submitButton = FindViewById<Button>(Resource.Id.submitButton);

            //setup the click event for the submit button
            submitButton.Click += SubmitButton_Click;

            //get the instance of the scan button
            Button scanButton = FindViewById<Button>(Resource.Id.scanButton);

            //setup the click event for the scan button
            scanButton.Click += ScanButton_Click;

        }


        private void SubmitButton_Click(object sender, EventArgs e)
        {
            //get the isbn number from the box
            EditText isbn = FindViewById<EditText>(Resource.Id.isbn);
            string bookISBN = isbn.Text;

            //pass the content from the api over to the book details activity
            var bookActivity = new Intent(this, typeof(BookDetailsActivity));
            bookActivity.PutExtra("bookDetails", bookISBN);
            StartActivity(bookActivity);

        }

        private void ScanButton_Click(object sender, EventArgs e)
        {
            var scanActivity = new Intent(this, typeof(ScanActivity));
            StartActivity(scanActivity);
        }
    }
}

