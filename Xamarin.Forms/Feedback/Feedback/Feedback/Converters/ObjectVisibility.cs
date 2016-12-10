using System.Collections;
using Feedback.Core.Toolbox;

namespace Feedback.UI.Core.Converters
{
    public static class ObjectVisibility
    {
        public static bool IsObjectVisible(object value)
        {
            if(value is bool) return (bool) value;

            bool isVisible = true;
            if(value == null)
            {
                isVisible = false;
            }
            else
            {
                var enumerable = value as IEnumerable;
                if(enumerable != null)
                {
                    isVisible = enumerable.Count() != 0;
                }
                else if(string.IsNullOrEmpty(value.ToString()))
                {
                    isVisible = false;
                }
            }
            return isVisible;
        }
    }
}