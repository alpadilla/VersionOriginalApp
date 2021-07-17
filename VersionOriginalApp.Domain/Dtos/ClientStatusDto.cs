using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class ClientStatusDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
