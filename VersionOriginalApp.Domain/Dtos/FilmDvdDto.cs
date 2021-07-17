using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class FilmDvdDto : DvdDto
    {
        [Required]
        public int Duration { get; set; }
        [Required]
        public int Year { get; set; }
    }
}
