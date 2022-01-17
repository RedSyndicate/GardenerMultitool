using System;
using System.Collections.Generic;

namespace GardenersMultitool.Domain.Entities.Schedule
{
    public class Schedule : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        private List<IScheduleEvent> ScheduleEvents = new List<IScheduleEvent>();
        public virtual IReadOnlyCollection<IScheduleEvent> Events => ScheduleEvents.AsReadOnly();

        public static Schedule Create() =>
            new()
            {
                Id = Guid.NewGuid()
            };

    }

    public interface IScheduleEvent
    {

    }
}

