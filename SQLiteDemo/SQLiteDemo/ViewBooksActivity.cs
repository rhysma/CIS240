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
using SQLite;
using System.Collections;

namespace SQLiteDemo
{
    [Activity(Label = "ViewBooksActivity")]
    public class ViewBooksActivity : ListActivity
    {
        List<Book> myBooks;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ViewBooks);

            //setup the list to hold the books
            myBooks = new List<Book>();

            //setup the path of where the db file will be stored
            string dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            //setup a new blank database if one doesn't already exist
            var db = new SQLiteConnection(System.IO.Path.Combine(dbpath, "books.db"));

            //create a new table Book if it doesn't already exist
            db.CreateTable<Book>();

            //read each book that is in the table
            var table = db.Table<Book>();
            foreach(var b in table)
            {
                myBooks.Add(b);
            }

            ListAdapter = new ArrayAdapter<Book>(this, Android.Resource.Layout.SimpleListItem1, myBooks);
        }
    }
}