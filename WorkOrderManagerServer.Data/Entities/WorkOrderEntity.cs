using System.ComponentModel.DataAnnotations.Schema;

namespace WorkOrderManagerServer.Data.Entities
{
    public class WorkOrderEntity
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public string OrderOpeningDatetime { get; set; }
        public string? OrderClosingDatetime { get; set; }
        public string Client { get; set; }
        public string? Phone { get; set; }
        public string Vehicle { get; set; }
        public string? VehiclePlate { get; set; }
        public string ClientRequest { get; set; }
        public string? Pendencies { get; set; }
        public string? Deadline { get; set; }
        public string? Remarks { get; set; }

        public WorkOrderEntity(int id, int dayId, string status, int priority,
            string orderOpeningDatetime, string? orderClosingDatetime,
            string client, string? phone, string vehicle,
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
            Phone = phone;
            Vehicle = vehicle;
            VehiclePlate = vehiclePlate;
            ClientRequest = clientRequest;
            Pendencies = pendencies;
            Deadline = deadline;
            Remarks = remarks;
        }
    }
}