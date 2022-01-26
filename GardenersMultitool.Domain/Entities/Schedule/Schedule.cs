using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace GardenersMultitool.Domain.Entities.Schedule
{
    public class Schedule : IAggregateRoot
    {
        public string Id { get; protected set; }
        private List<IScheduleEvent> ScheduleEvents = new List<IScheduleEvent>();
        public virtual IReadOnlyCollection<IScheduleEvent> Events => ScheduleEvents.AsReadOnly();

        public static Schedule Create() =>
            new()
            {
                Id = ObjectId.GenerateNewId().ToString()
            };

    }

    public interface IScheduleEvent
    {

    }
}

