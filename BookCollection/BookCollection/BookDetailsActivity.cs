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
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SQLite;

namespace BookCollection
{
    [Activity(Label = "BookDetailsActivity")]
    public class BookDetailsActivity : Activity
    {
         string WEBSERVICE_URL = "https://www.googleapis.com/books/v1/volumes?q=isbn:";
        string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
        Book currentBook;
        string bookISBN;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //setup a new blank database if one doesn't already exist
            var db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "BookCollection.db"));

            db.CreateTable<IndustryIdentifier>();
            //db.CreateTable<ReadingModes>();
            //db.CreateTable<ImageLinks>();
            //db.CreateTable<SaleInfo>();
            //db.CreateTable<Epub>();
            //db.CreateTable<Pdf>();
            //db.CreateTable<SearchInfo>();
            //db.CreateTable<AccessInfo>();
            //db.CreateTable<VolumeInfo>();
            //db.CreateTable<Item>();
            //db.CreateTable<Book>();


            // Create your application here
            // Set our view from the layout resource
            SetContentView(Resource.Layout.bookDetails);

            //get the textview control
            TextView details = FindViewById<TextView>(Resource.Id.details);

            //get the content from the activity that is passed in
            bookISBN = Intent.GetStringExtra("bookDetails");

            try
            {
                WEBSERVICE_URL = WEBSERVICE_URL + bookISBN;

                var webRequest = WebRequest.Create(WEBSERVICE_URL);

                if (webRequest != null)
                {
                    webRequest.Method = "GET";
                    webRequest.ContentType = "application/json";

                    //Get the response 
                    WebResponse wr = webRequest.GetResponseAsync().Result;
                    Stream receiveStream = wr.GetResponseStream();
                    StreamReader reader = new StreamReader(receiveStream);

                    currentBook = JsonConvert.DeserializeObject<Book>(reader.ReadToEnd());

                    foreach(var item in currentBook.items)
                    {
                        details.Text = "Title: " + item.volumeInfo.title + "\n";
                        foreach(var author in item.volumeInfo.authors)
                        {
                            details.Text += "Author: " + author + "\n";
                        }
                        details.Text += "Published Date: " + item.volumeInfo.publishedDate + "\n";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                //check to see if this book is already available in the database
                var hasBook = db.Query<IndustryIdentifier>("select * from IndustryIdentifier where identifier=?", bookISBN);
                TextView responseText = FindViewById<TextView>(Resource.Id.bookResponse);

                if (hasBook.Count != 0 )
                {
                    //a book has been found
                    responseText.SetTextColor(Android.Graphics.Color.Green);
                    responseText.Text = "You already own this book";
                }
                else
                {
                    //a book has not been found
                    //if it isn't we're going to ask to add it
                    responseText.SetTextColor(Android.Graphics.Color.Red);
                    responseText.Text = "You do not own this book";
                    Button addButton = FindViewById<Button>(Resource.Id.addButton);
                    addButton.Click += AddButton_Click;
                    addButton.Visibility = ViewStates.Visible;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            //add the book into the collection
            //setup a new blank database if one doesn't already exist
            try
            {
                SQLiteConnection db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "BookCollection.db"));
                var command = db.CreateCommand("Insert into IndustryIdentifier (type, identifier) values (?, ?)", "ISBN", bookISBN);
                command.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

    }
}