using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace bnotes_web_api.Models
{
    public class Favourite
    {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int FavouriteId { get; set; }
        [Required, StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }
        [ForeignKey( "FriendId" )]
        public int FriendId { get; set; }
        public Friend Friend { get; set; }
    }
}