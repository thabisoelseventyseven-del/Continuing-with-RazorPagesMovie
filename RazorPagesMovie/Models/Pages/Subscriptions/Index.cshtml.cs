using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Subscriptions
{
    public class SubscribeModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public SubscribeModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? Plan { get; set; }

        public string PlanDisplayName { get; set; } = string.Empty;
        public decimal PlanPrice { get; set; }
        public string PlanDescription { get; set; } = string.Empty;

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(Plan))
            {
                return Page();
            }

            // Set plan details
            switch (Plan)
            {
                case "withAds":
                    PlanDisplayName = "With Ads";
                    PlanPrice = 39.99M;
                    PlanDescription = "Cheaper option but with ads during movies.";
                    break;

                case "noAds":
                    PlanDisplayName = "No Ads";
                    PlanPrice = 79.99M;
                    PlanDescription = "Watch movies without ads. Enjoy uninterrupted experience.";
                    break;

                default:
                    return RedirectToPage("./Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Plan))
            {
                return Page();
            }

            var subscription = new Subscription();

            switch (Plan)
            {
                case "withAds":
                    subscription.PlanName = "With Ads";
                    subscription.Price = 39.99M;
                    subscription.Description = "Cheaper option but with ads during movies.";
                    subscription.HasAds = true;
                    break;

                case "noAds":
                    subscription.PlanName = "No Ads";
                    subscription.Price = 79.99M;
                    subscription.Description = "Watch movies without ads. Enjoy uninterrupted experience.";
                    subscription.HasAds = false;
                    break;

                default:
                    return RedirectToPage("./Index");
            }

            _context.Subscription.Add(subscription);
            _context.SaveChanges();

            TempData["SuccessMessage"] = $"You have successfully subscribed to the {subscription.PlanName} plan!";
            return RedirectToPage("./Index");
        }
    }
}
