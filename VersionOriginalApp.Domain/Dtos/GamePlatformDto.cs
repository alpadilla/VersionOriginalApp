using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class GamePlatformDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
