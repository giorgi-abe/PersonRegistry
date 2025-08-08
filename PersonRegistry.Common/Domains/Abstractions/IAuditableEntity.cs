using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Common.Domains.Abstractions
{
    public interface IAuditableEntity
    {
        public DateTimeOffset? CreatedAtUtc { get; }
        public DateTimeOffset? LastModifiedAtUtc { get; }
    }
}
