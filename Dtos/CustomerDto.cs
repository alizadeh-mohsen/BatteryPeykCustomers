namespace BatteryPeykCustomers.Dtos
{
    public class CustomerDto
    {
        public string? Phone { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public List<CarDto> CarDtos { get; set; }
    }
}
