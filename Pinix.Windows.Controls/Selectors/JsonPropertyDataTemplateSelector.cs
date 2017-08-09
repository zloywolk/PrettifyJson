using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace Pinix.Windows.Controls.Selectors
{
    class JsonPropertyDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ObjectTemplate { get; set; }
        public DataTemplate ValueTemplate { get; set; }
        public DataTemplate ArrayTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null || !(container is FrameworkElement)) return null;
            var fe = (FrameworkElement) container;

            var property = item as JProperty;
            if (property != null)
            {
                switch (property.Value.Type)
                {
                    case JTokenType.Array:
                        return ArrayTemplate;
                    case JTokenType.Object:
                        return ObjectTemplate;
                    default:
                        return ValueTemplate;
                }
            }

            //return  base.SelectTemplate(item, container);
            try
            {
                var key = new DataTemplateKey(item.GetType());
                return ((FrameworkElement)container).FindResource(key) as DataTemplate;
            }
            catch (System.Exception)
            {
                // TODO: Fix it
                return base.SelectTemplate(item, container);
            }
        }
    }
}
