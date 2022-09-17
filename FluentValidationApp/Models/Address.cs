namespace FluentValidationApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Province { get; set; }
        public string PostaCode { get; set; }
        // Customer classı ile ilişki kuruldu.
        public virtual Customer Customer { get; set; }

    }
}
