using System.ComponentModel.DataAnnotations;

namespace HydraTestProject.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}