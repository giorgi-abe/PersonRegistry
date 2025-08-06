using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Common.Contracts
{
    public interface IPagingRequest
    {
        int Page { get; }
        int PageSize { get; }
    }
}
