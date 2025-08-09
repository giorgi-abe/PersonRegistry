using PersonRegistry.Common.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence
{
    // Simple identity map: tracks which persistence entity corresponds to a given domain object.
    public sealed class IdentityMap
    {
        private readonly Dictionary<object, object> _map = new();

        // Links a domain object to its persistence entity.
        public void Attach<TDomain, TEntity>(TDomain domain, TEntity entity)
            where TDomain : class where TEntity : class
            => _map[domain] = entity;
        // Tries to retrieve the persistence entity for a given domain object.
        public bool TryGetEntity<TDomain, TEntity>(TDomain domain, out TEntity entity)
            where TDomain : class where TEntity : class
        {
            if (_map.TryGetValue(domain!, out var e) && e is TEntity te)
            {
                entity = te;
                return true;
            }
            entity = default!;
            return false;
        }
    }
}
