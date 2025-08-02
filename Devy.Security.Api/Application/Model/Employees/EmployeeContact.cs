namespace Devy.Security.Api.Application.Model.Employees;

public class EmployeeContact
{
    public int EmployeeId { get; set; }
    public string CountryCode { get; set; }
    public IReadOnlyCollection<string> Numbers { get; set; }
    public int LocationId { get; set; }
}
