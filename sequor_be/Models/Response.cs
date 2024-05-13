namespace sequor_be.Models
{
    public class Response
    {
        public Response(string description)
        {
            Type = "E";
            Status = 201;
            Description = description;
        }
        public int Status { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
