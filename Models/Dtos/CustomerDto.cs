namespace invenio.Models.Dtos
{
    public record CustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    public record CreateCustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ManagerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
    
    public record UpdateCustomerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ManagerName { get; set; }
        public string PhoneNumber { get; set; }
    }
    
}
