using Android.App;
using Android.Widget;
using Android.OS;
using SQLite;
using System;
using Android.Content;

namespace SQLiteDemo
{
    [Activity(Label = "SQLiteDemo", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //resources
        //https://docs.microsoft.com/en-us/xamarin/android/data-cloud/data-access/using-sqlite-orm

        protected override void OnCreate(Bundle savedInstanceState)
        {
            
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //get add button
            Button addButton = FindViewById<Button>(Resource.Id.AddButton);
            addButton.Click += AddButton_Click;

            //get the show button
            Button viewButton = FindViewById<Button>(Resource.Id.ViewButton);
            viewButton.Click += ViewButton_Click;
        }


        private void AddButton_Click(object sender, EventArgs e)
        {
            //setup the path of where the db file will be stored
            string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            //setup a new blank database if one doesn't already exist
            var db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "books.db"));

            //create a new table Book if it doesn't already exist
            db.CreateTable<Book>();

            //get the data from the boxes  
            EditText titleBox = FindViewById<EditText>(Resource.Id.bookTitleBox);
            string title = titleBox.Text;
            EditText authorBox = FindViewById<EditText>(Resource.Id.bookAuthorBox);
            string author = authorBox.Text;
            EditText isbnBox = FindViewById<EditText>(Resource.Id.bookISBNBox);
            string isbn = isbnBox.Text;

            //create a new Book Object
            Book newBook = new Book(title, author, isbn);

            //add the new book to the database
            db.Insert(newBook);

            //clear the boxes
            titleBox.Text = "";
            authorBox.Text = "";
            isbnBox.Text = "";
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            var viewActivity = new Intent(this, typeof(ViewBooksActivity));
            StartActivity(viewActivity);
        }

    }
}

