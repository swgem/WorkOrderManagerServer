using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrderManagerServer.Application.DTOs.Models;
using WorkOrderManagerServer.Application.Services;
using WorkOrderManagerServer.Data.Entities;
using WorkOrderManagerServer.Data.Repo;

namespace WorkOrderManagerServer.Data.Services
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly WorkOrderDbContext _db;

        public WorkOrderService(WorkOrderDbContext db)
        {
            _db = db;
        }

        void IWorkOrderService.DeleteWorkOrder(int id)
        {
            WorkOrderEntity? wo = _db.WorkOrders.Find(id);
            if (wo != null)
            {
                _db.WorkOrders.Remove(wo);
                _db.SaveChanges();
            }
        }

        List<WorkOrder> IWorkOrderService.GetAllWorkOrders()
        {
            var response = new List<WorkOrder>();
            foreach (var wo in _db.WorkOrders)
            {
                response.Add(WorkOrderEntityToModel(wo));
            }
            return response;
        }

        List<WorkOrder> IWorkOrderService.GetWorkOrdersFilteredByStatus(List<string> status)
        {
            var response = new List<WorkOrder>();
            if (status != null && status.Any())
            {
                foreach(var wo in _db.WorkOrders.Where(w => status.Contains(w.Status)))
                {
                    response.Add(WorkOrderEntityToModel(wo));
                }
            }
            return response;
        }

        WorkOrder? IWorkOrderService.GetWorkOrder(int id)
        {
            WorkOrder? response = null;
            WorkOrderEntity? entity = _db.WorkOrders.Find(id);
            if (entity != null)
            {
                response = WorkOrderEntityToModel(entity);
            }
            return response;
        }

        void IWorkOrderService.SaveWorkOrder(WorkOrder workOrder)
        {
            if (workOrder.Id == 0)
            {
                int dayId = 1;
                WorkOrderEntity? lastAddedWorkOrder = _db.WorkOrders.LastOrDefault();
                if (lastAddedWorkOrder != null)
                {
                    DateTime dateTime = DateTime.ParseExact(lastAddedWorkOrder.OrderOpeningDatetime,
                        "dd/MM/yyyy HH:mm:ss", null);

                    if (dateTime.Date == DateTime.Now.Date)
                        dayId = lastAddedWorkOrder.DayId + 1;
                }
                workOrder.DayId = dayId;

                _db.WorkOrders.Add(WorkOrderModelToEntity(workOrder));
                _db.SaveChanges();
            }
            else
            {
                WorkOrderEntity? entity = _db.WorkOrders.Find(workOrder.Id);

                if(entity != null)
                {
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

        private static WorkOrder WorkOrderEntityToModel(WorkOrderEntity w)
        {
            return new WorkOrder(w.Id, w.DayId, w.Status, w.Priority, w.OrderOpeningDatetime,
                w.OrderClosingDatetime, w.Client, w.Telephone, w.Vehicle, w.VehiclePlate, w.ClientRequest,
                w.Pendencies, w.Deadline, w.Remarks);
        }

        private static WorkOrderEntity WorkOrderModelToEntity(WorkOrder w)
        {
            return new WorkOrderEntity(w.Id, w.DayId, w.Status, w.Priority, w.OrderOpeningDatetime,
                w.OrderClosingDatetime, w.Client, w.Telephone, w.Vehicle, w.VehiclePlate, w.ClientRequest,
                w.Pendencies, w.Deadline, w.Remarks);
        }
    }
}
