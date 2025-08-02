using AutoMapper;
using Devy.Security.Api.Application.Enums;
using Devy.Security.Api.Application.Model.Employees;
using Devy.Security.Api.Caching;
using Devy.Security.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Devy.Security.Api.Application.Query
{
    public class CreateEmployeeContactsCommandHandler : BaseCommandHandler, IRequestHandler<CreateEmployeeContactsCommand, EmployeeContact>
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEmployeeContactsCommandHandler"/> class.
        /// </summary>
        public CreateEmployeeContactsCommandHandler(DevySecurityContext context, IMapper mapper) : base(context, mapper) { }

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
        public async Task<EmployeeContact> Handle(CreateEmployeeContactsCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.EmployeeContact data = new();
            base.Mapper.Map<CreateEmployeeContactsCommand, Domain.Entities.EmployeeContact>(request, data);

            await base.DevySecurityContext.EmployeeContacts.AddAsync(data);
            await base.DevySecurityContext.SaveChangesAsync();

            CacheExtensions.RemoveFromCache(CacheKeyEnum.EMPLOYEE_CONTACTS.ToString());
            return base.Mapper.Map<Domain.Entities.EmployeeContact, EmployeeContact>(data);
        }

        #endregion Public
    }
}
