using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesWebAppTest.Pages
{
    public class ProductsModel : PageModel
    {
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; }

        private const int PageSize = 10; // Products per page

        public void OnGet(string searchTerm, int pageIndex = 1)
        {
            SearchTerm = searchTerm;
            PageIndex = pageIndex;

            // Mock data (replace with database or service call)
            var allProducts = new List<Product>
        {
            new Product { Name = "Product 1", Price = 10.00m, Description = "Description 1" },
            new Product { Name = "Product 2", Price = 20.00m, Description = "Description 2" },
            // Add more products as needed
        };

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                allProducts = allProducts
                    .Where(p => p.Name.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            TotalPages = (int)System.Math.Ceiling(allProducts.Count / (double)PageSize);

            Products = allProducts
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
