namespace FluentValidationApp.Models
{
    /// <summary>
    /// Code First yaklaşımı kullanıldı.
    /// Database tarfına yansıyacak model class.
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public DateTime? BirthDay { get; set; }
        // Address model classı ile ilişki kuruldu.
        public IList<Address> Addresses { get; set; }

    }
}
