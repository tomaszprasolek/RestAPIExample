using System.Collections.Generic;

namespace RestAPIExample.BL
{
    public class Database
    {
        public List<Book> Books;

        public Database()
        {
            Books = new List<Book>
            {
                new Book { Id = 1, Title = "Wiedźmin" },
                new Book { Id = 2, Title = "Lupus" },
                new Book { Id = 3, Title = "Narkonomia" }
            };
        }
    }
}
