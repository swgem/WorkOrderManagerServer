using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkOrderManagerServer.Application.DTOs.Models;
using WorkOrderManagerServer.Application.Services;

namespace WorkOrderManagerServer.Controllers
{
    [Authorize(Roles = "Admin, Manager, Collaborator")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOrderController : Controller
    {
        private readonly IWorkOrderService _workOrderService;

        public WorkOrderController(IWorkOrderService workOrderService)
        {
            _workOrderService = workOrderService;
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WorkOrder data)
        {
            if (data == null || data.Id == 0)
            {
                return BadRequest();
            }

            await _workOrderService.SaveWorkOrder(data);

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Store([FromBody] WorkOrder data)
        {
            if(data == null || data.Id != 0)
            {
                return BadRequest();
            }

            await _workOrderService.SaveWorkOrder(data);

            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkOrder([FromQuery(Name = "status")] List<string> status)
        {
            List<WorkOrder> data;

            if ((status?.Count ?? 0) == 0)
            {
                data = await _workOrderService.GetAllWorkOrders();
            }
            else
            {
                data = await _workOrderService.GetWorkOrdersFilteredByStatus(status);
            }

            return Ok(data);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetWorkOrder(int id)
        {
            WorkOrder? data = await _workOrderService.GetWorkOrder(id);

            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _workOrderService.DeleteWorkOrder(id);
            return Ok();
        }
    }
}
