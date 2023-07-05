namespace Boardgames.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlTypes;
    using System.Text;
    using System.Xml.Serialization;
    using Boardgames.Data;
    using Boardgames.Data.Models;
    using Boardgames.Data.Models.Enums;
    using Boardgames.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using Boardgame = Data.Models.Boardgame;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCreator
            = "Successfully imported creator – {0} {1} with {2} boardgames.";

        private const string SuccessfullyImportedSeller
            = "Successfully imported seller - {0} with {1} boardgames.";

        public static string ImportCreators(BoardgamesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ImportProjectTaskDto[]), new XmlRootAttribute("Creators"));
            using (StringReader stringReader = new StringReader(xmlString))
            {
                ImportProjectTaskDto[] creatorDtos = (ImportProjectTaskDto[])xmlSerializer
                    .Deserialize(stringReader);
                List<Creator> creatorList = new List<Creator>();
                foreach (var creatorDto in creatorDtos)
                {
                    if (!IsValid(creatorDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (!IsValid(creatorDto.Boardgames))
                    {
                        sb.AppendLine(ErrorMessage);
                        Creator creator = new Creator()
                        {
                            FirstName = creatorDto.FirstName,
                            LastName = creatorDto.LastName,
                        };
                        creatorList.Add(creator);
                        continue;
                    }
                    Creator fullCreator = new Creator()
                    {
                        FirstName = creatorDto.FirstName,
                        LastName = creatorDto.LastName,
                        Boardgames = creatorDto.Boardgames
                        .Select(x => new Boardgame
                        {
                            Name = x.Name,
                            Rating = x.Rating,
                            YearPublished = x.YearPublished,
                            CategoryType = x.CategoryType,
                            Mechanics = x.Mechanics
                        })
                        .ToArray()
                    };
                       
                    creatorList.Add(fullCreator);
                    sb.AppendLine(String.Format(SuccessfullyImportedCreator, creatorDto.FirstName, creatorDto.LastName, creatorDto.Boardgames.Count()));
                    };
                context.Creators.AddRange(creatorList);
                context.SaveChanges();
                return sb.ToString();
                }

            }

        public static string ImportSellers(BoardgamesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();

            ImportSellersDto[] sellerDto =
                JsonConvert.DeserializeObject<ImportSellersDto[]>(jsonString);

            List<Seller> sellers = new List<Seller>();  
            foreach (var seller in sellerDto)
            {
                if (!IsValid(seller))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
            }
            return sb.ToString().Trim();
            

        }
        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
