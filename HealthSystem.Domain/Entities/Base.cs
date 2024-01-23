namespace HealthSystem.Domain.Entities
{
    public class Base
    {
        public Guid Id {get; set;} = Guid.NewGuid(); 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}