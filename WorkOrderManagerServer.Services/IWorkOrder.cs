using WorkOrderManagerServer.Data;

namespace WorkOrderManagerServer.Services
{
    public interface IWorkOrder
    {
        WorkOrder GetWorkOrder(int? id);
        IQueryable<WorkOrder> GetAllWorkOrders();
        void SaveWorkOrder(WorkOrder workOrder);
        void DeleteWorkOrder(int? id);
    }
}