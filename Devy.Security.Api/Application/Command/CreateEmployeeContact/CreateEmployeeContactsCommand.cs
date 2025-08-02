using Devy.Security.Api.Application.Model.Employees;
using MediatR;

namespace Devy.Security.Api.Application.Query
{
    public class CreateEmployeeContactsCommand : IRequest<EmployeeContact>
    {
        public string EmployeeId { get; set; }
        public string CountryCode { get; set; }
        public IReadOnlyCollection<string> Numbers { get; set; }
        public int LocationId { get; set; }
    }
}
