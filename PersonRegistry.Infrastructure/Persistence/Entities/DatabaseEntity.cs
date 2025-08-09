using PersonRegistry.Common.Domains.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence.Entities
{
    public class DatabaseEntity : IAuditableEntity, ISoftDeletable
    {
        public DateTimeOffset? CreatedAtUtc { get; set; }
        public DateTimeOffset? LastModifiedAtUtc { get; set; }
        public bool IsDeleted { get; set; }

        public void MarkDeleted()
        {
            throw new NotImplementedException();
        }
    }
}
