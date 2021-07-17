using System;
using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class ClientDto : BaseDto
    {
        [Required]
        public string PartnerNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Dni { get; set; }
        [Required]
        public string Phone { get; set; }
        public string SecondaryPhone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Guid ClientStatusId { get; set; }
        public ClientStatusDto ClientStatus { get; set; }
        [Required]
        public Guid ClientTypeId { get; set; }
        public ClientTypeDto ClientType { get; set; }
    }
}
