using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class ClientTypeDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
