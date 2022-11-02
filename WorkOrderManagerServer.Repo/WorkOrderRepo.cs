using System;
using System.Collections.Generic;
using System.Globalization;
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
                int dayId = 1;
                WorkOrder? lastAddedWorkOrder = _db.WorkOrders.LastOrDefault();
                if (lastAddedWorkOrder != null)
                {
                    DateTime dateTime = DateTime.ParseExact(lastAddedWorkOrder.OrderOpeningDatetime,
                        "dd/MM/yyyy HH:mm:ss", null);

                    if (dateTime.Date == DateTime.Now.Date)
                        dayId = lastAddedWorkOrder.DayId + 1;
                }
                workOrder.DayId = dayId;

                _db.WorkOrders.Add(workOrder);
                _db.SaveChanges();
            }
            else
            {
                WorkOrder entity = _db.WorkOrders.Find(workOrder.Id);

                entity.DayId = workOrder.DayId;
                entity.Status = workOrder.Status;
                entity.Priority = workOrder.Priority;
                entity.OrderOpeningDatetime = workOrder.OrderOpeningDatetime;
                entity.OrderClosingDatetime = workOrder.OrderClosingDatetime;
                entity.Client = workOrder.Client;
                entity.Telephone = workOrder.Telephone;
                entity.ClientRequest = workOrder.ClientRequest;
                entity.Vehicle = workOrder.Vehicle;
                entity.VehiclePlate = workOrder.VehiclePlate;
                entity.ClientRequest = workOrder.ClientRequest;
                entity.Pendencies = workOrder.Pendencies;
                entity.Deadline = workOrder.Deadline;
                entity.Remarks = workOrder.Remarks;

                _db.SaveChanges();
            }
        }
    }
}
