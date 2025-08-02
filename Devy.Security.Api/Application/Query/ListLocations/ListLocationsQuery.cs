using Devy.Security.Api.Application.Model.Locations;
using MediatR;

namespace Devy.Security.Api.Application.Query
{
    public class ListLocationsQuery : IRequest<IReadOnlyCollection<Location>>
    {
    }
}
