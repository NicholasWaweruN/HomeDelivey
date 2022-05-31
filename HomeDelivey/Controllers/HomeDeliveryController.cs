using DataAccessLayer.ViewModels;
using DataAccessLayer.ViewModels.HD_ViewModels;
using HomeDelivey.HomeDelivery_BussinessLgic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace HomeDelivey.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeDeliveryController : ControllerBase
    {
        private readonly IHDBussinessLogic _hDBussinessLogic;
        public HomeDeliveryController(IHDBussinessLogic hDBussinessLogic )
        {
            _hDBussinessLogic = hDBussinessLogic;
        }
        [HttpGet]
        [Route("get-catalog")]
        [DisplayName("get-catalog")]
        public async Task<IActionResult> GetCatalog()
        {
            try
            {
                var catalog = await _hDBussinessLogic.GetCatalog();
                if (catalog == null)
                {
                    return NotFound();
                }
                return StatusCode(StatusCodes.Status200OK, catalog);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet]
        [Route("get-previousorders")]
        public async Task<IActionResult> PreviousOrders(string phonenumber)
        {
            try
            {
                var result = await _hDBussinessLogic.PreviousOrders(phonenumber);
                if (result == null)
                {
                    return NotFound();
                }
                return StatusCode(StatusCodes.Status200OK,result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        [Route("post-order")]
        public async Task<IActionResult> PostOrder(CustomerOrderViewModel customerOrder)
        {
            try
            {
                var order = await _hDBussinessLogic.PostOrder(customerOrder);
                return StatusCode(StatusCodes.Status200OK, new ResponseMessage { Message = "Order Posted SuccessFully", Status = "Success" });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseMessage { Message = e.Message, Status = "Failed" });
            }
        }

    }
}
