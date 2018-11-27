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

namespace SQLiteDemo
{
    [Table("Items")]
    public class Book
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public Book()
        {

        }

        public Book(string title, string author, string isbn)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }

        public override string ToString()
        {
            return ISBN + "  " + Title + " by: " + Author;
        }
    }
}