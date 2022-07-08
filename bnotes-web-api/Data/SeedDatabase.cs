using bnotes_web_api.Models;
using Microsoft.EntityFrameworkCore;

public class SeedDatabase
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        // creating a new DBContext and passing options (:
        using (var context = new DataContext(

            serviceProvider.GetRequiredService<
            DbContextOptions<DataContext>>()))
        {
            context.Database.Migrate();
            populateFriends(context);
            // commiting the changes
            context.SaveChanges();
        }
    }

    private static void populateFriends(DataContext context)
    {
        // SET IDENTITY_INSERT Friends OFF;  (must do beforehand)
        if (context.Friends.Any()) { return; }
        context.Friends.AddRange(
            new Friend
            {
                FirstName = "Andrea",
                BirthDate = new DateTime(2000, 6, 2),
            },
            new Friend
            {
                FirstName = "Monika",
                BirthDate = new DateTime(2000, 1, 23),
            },

            new Friend
            {
                FirstName = "Stefan",
                BirthDate = new DateTime(2000, 12, 31),
            },
                new Friend
                {
                    FirstName = "Gabriel",
                    BirthDate = new DateTime(2000, 9, 13),
                },
                new Friend
                {
                    FirstName = "Kristina",
                    BirthDate = new DateTime(2000, 12, 12),
                }
        );
    }

}