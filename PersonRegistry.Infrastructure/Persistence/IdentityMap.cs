using PersonRegistry.Common.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Infrastructure.Persistence
{
    public sealed class IdentityMap
    {
        private readonly Dictionary<object, object> _map = new();

        public void Attach<TDomain, TEntity>(TDomain domain, TEntity entity)
            where TDomain : class where TEntity : class
            => _map[domain] = entity;

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
