using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace bnotes_web_api.Models
{
    public class FriendFavouritesResp
    {
        public string FirstName { get; set; }
        // deprecated
        public IEnumerable<String> FavouriteThings { get; set; }
    }
}