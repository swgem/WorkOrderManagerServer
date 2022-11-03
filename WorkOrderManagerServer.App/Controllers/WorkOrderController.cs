using Microsoft.AspNetCore.Mvc;
using WorkOrderManagerServer.Data;
using WorkOrderManagerServer.Services;

namespace WorkOrderManagerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : Controller
    {
        private readonly IWorkOrder _db;

        public WorkOrderController(IWorkOrder db)
        {
            _db = db;
        }

        [HttpPut]
        public IActionResult Save([FromBody] WorkOrder data)
        {
            if (data == null || data.Id == 0)
            {
                return BadRequest();
            }

            _db.SaveWorkOrder(data);

            return Ok(data);
        }

        [HttpPost]
        public IActionResult Store([FromBody] WorkOrder data)
        {
            if(data == null || data.Id != 0)
            {
                return BadRequest();
            }

            _db.SaveWorkOrder(data);

            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetWorkOrders()
        {
            IQueryable<WorkOrder> data = _db.GetAllWorkOrders();

            return Ok(data);
        }

        [HttpGet("{Id}")]
        public IActionResult GetWorkOrder(int? id)
        {
            WorkOrder data = _db.GetWorkOrder(id);

            return Ok(data);
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            _db.DeleteWorkOrder(id);
            return Ok();
        }
    }
}
