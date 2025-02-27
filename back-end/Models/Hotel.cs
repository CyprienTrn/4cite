using System;
using System.Collections.Generic;

namespace back_end.Models
{
    public class Hotel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }
        public string? Description { get; set; }
        public List<string> PictureList { get; set; } = new List<string>();

        public bool ValidateRequiredField()
        {
            return string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Location);
        }
    }
}