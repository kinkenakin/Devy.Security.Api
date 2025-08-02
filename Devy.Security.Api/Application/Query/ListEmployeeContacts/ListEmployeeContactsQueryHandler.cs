using AutoMapper;
using Devy.Security.Api.Application.Enums;
using Devy.Security.Api.Application.Model.Employees;
using Devy.Security.Api.Caching;
using Devy.Security.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Devy.Security.Api.Application.Query
{
    public class ListEmployeeContactsQueryHandler : BaseQueryHandler, IRequestHandler<ListEmployeeContactsQuery, IReadOnlyCollection<EmployeeContact>>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ListEmployeeContactsQueryHandler"/> class.
        /// </summary>
        public ListEmployeeContactsQueryHandler(DevySecurityContext context, IMapper mapper) : base(context, mapper) { }

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
        public async Task<IReadOnlyCollection<EmployeeContact>> Handle(ListEmployeeContactsQuery request, CancellationToken cancellationToken)
        {
            object result = CacheExtensions.GetFromCache(CacheKeyEnum.EMPLOYEE_CONTACTS.ToString());

            if (result != null)
                return result as IReadOnlyCollection<EmployeeContact>;

            var data = (from t in await base.DevySecurityContext.EmployeeContacts.ToListAsync()
                        select new EmployeeContact()
                        {
                            EmployeeId = t.EmployeeId,
                            CountryCode = t.CountryCode,
                            LocationId = t.LocationId,
                            Numbers = t.Number == null ? null : t.Number.Split("|").ToArray()
                        }).ToList();

            CacheExtensions.AddToCache(data, CacheKeyEnum.EMPLOYEE_CONTACTS.ToString());

            return data;
        }

        #endregion Public
    }
}
