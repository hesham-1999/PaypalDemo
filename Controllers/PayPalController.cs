using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using PaypalDemo.PaymentService;
using PaypalDemo.PayMentTwo;

namespace PaypalDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly PayPalSetting payPalSetting;

        public PayPalController( PayPalSetting payPalSetting)
        {
            this.payPalSetting = payPalSetting;
        }
        [HttpGet]
        [Route("GetacessToken")]
        public async Task<IActionResult> GetacessToken()
        {
            var paypalclient = new PaypalClient(payPalSetting.ClientId, payPalSetting.ClientSecret, payPalSetting.Mode);
            var res = await paypalclient.Authenticate();
            return Ok(res);
        }
        [HttpPost]
        [Route("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            var paypalclient = new PaypalClient(payPalSetting.ClientId, payPalSetting.ClientSecret, payPalSetting.Mode);
            var order =await  paypalclient.CreateOrder("150", "USD", Guid.NewGuid().ToString());

            return Ok(order);
        }
        [HttpGet]
        [Route("CaptureOrder")]
        public async Task<IActionResult> CaptureOrder(string orderId)
        {
            var paypalclient = new PaypalClient(payPalSetting.ClientId, payPalSetting.ClientSecret, payPalSetting.Mode);
            var response = await paypalclient.CaptureOrder(orderId);

            return Ok(response);
        }


        //public IPaymentService PaymentService { get; }

        //[HttpGet]
        //[Route("CreatePayment")]
        //public async Task<IActionResult> CreatePayment()
        //{
        //    var result = await PaymentService.CreatePayment(); ;
        //    foreach (var item in result.links) 
        //    {
        //        return Redirect(item.href);
        //    }
        //    return NotFound();
        //}
    }
}
