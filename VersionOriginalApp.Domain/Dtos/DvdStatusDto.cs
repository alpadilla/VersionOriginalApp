using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class DvdStatusDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
