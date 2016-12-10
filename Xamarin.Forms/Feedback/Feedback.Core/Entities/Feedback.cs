using System;
using Microsoft.WindowsAzure.MobileServices;

namespace Feedback.Core.Entities
{
    [DataTable("FeedbackItem")]
    public class Feedback : BaseEntity
    {
        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Время создания
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Идентификатор места
        /// </summary>
        public string PlaceId { get; set; }
    }
}