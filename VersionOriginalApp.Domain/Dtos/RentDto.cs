using System;
using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class RentDto : BaseDto
    {
        [Required]
        public Guid ClientId { get; set; }
        public ClientDto Client { get; set; }
        [Required]
        public Guid DvdId { get; set; }
        public DvdDto Dvd { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
