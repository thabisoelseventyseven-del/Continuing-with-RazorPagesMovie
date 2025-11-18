using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Subscription
    {
        public int Id { get; set; }

        [Required]
        public string PlanName { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        // e.g. “With Ads” or “No Ads”
        public bool HasAds { get; set; }
    }
}
