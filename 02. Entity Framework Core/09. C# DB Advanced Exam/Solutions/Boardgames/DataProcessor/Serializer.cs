namespace Boardgames.DataProcessor
{
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System.Text;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
        {
            StringBuilder stringBuilder = new StringBuilder();
            ExportCreators[] creators = context
                .Creators
                .Select(x => new ExportCreators
                {
                    CreatorName = x.FirstName + " " + x.LastName, 
                    BoardGames = x.Boardgames
                    .Select(x => new BoardGame
                    {
                        BoardgameName = x.Name,
                        BoardgameYearPublished = x.YearPublished,
                    })
                    .OrderBy(x => x.BoardgameName)
                    .ToArray()
                }) 
                .ToArray();

            XmlSerializer xmlSerialiazirer = new XmlSerializer(typeof
                (ExportCreators[]), new XmlRootAttribute("Creators"));
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty); 

            using (StringWriter stringWriter = new StringWriter(stringBuilder))
            {
                xmlSerialiazirer.Serialize(stringWriter, creators, namespaces);
            }
            return stringBuilder.ToString();
        }

        public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
        {
            var sellers = context
                .Sellers
                .Where(x => x.BoardgamesSellers.Any(y => y.Boardgame.YearPublished >= year
                && y.Boardgame.Rating <= rating))
                .ToArray()
                .Select(y => new
                {
                    Name = y.Name,
                    Website = y.Website,
                    Boardgames = y.BoardgamesSellers
                    .Select(b => new
                    {
                        Name = b.Boardgame.Name,
                        Rating = b.Boardgame.Rating,
                        Mechanics = b.Boardgame.Mechanics,
                        Category = b.Boardgame.CategoryType
                    })
                    .ToArray()

                })
                .ToList();
                
            var output = JsonConvert.SerializeObject(sellers, Formatting.Indented);
            return output;
            
        }
    }
}