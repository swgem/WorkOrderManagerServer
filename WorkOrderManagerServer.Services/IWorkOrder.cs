using WorkOrderManagerServer.Data;

namespace WorkOrderManagerServer.Services
{
    public interface IWorkOrder
    {
        WorkOrder GetWorkOrder(int? id);
        IQueryable<WorkOrder> GetAllWorkOrders();
        IQueryable<WorkOrder> GetWorkOrdersFilteredByStatus(List<string> status);
        void SaveWorkOrder(WorkOrder workOrder);
        void DeleteWorkOrder(int? id);
    }
}