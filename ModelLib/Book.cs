using System;
using System.Collections.Generic;
using System.Text;

namespace Obligatorisk_Opgave_1_Unit_Test
{
    public class Book
    {
        private string _title;
        private string _author;
        private int _pages;
        private string _isbn13;

        public Book(string title, string author, int pages, string isbn13)
        {
            Title = title;
            Author = author;
            Pages = pages;
            Isbn13 = isbn13;
        }

        public Book()
        {
            
        }

        public string Isbn13
        {
            get => _isbn13;
            set
            {
                _isbn13 = value;
                CheckIsbnRegex(value);
                CheckIsbn(value);
            }
        }

        public int Pages
        {
            get => _pages;
            set
            {
                _pages = value;
                CheckPages(value);
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                CheckAuthor(value);
            }
        }

        public string Title
        {
            get => _title;
            set 
            {
                _title = value;
                CheckTitle(value);
            }
        }


        public void CheckTitle(string title)
        {
            if (title.Length < 2)
            {
                throw new ArgumentOutOfRangeException("Title cannot be lower then 2 characters!");
            }
        }

        public void CheckAuthor(string author)
        {
            if (string.IsNullOrEmpty(author))
            {
                throw new ArgumentNullException("The book must have an author!");
            }
        }

        public void CheckPages(int pages)
        {
            if (Pages > 1000)
            {
                throw  new ArgumentOutOfRangeException("The book has too many pages! Must be 1000 or under!");
            }
            else if (Pages < 10)
            {
                throw  new ArgumentOutOfRangeException("The book has to few pages! Must be 10 or over!");
            }
        }


        public void CheckIsbn(string isbn13)
        {
            if (Isbn13.Length > 13)
            {
                throw new ArgumentOutOfRangeException("The ISBN is over 13! Must be exactly 13!");
            }
            else if (Isbn13.Length < 13)
            {
                throw new ArgumentOutOfRangeException("The ISBN is under 13! Must be exactly 13!");
            }
        }

        public void CheckIsbnRegex(string isbn13)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(isbn13, "^[0-9]*$")) 
            {
                throw new ArgumentException("The ISBN can only contain numbers!");
            }
        }


    }
}
