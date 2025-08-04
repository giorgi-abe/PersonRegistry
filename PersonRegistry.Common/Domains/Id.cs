using PersonRegistry.Common.Domains.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Common.Domains
{
    public class Id<TModel> : IId, IComparable, IComparable<IId>, IComparable<Guid>, IEquatable<IId>
    {
        public Guid Value { get; }
        public Id(Guid value) => Value = value;
        public Id() : this(Guid.NewGuid()) { }


        // factory methods
        public static Id<TModel> New() => new(Guid.NewGuid());
        public static Id<TModel> FromId<TNewModel>(Id<TNewModel> id) => new(id.Value);
        public static Id<TModel> FromGuid(Guid id) => new(id);
        public static Id<TModel> FromString(string id) => new(Guid.Parse(id));


        public static implicit operator Guid?(Id<TModel>? id) => id?.Value;

        public static implicit operator Guid(Id<TModel> id) => id.Value;

        public static implicit operator Id<TModel>(Guid id) => new(id);


        // Comparisons
        public int CompareTo(object? obj)
        {
            if (obj is IId otherId) return CompareTo(otherId);
            if (obj is Guid otherGuid) return CompareTo(otherGuid);
            if (obj == null) return 1;

            throw new ArgumentException("Object must be of type IId or Guid", nameof(obj));
        }

        public int CompareTo(IId? other) => other?.Value.CompareTo(Value) ?? 1;
        public int CompareTo(Guid other) => Value.CompareTo(other);

        // Equality
        public bool Equals(IId? other) => other?.Value == Value;
    }
}
