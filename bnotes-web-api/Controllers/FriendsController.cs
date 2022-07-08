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

        // GET: Friends
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
                BirthDate = f.BirthDate.ToString("yyyy-MM-dd")
            };
        }

        [Route("/things")] //rename
        [HttpGet]
        public FriendFavouritesResp GetDetails(int? id)
        {
            if (id == null || _context.Friends == null)
            {
                return null;
            }
            var friend = _context.Friends.FirstOrDefault(m => m.FriendId == id); // could be async...
            if (friend == null)
            {
                return null;
            }
            return new FriendFavouritesResp
            {
                FirstName = friend.FirstName,
                FavouriteThings = _context.Favourites.Where(f => f.FriendId == id).Select(t => t.Title)
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
            return mapToResp(savedEntity.Entity); //todo Fix
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

    // GET: Friends/Details/5
    // public async Task<IActionResult> Details(int? id)
    // {
    //     if (id == null || _context.Friend == null)
    //     {
    //         return NotFound();
    //     }

    //     var friend = await _context.Friend
    //         .FirstOrDefaultAsync(m => m.FriendId == id);
    //     if (friend == null)
    //     {
    //         return NotFound();
    //     }

    //     return View(friend);
    // }


    // // GET: Friends/Edit/5
    // public async Task<IActionResult> Edit(int? id)
    // {
    //     if (id == null || _context.Friend == null)
    //     {
    //         return NotFound();
    //     }

    //     var friend = await _context.Friend.FindAsync(id);
    //     if (friend == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(friend);
    // }

    // // POST: Friends/Edit/5
    // // To protect from overposting attacks, enable the specific properties you want to bind to.
    // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    // [HttpPost]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Edit(int id, [Bind("FriendId,FirstName,LastName,BirthDate")] Friend friend)
    // {
    //     if (id != friend.FriendId)
    //     {
    //         return NotFound();
    //     }

    //     if (ModelState.IsValid)
    //     {
    //         try
    //         {
    //             _context.Update(friend);
    //             await _context.SaveChangesAsync();
    //         }
    //         catch (DbUpdateConcurrencyException)
    //         {
    //             if (!FriendExists(friend.FriendId))
    //             {
    //                 return NotFound();
    //             }
    //             else
    //             {
    //                 throw;
    //             }
    //         }
    //         return RedirectToAction(nameof(Index));
    //     }
    //     return View(friend);
    // }

    // // GET: Friends/Delete/5
    // public async Task<IActionResult> Delete(int? id)
    // {
    //     if (id == null || _context.Friend == null)
    //     {
    //         return NotFound();
    //     }

    //     var friend = await _context.Friend
    //         .FirstOrDefaultAsync(m => m.FriendId == id);
    //     if (friend == null)
    //     {
    //         return NotFound();
    //     }

    //     return View(friend);
    // }

    // // POST: Friends/Delete/5
    // [HttpPost, ActionName("Delete")]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> DeleteConfirmed(int id)
    // {
    //     if (_context.Friend == null)
    //     {
    //         return Problem("Entity set 'DataContext.Friend'  is null.");
    //     }
    //     var friend = await _context.Friend.FindAsync(id);
    //     if (friend != null)
    //     {
    //         _context.Friend.Remove(friend);
    //     }

    //     await _context.SaveChangesAsync();
    //     return RedirectToAction(nameof(Index));
    // }

    // private bool FriendExists(int id)
    // {
    //   return (_context.Friend?.Any(e => e.FriendId == id)).GetValueOrDefault();
    // }
}
