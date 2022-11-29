using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkOrderManagerServer.Application.Models;
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

        async Task IWorkOrderService.DeleteWorkOrder(int id)
        {
            WorkOrderEntity? wo = await _db.WorkOrders.FindAsync(id);
            if (wo != null)
            {
                _db.WorkOrders.Remove(wo);
                _db.SaveChanges();
            }
        }

        async Task<List<WorkOrder>> IWorkOrderService.GetAllWorkOrders()
        {
            var response = new List<WorkOrder>();
            var workOrders = await _db.WorkOrders.ToListAsync();
            foreach (var wo in workOrders)
            {
                response.Add(WorkOrderEntityToModel(wo));
            }
            return response;
        }

        async Task<List<WorkOrder>> IWorkOrderService.GetWorkOrdersFilteredByStatus(List<string> status)
        {
            var response = new List<WorkOrder>();
            if (status != null && status.Any())
            {
                var workOrders = await _db.WorkOrders.Where(w => status.Contains(w.Status)).ToListAsync();
                foreach (var wo in workOrders)
                {
                    response.Add(WorkOrderEntityToModel(wo));
                }
            }
            return response;
        }

        async Task<WorkOrder?> IWorkOrderService.GetWorkOrder(int id)
        {
            WorkOrder? response = null;
            WorkOrderEntity? entity = await _db.WorkOrders.FindAsync(id);
            if (entity != null)
            {
                response = WorkOrderEntityToModel(entity);
            }
            return response;
        }

        async Task IWorkOrderService.SaveWorkOrder(WorkOrder workOrder)
        {
            if (workOrder.Id == 0)
            {
                int dayId = 1;
                WorkOrderEntity? lastAddedWorkOrder =
                    await _db.WorkOrders.OrderByDescending(wo => wo.Id).FirstOrDefaultAsync();
                if (lastAddedWorkOrder != null)
                {
                    DateTime dateTime = DateTime.ParseExact(lastAddedWorkOrder.OrderOpeningDatetime,
                        "dd/MM/yyyy HH:mm:ss", null);

                    if (dateTime.Date == DateTime.Now.Date)
                        dayId = lastAddedWorkOrder.DayId + 1;
                }
                workOrder.DayId = dayId;

                await _db.WorkOrders.AddAsync(WorkOrderModelToEntity(workOrder));
                await _db.SaveChangesAsync();
            }
            else
            {
                WorkOrderEntity? entity = await _db.WorkOrders.FindAsync(workOrder.Id);

                if(entity != null)
                {
                    entity.DayId = workOrder.DayId;
                    entity.Status = workOrder.Status;
                    entity.Priority = workOrder.Priority;
                    entity.OrderOpeningDatetime = workOrder.OrderOpeningDatetime;
                    entity.OrderClosingDatetime = workOrder.OrderClosingDatetime;
                    entity.Client = workOrder.Client;
                    entity.Phone = workOrder.Phone;
                    entity.ClientRequest = workOrder.ClientRequest;
                    entity.Vehicle = workOrder.Vehicle;
                    entity.VehiclePlate = workOrder.VehiclePlate;
                    entity.ClientRequest = workOrder.ClientRequest;
                    entity.Pendencies = workOrder.Pendencies;
                    entity.Deadline = workOrder.Deadline;
                    entity.Remarks = workOrder.Remarks;

                    await _db.SaveChangesAsync();
                }
            }
        }

        private static WorkOrder WorkOrderEntityToModel(WorkOrderEntity w)
        {
            return new WorkOrder(w.Id, w.DayId, w.Status, w.Priority, w.OrderOpeningDatetime,
                w.OrderClosingDatetime, w.Client, w.Phone, w.Vehicle, w.VehiclePlate, w.ClientRequest,
                w.Pendencies, w.Deadline, w.Remarks);
        }

        private static WorkOrderEntity WorkOrderModelToEntity(WorkOrder w)
        {
            return new WorkOrderEntity(w.Id, w.DayId, w.Status, w.Priority, w.OrderOpeningDatetime,
                w.OrderClosingDatetime, w.Client, w.Phone, w.Vehicle, w.VehiclePlate, w.ClientRequest,
                w.Pendencies, w.Deadline, w.Remarks);
        }
    }
}
