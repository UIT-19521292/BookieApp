using System;
using System.Collections.Generic;
using System.Text;

namespace Bookie.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string BookTitle { get; set; }
        public string BookGenre { get; set; }
        public string BookAuthor { get; set; }
        public string BookImg { get; set; }
        public double BookPrice { get; set; }
        public int BookQuantity { get; set; }
        public string BookDescription { get; set; }
        public bool IsStock { get; set; }
    }
    public class BookCollection
    {
        public List<Book> Books { get; private set; }

        public BookCollection()
        {
            Books = new List<Book>();
        }

        public void Add(Book Book)
        {
            Book.IsStock = Book.BookQuantity > 0 ? true : false;
            Books.Add(Book);
        }

    }

    public class BookGroup : List<BookCollection>
    {
        public string Title { get; private set; }

        public BookGroup(string title)
        {
            Title = title;
        }
    }
}
