using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace bnotes_web_api.Models
{
    public class Note
    {
        [DatabaseGenerated( DatabaseGeneratedOption.Identity )]
        public int NoteId { get; set; }
        [Required]
        public string Text { get; set; }
        [ForeignKey( "FriendId" )]
        public int FriendId { get; set; }
        public Friend Friend { get; set; }


    }
}