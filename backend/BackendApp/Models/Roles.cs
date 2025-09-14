namespace BackendApp.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // Employee, Distributor, ExportManager, Admin, SuperAdmin
        public string? Description { get; set; }

        public ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
