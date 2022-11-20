using WorkOrderManagerServer.Application.DTOs.Models;

namespace WorkOrderManagerServer.Application.Services
{
    public interface IWorkOrderService
    {
        WorkOrder? GetWorkOrder(int id);
        List<WorkOrder> GetAllWorkOrders();
        List<WorkOrder> GetWorkOrdersFilteredByStatus(List<string> status);
        void SaveWorkOrder(WorkOrder workOrder);
        void DeleteWorkOrder(int id);
    }
}