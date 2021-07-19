using System;

namespace VersionOriginalApp.Domain.Dtos
{
    public class ReturnDto
    {
        public bool IsReturned { get; set; }
        public bool Exist { get; set; }
        public bool Returned { get; set; }
        public DateTime ReturnedDate { get; set; }
        public double OutOfDateAmount { get; set; }
    }
}
