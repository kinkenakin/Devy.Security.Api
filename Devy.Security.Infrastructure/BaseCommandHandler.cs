using AutoMapper;

namespace Devy.Security.Infrastructure;

public class BaseCommandHandler
{
    #region Variables

    /// <summary>
    /// Gets or sets the notification context.
    /// </summary>
    /// <value>
    /// The notification context.
    /// </value>
    protected DevySecurityContext DevySecurityContext { get; set; }
    /// <summary>
    /// Gets or sets the mapper.
    /// </summary>
    /// <value>
    /// The mapper.
    /// </value>
    protected IMapper Mapper { get; set; }

    #endregion Variables

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseCommandHandler"/> class.
    /// </summary>
    /// <param name="context">The context.</param>
    public BaseCommandHandler(DevySecurityContext context, IMapper mapper)
    {
        this.DevySecurityContext = context;
        this.Mapper = mapper;
    }

    #endregion Constructors
}
