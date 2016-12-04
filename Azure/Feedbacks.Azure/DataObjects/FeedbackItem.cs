using Microsoft.Azure.Mobile.Server;
using System;

namespace Feedbacks.Azure.DataObjects
{
    /// <summary>
    /// Модель отзыва
    /// </summary>
    public class FeedbackItem : EntityData
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