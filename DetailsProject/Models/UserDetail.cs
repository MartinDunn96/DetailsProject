using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DetailsProject.Models
{
    public class UserDetail
    {
        public Guid id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Hometown { get; set; }
        public DateTime Birthdate { get; set; }
        public string FavoriteColour { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]

        public IdentityUser User { get; set; }
    }
}
