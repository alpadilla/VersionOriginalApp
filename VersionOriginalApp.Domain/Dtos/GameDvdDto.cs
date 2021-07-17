using System;
using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class GameDvdDto : DvdDto
    {
        [Required]
        public Guid GamePlatformId { get; set; }
        public GamePlatformDto GamePlatform { get; set; }
    }
}
