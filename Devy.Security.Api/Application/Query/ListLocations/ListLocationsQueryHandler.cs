using AutoMapper;
using Devy.Security.Api.Application.Enums;
using Devy.Security.Api.Application.Model.Locations;
using Devy.Security.Api.Caching;
using Devy.Security.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Devy.Security.Api.Application.Query
{
    public class ListLocationsQueryHandler : BaseQueryHandler, IRequestHandler<ListLocationsQuery, IReadOnlyCollection<Location>>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListLocationsQueryHandler"/> class.
        /// </summary>
        public ListLocationsQueryHandler(DevySecurityContext context, IMapper mapper) : base(context, mapper) { }

        #endregion Constructor

        #region Public

        /// <summary>
        /// Handles a request
        /// </summary>
        /// <param name="request">The request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        /// Response from the request
        /// </returns>
        public async Task<IReadOnlyCollection<Location>> Handle(ListLocationsQuery request, CancellationToken cancellationToken)
        {
            object result = CacheExtensions.GetFromCache(CacheKeyEnum.LOCATIONS.ToString());

            if (result != null)
                return result as IReadOnlyCollection<Location>;

            var data = base.Mapper.Map<IReadOnlyCollection<Domain.Entities.Location>, IReadOnlyCollection<Location>>
                (await base.DevySecurityContext.Locations.ToListAsync());


            CacheExtensions.AddToCache(data, CacheKeyEnum.LOCATIONS.ToString());

            return data;
        }

        #endregion Public
    }
}
