using Microsoft.AspNetCore.Mvc;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilverGym.Web.Controllers
{
    public class PaymentController : BaseController
    {
        public IActionResult Checkout()
        {
            return this.View();
        }

        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions() 
            {
                Email = stripeEmail,
                SourceToken = stripeToken,
            });

            var charge = charges.Create(new ChargeCreateOptions()
            {
                Amount = 500, // 500 = 5$
                Description = "Test description",
                Currency = "BGN",
                CustomerId = customer.Id,
            });

            if (charge.Status == "succeeded")
            {
                string balanceTransactionId = charge.BalanceTransactionId; // successful payment
                return this.View();
            }
            else
            {
                // unsuccesful payment
            }
            return this.View();
        }
    }
}
