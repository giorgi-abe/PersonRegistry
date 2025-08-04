using PersonRegistry.Common.Domains.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Common.Domains
{
    public abstract class Entity<TModel> : IAuditableEntity , ISoftDeletable
    {
        public Id<TModel> Id { get; }
        public DateTimeOffset CreatedAtUtc { get; protected set; }
        public DateTimeOffset LastModifiedAtUtc { get; protected set; }
        public bool IsDeleted { get; protected set; }

        protected Entity(Id<TModel> id)
        {
            Id = id;
        }

        protected Entity() : this(Id<TModel>.New()) { }
        public override bool Equals(object? obj)
        {
            if (obj is Entity<TModel> entity)
            {
                return entity.Id == Id;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void MarkDeleted() => IsDeleted = true;
    }
}
