using System;

namespace VersionOriginalApp.Domain.Dtos
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
