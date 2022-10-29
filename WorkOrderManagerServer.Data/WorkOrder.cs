using System.ComponentModel.DataAnnotations.Schema;

namespace WorkOrderManagerServer.Data
{
    public class WorkOrder
    {
        public int Id { get; set; }
        public int DayId { get; set; }
        public int Status { get; set; }
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
    }
}