using System;
using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.Core.Entities
{
    [DataTable("FeedbackItem")]
    public class Feedback : BaseEntity
    {
        public string Text { get; set; }
        public string UserEmail { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string PlaceId { get; set; }
    }
}