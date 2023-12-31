namespace BatteryPeykCustomers.Helpers
{
    public class Response
    {

        public byte Status { get; set; }
        public string Message { get; set; }
        //public decimal Cost { get; set; }
        //public Data Data { get; set; }
        //public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
    }


    public class Data
    {
        //public string PackId { get; set; }
        //public int[] MessageIds { get; set; }
    }
}
