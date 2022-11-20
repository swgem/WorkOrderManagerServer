using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkOrderManagerServer.Application.DTOs.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public string OrderOpeningDatetime { get; set; }
        public string? OrderClosingDatetime { get; set; }
        public string Client { get; set; }
        public string? Telephone { get; set; }
        public string Vehicle { get; set; }
        public string? VehiclePlate { get; set; }
        public string ClientRequest { get; set; }
        public string? Pendencies { get; set; }
        public string? Deadline { get; set; }
        public string? Remarks { get; set; }

        public WorkOrder(int id, int dayId, string status, int priority,
            string orderOpeningDatetime, string? orderClosingDatetime,
            string client, string? telephone, string vehicle,
            string? vehiclePlate, string clientRequest, string? pendencies,
            string? deadline, string? remarks)
        {
            Id = id;
            DayId = dayId;
            Status = status;
            Priority = priority;
            OrderOpeningDatetime = orderOpeningDatetime;
            OrderClosingDatetime = orderClosingDatetime;
            Client = client;
            Telephone = telephone;
            Vehicle = vehicle;
            VehiclePlate = vehiclePlate;
            ClientRequest = clientRequest;
            Pendencies = pendencies;
            Deadline = deadline;
            Remarks = remarks;
        }
    }
}
