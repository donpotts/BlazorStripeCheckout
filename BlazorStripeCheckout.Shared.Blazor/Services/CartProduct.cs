using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorStripeCheckout.Shared.Blazor.Services
{
    public class CartProduct
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; } // Added Description property
        public decimal? Price { get; set; }
        public int Quantity { get; set; }
    }
}