using Devy.Security.Api.Application.Model.Employees;
using MediatR;

namespace Devy.Security.Api.Application.Query
{
    public class ListEmployeeContactsQuery : IRequest<IReadOnlyCollection<EmployeeContact>>
    {
    }
}
