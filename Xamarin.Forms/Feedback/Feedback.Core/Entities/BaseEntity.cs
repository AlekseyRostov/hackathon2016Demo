using System;

namespace Feedback.Core.Entities
{
    public class BaseEntity
    {
        public string Id { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}