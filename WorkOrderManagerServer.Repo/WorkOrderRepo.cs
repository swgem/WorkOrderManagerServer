using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrderManagerServer.Data;
using WorkOrderManagerServer.Services;

namespace WorkOrderManagerServer.Repo
{
    public class WorkOrderRepo : IWorkOrder
    {
        private readonly WorkOrderDbContext _db;

        public WorkOrderRepo(WorkOrderDbContext db)
        {
            _db = db;
        }

        public void DeleteWorkOrder(int? id)
        {
            WorkOrder wo = _db.WorkOrders.Find(id);
            _db.WorkOrders.Remove(wo);
            _db.SaveChanges();
        }

        IQueryable<WorkOrder> IWorkOrder.GetAllWorkOrders() => _db.WorkOrders;

        public WorkOrder GetWorkOrder(int? id)
        {
            WorkOrder wo = _db.WorkOrders.Find(id);
            return wo;
        }

        public void SaveWorkOrder(WorkOrder workOrder)
        {
            if (workOrder.Id == 0)
            {
                _db.WorkOrders.Add(workOrder);
                _db.SaveChanges();
            }
            else
            {
                WorkOrder _Entity = _db.WorkOrders.Find(workOrder.Id);
                _Entity.Client = workOrder.Client;
                _Entity.ServiceSummary = workOrder.ServiceSummary;
                _db.SaveChanges();
            }
        }
    }
}
