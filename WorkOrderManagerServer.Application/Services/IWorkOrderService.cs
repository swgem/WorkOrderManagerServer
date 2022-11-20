using WorkOrderManagerServer.Application.DTOs.Models;

namespace WorkOrderManagerServer.Application.Services
{
    public interface IWorkOrderService
    {
        Task<WorkOrder?> GetWorkOrder(int id);
        Task<List<WorkOrder>> GetAllWorkOrders();
        Task<List<WorkOrder>> GetWorkOrdersFilteredByStatus(List<string> status);
        Task SaveWorkOrder(WorkOrder workOrder);
        Task DeleteWorkOrder(int id);
    }
}