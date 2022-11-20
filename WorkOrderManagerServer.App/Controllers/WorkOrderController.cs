using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkOrderManagerServer.Data;
using WorkOrderManagerServer.Services;

namespace WorkOrderManagerServer.Controllers
{
    [Authorize]
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
        public IActionResult Update([FromBody] WorkOrder data)
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
        public IActionResult GetWorkOrder([FromQuery(Name = "status")] List<string> status)
        {
            IQueryable<WorkOrder> data;

            if ((status?.Count ?? 0) == 0)
            {
                data = _db.GetAllWorkOrders();
            }
            else
            {
                data = _db.GetWorkOrdersFilteredByStatus(status);
            }

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
