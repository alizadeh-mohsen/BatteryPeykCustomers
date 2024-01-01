namespace BatteryPeykCustomers.Helpers
{
    public class Request
    {

        public string Mobile { get; set; }
        public int TemplateId { get; set; }
        public Parameter[] Parameters { get; set; }
    }

    public class Parameter
    {
        public string Name { get; set; }
        public string Value { get; set; }

    }
}
