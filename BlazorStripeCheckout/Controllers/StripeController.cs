using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using BlazorStripeCheckout.Models;

namespace BlazorStripeCheckout.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StripeController : ControllerBase
    {
        private readonly StripeSettings _stripeSettings;

        public StripeController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        [HttpPost("create-payment-intent")]
        public IActionResult CreatePaymentIntent([FromBody] PaymentIntentRequest request)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var options = new PaymentIntentCreateOptions
            {
                Amount = request.Amount,
                Currency = request.Currency,
                PaymentMethodTypes = new List<string> { "card" },
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }

        [HttpPost("webhook")]
        [AllowAnonymous]
        public IActionResult Webhook()
        {
            var json = new StreamReader(HttpContext.Request.Body).ReadToEnd();
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                _stripeSettings.SecretKey
            );

            if (stripeEvent.Type == "payment_intent.succeeded")
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                // Handle successful payment intent
            }

            return Ok();
        }

        [HttpPost("create-checkout-session")]
        public IActionResult CreateCheckoutSession([FromBody] PaymentIntentRequest request)
        {
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var domain = $"{Request.Scheme}://{Request.Host}";
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = request.Currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Your Product Name",
                            },
                            UnitAmount = request.Amount,
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = $"{domain}/success",
                CancelUrl = $"{domain}/cancel",
            };

            var service = new SessionService();
            var session = service.Create(options);

            return Ok(new { sessionId = session.Id });
        }

        [HttpPost("checkout")]
        [AllowAnonymous]
        public async Task<IActionResult> Checkout([FromBody] List<CartItem> cart)
        {
            var jsonoptions = new System.Text.Json.JsonSerializerOptions
            {
                WriteIndented = true // Enables pretty-printing with indentation
            };

            Console.WriteLine("START CART LOG");
            Console.WriteLine("================================================");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(cart, jsonoptions));
            Console.WriteLine("END CART LOG");
            Console.WriteLine("================================================");

            try
            {
                var taxRate = 0.0875m; // 8.75% tax
                var totalTax = cart.Sum(item => item.Price * item.Quantity) * taxRate;

                var domain = $"{Request.Scheme}://{Request.Host}";
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = cart.Select(item => new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100), // Convert to cents
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Name,
                                Description = item.Description,
                            },
                        },
                        Quantity = item.Quantity,
                    }).ToList(),
                    Mode = "payment",
                    SuccessUrl = $"{domain}/success?session_id={{CHECKOUT_SESSION_ID}}",
                    CancelUrl = $"{domain}/cancel",
                };

                // Add tax as a separate line item
                options.LineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(totalTax * 100), // Convert to cents
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Tax",
                            Description = "Sales Tax (8.75%)",
                        },
                    },
                    Quantity = 1,
                });

                var service = new SessionService();
                var session = await service.CreateAsync(options);

                // Return session information to the client
                return Ok(new
                {
                    sessionId = session.Id,
                    url = session.Url,
                    successUrl = options.SuccessUrl,
                    cancelUrl = options.CancelUrl
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = $"Payment initialization failed: {ex.Message}" });
            }
        }

    }

    public class PaymentIntentRequest
    {
        public long Amount { get; set; }
        public string Currency { get; set; } = "usd";
    }

    public class CartItem
    {
        public string Name { get; set; }
        public string Description { get; set; } // Added Description property
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
