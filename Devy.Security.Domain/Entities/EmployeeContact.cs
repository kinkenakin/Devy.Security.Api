
namespace Devy.Security.Domain.Entities;

public partial class EmployeeContact
{
    public int EmployeeId { get; set; }
    public string CountryCode { get; set; }
    public string Number { get; set; }
    public int LocationId { get; set; }
}
