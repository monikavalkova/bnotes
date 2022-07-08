using Microsoft.AspNetCore.Mvc;
using bnotes_web_api.Models;
using System.Globalization;

namespace bnotes_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendsController : Controller
    {
        private readonly DataContext _context;

        public FriendsController(DataContext context)
        {
            _context = context;
        }

        [HttpGet(Name = "GetAllFriends")]
        public List<FriendResp> GetFriends()
        {
            return _context.Friends.Select(f => mapToResp(f)).ToList();
        }

        private static FriendResp mapToResp(Friend f)
        {
            return new FriendResp
            {
                FirstName = f.FirstName,
                FriendId = f.FriendId,
                BirthDate = f.BirthDate.ToString("yyyy-MM-dd"),
                DayOfYear = f.BirthDate.DayOfYear
            };
        }

        [HttpPost]
        public async Task<FriendResp> Create([Bind("FirstName,BirthDate")] FriendReq fr)
        {
            if (fr == null)
            {
                throw new BadHttpRequestException("Missing expected request body.");
            }

            var savedEntity = _context.Add(mapToEntity(fr));
            await _context.SaveChangesAsync();
            
            return mapToResp(savedEntity.Entity);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Friends == null)
            {
                return NotFound();
            }

            var friend = _context.Friends.FirstOrDefault(m => m.FriendId == id);
            _context.Friends.Attach(friend);
            _context.Friends.Remove(friend);
            _context.SaveChanges();
            
            if (friend == null)
            {
                return NotFound();
            }

            return Ok();
        }

        private Friend mapToEntity(FriendReq fr)
        {
            DateTime parsedDate;
            bool isParsable = DateTime.TryParseExact(fr.BirthDate, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None, out parsedDate);
            if (!isParsable)
            {
                throw new Exception("Date could not be parsed.");
            }
            return new Friend
            {
                FirstName = fr.FirstName,
                BirthDate = parsedDate
            };
        }

    }
}
