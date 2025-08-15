using PersonRegistry.Common.Domains;
using PersonRegistry.Common.Events.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonRegistry.Tests.Unit.Common
{

    internal sealed record DummyEvent(string Name) : IDomainEvent
    {
        Guid IDomainEvent.Id { get; }
        DateTimeOffset IDomainEvent.OccurredOnUtc { get; }
        Guid IDomainEvent.AggregateId { get; }
        string IDomainEvent.EventType { get; set; }
    }

    // Minimal aggregate exposing protected members for testing
    internal sealed class TestAggregate : AggregateRoot<TestAggregate>
    {
        public TestAggregate() { }
        public TestAggregate(Id<TestAggregate>? id) : base(id) { }

        public void DoAddEvent(string name) => AddEvent(new DummyEvent(name));
        public void DoIncrementVersion() => IncrementVersion();
    }

    public class AggregateRootTests
    {
        [Fact]
        public void Initially_NoEvents_VersionZero()
        {
            var agg = new TestAggregate();
            Assert.Equal<uint>(0, agg.Version);
            Assert.Empty(agg.Events);
        }

        [Fact]
        public void First_AddEvent_Increments_Version_And_Stores_Event()
        {
            var agg = new TestAggregate();
            agg.DoAddEvent("e1");

            Assert.Equal<uint>(1, agg.Version);
            Assert.Single(agg.Events);
            Assert.IsType<DummyEvent>(agg.Events.Single());
        }

        [Fact]
        public void Subsequent_AddEvent_Does_Not_Increment_Version_Again()
        {
            var agg = new TestAggregate();
            agg.DoAddEvent("e1");
            agg.DoAddEvent("e2");

            Assert.Equal<uint>(1, agg.Version);
            Assert.Equal(2, agg.Events.Count());
        }

        [Fact]
        public void ClearEvents_Removes_All_But_Does_Not_Touch_Version()
        {
            var agg = new TestAggregate();
            agg.DoAddEvent("e1");
            agg.ClearEvents();

            Assert.Empty(agg.Events);
            Assert.Equal<uint>(1, agg.Version);
        }

        [Fact]
        public void IncrementVersion_Once_Only()
        {
            var agg = new TestAggregate();
            agg.DoIncrementVersion();

            Assert.Equal<uint>(1, agg.Version);

            // Second call is ignored due to _versionIncremented guard
            agg.DoIncrementVersion();
            Assert.Equal<uint>(1, agg.Version);
        }

        [Fact]
        public void IncrementVersion_Before_AddEvent_Prevents_Double_Increment()
        {
            var agg = new TestAggregate();
            agg.DoIncrementVersion();     // Version -> 1
            agg.DoAddEvent("later");      // must NOT increment again

            Assert.Equal<uint>(1, agg.Version);
            Assert.Single(agg.Events);
        }
    }
}
