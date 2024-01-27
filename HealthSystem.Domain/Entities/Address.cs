namespace HealthSystem.Domain.Entities
{
#nullable disable
    public class Address : Base
    {
        public string Street { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public virtual Patient Patient { get; set; }
        public Guid PatientId { get; set; }
    }
}