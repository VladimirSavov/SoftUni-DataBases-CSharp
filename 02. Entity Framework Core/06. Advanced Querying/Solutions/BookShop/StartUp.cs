namespace BookShop
{
    using BookShop.Models.Enums;
    using Data;
    using Initializer;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
             using var db = new BookShopContext();
            //DbInitializer.ResetDatabase(db);

            //int input = int.Parse(Console.ReadLine());
            var result = GetMostRecentBooks(db);
            Console.WriteLine(result);
        }
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var titles = context
                .Books
                .ToList()
                .Where(x => x.AgeRestriction.ToString().ToLower() == command.ToLower())
                .Select(x => x.Title)
                .OrderBy(x => x)
                .ToList();
            foreach ( var title in titles )
            {
                stringBuilder.AppendLine(title);
            }
            return stringBuilder.ToString();    
        }
        public static string GetGoldenBooks(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .ToList()
                .Where(b => b.Copies < 5000 && b.EditionType == EditionType.Gold)
                .Select(b => new
                {
                    b.Title,
                    b.BookId
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach ( var book in books )
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString();
        }

        public static string GetBooksByPrice(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.Price > 40)
                .Select(b => new
                {
                    b.Price,
                    b.Title
                })
                .OrderByDescending(b => b.Price)
                .ToList();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.Price}$");
            }
            return sb.ToString();
        }
        public static string GetBooksNotReleasedIn(BookShopContext context, int year)
        {
            StringBuilder sb = new StringBuilder();

            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year != year)
                .Select(b => new
                {
                    b.Title,
                    b.BookId
                })
                .OrderBy(b => b.BookId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString();   
        }
        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();
            string[] categories = input.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(c => c.ToLower())
                .ToArray();

            List<string> currCatBookTitles = new List<string>();

            foreach (string category in categories)
            {
                var books = context
                    .Books
                    .Where(b => b.BookCategories.Any(bc => bc.Category.Name.ToLower() == category))
                    .Select(b => b.Title)
                    .ToList();

                currCatBookTitles.AddRange(books);
            }

            currCatBookTitles = currCatBookTitles
                .OrderBy(b => b)
                    .ToList();
            foreach (var title in currCatBookTitles)
            {
                sb.AppendLine(title);
            }
            return sb.ToString().Trim();
        }
        public static string GetBooksReleasedBefore(BookShopContext context, string date)
        {
            StringBuilder sb = new StringBuilder();
            var books = context
                .Books
                .Where(b => b.ReleaseDate < Convert.ToDateTime(date))
                .OrderByDescending(b => b.ReleaseDate)
                .Select(b => new
                {
                    b.Title,
                    b.EditionType,
                    b.Price,
                    b.ReleaseDate
                })
                .ToList();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - {book.Price}$");
            }
            return sb.ToString().Trim();
        }
        public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var authorNames = context
                .Authors
                .Where(a => a.FirstName.EndsWith(input))
                .ToList();

            foreach (var author in authorNames)
            {
                sb.AppendLine($"{author.FirstName} {author.LastName}");
            }
            return sb.ToString();
        }
        public static string GetBookTitlesContaining(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            var bookTitles = context
                .Books
                .Where(b => b.Title.ToLower().Contains(input.ToLower()))
                .Select(b => b.Title)
                .OrderBy(b => b)
                .ToList();

            foreach (var title in bookTitles)
            {
                sb.AppendLine(title);
            }

            return sb.ToString();
        }
        public static string GetBooksByAuthor(BookShopContext context, string input)
        {
            StringBuilder sb = new StringBuilder();

            input = input.ToLower();
            var books = context
                .Books
                .Select(b => new 
                {
                    Title = b.Title,
                    AuthorFirstName = b.Author.FirstName,
                    AuthorLastName = b.Author.LastName,
                    BookingId = b.BookId
                })
                .Where(b => b.AuthorLastName.ToLower().StartsWith(input))
                .OrderBy(b => b.BookingId)
                .ToList();

            foreach (var book in books)
            {
                sb.AppendLine($"{book.Title} ({book.AuthorFirstName} {book.AuthorLastName})");
            }
            return sb.ToString();
        }
        public static int CountBooks(BookShopContext context, int lengthCheck)
        {

            var bookCount = context
                .Books
                .Where(b => b.Title.Length > lengthCheck)
                .ToList().Count();

            
            return bookCount;
        }
        public static string CountCopiesByAuthor(BookShopContext context)
        {
            StringBuilder sb = new StringBuilder();

            var bookCount = context
                .Books
                .Select(b => new
                {
                    AuthorFirstName = b.Author.FirstName,
                    AuthorLastName = b.Author.LastName,
                    Count = b.Copies
                })
                .OrderByDescending(b => b.Count)
                .ToList();
            foreach ( var book in bookCount)
            {
                sb.AppendLine($"{book.AuthorFirstName} {book.AuthorLastName} - {book.Count}");
            }
            return sb.ToString().Trim();
        }
        public static string GetTotalProfitByCategory(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Dictionary<string, double> output = new Dictionary<string, double>();
            var count = context
                .Books
                .Select(c => new
                {
                    Copies = c.Copies,
                    Price = c.Price,
                    Category = c.BookCategories
                })
                .ToList();
             
            foreach (var b in count)
            {
                double result = b.Copies * (double)b.Price;
                output[b.Category.ToString()] = result;
            }


            foreach (var item in output)
            {
                stringBuilder.AppendLine($"{item.Key} ${item.Value:F2}");
            }
            return stringBuilder.ToString().Trim();
        }
        public static string GetMostRecentBooks(BookShopContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();

            var recentBooks = context
                .BooksCategories
                .OrderBy(b => b.Category)
                .Select(b => new
                {
                    CategoryName = b.Category,
                    Title = b.Book.Title,
                    ReleaseDate = b.Book.ReleaseDate
                })
                .GroupBy(b => b.CategoryName)
                .ToList();

            foreach (var item in recentBooks)
            {
                stringBuilder.AppendLine($"--{item}");
            }
            return stringBuilder.ToString().Trim(); 
        }
        public static void IncreasePrices(BookShopContext context)
        {
            var books = context
                .Books
                .Where(b => b.ReleaseDate.Value.Year < 2010)
                .Select(b => new
                {
                    PricePerBook = b.Price
                });
            foreach (var item in books)
            {
                //item.PricePerBook = item.PricePerBook + 5;
            }

        }
    }
}


