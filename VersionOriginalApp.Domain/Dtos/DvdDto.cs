using System;
using System.ComponentModel.DataAnnotations;

namespace VersionOriginalApp.Domain.Dtos
{
    public class DvdDto : BaseDto
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public long Qty { get; set; }
        [Required]
        public double RentPrice { get; set; }
        [Required]
        public double CostPrice { get; set; }
        [Required]
        public Guid StatusId { get; set; }
        public DvdStatusDto Status { get; set; }
        [Required]
        public DateTime BoughtAt { get; set; }
    }
}
