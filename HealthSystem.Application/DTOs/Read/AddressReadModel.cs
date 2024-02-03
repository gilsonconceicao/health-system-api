using HealthSystem.Application.DTOs.Read;

namespace HealthSystem.Domain.Entities
{
#nullable disable
    public class AddressReadModel
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Number { get; set; }
        public virtual PatientReadModel Patient { get; set; }
        public Guid PatientId { get; set; }
    }
}