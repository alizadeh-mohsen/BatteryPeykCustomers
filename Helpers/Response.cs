namespace BatteryPeykCustomers.Helpers
{
    public class Response
    {

        public int Status { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
        public bool IsSuccess { get; set; } = true;
    }


    public class Data
    {
        public int messageId { get; set; }
        public int DeliveryState { get; set; }
        public int DeliveryDateTime { get; set; }
    }
}
