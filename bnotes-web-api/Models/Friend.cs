using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace bnotes_web_api.Models
{
    public class Friend
    {
        public int FriendId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        [Required, Display(Name = "Birth Date"), DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string? ThingsTheyLike { get; set; }
    }
}