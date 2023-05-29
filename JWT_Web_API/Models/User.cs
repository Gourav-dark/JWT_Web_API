using System.ComponentModel.DataAnnotations;

namespace JWT_Web_API.Models
{
    public class User
    {
        public User()
        {
            Items = new HashSet<Item>();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public String Role { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
