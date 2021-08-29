using System.Collections.Generic;

namespace RestAPIExample.BL
{
    public static class Database
    {
        public static List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "Wiedźmin", Author = "Sapkowski" },
            new Book { Id = 2, Title = "Lupus" },
            new Book { Id = 3, Title = "Narkonomia" }
        };
    }
}
