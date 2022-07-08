using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace bnotes_web_api.Models
{
    public class FriendReq
    {
        public int FriendId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string BirthDate { get; set; }
        public string? ThingsTheyLike { get; set; }
    }
}