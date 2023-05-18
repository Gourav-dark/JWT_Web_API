using System.ComponentModel.DataAnnotations.Schema;

namespace JWT_Web_API.Models.ViewModel
{
    public class ViewItem
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Guid userId { get; set; }
    }
}
