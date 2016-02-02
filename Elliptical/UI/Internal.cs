using RevStack.Mvc;
namespace Elliptical.Mvc
{
    internal static class ElementExtensions
    {
        public static string ToOptionSelect(this string source)
        {
            if (source.InString("Select"))
            {
                return source;
            }
            return "Select " + source;
        }
    }

    internal class MetaSelectModel
    {
        public MetaSelectModel()
        {
            ValueProperty = "Value";
            TextProperty = "Text";
        }

        public string Options { get; set; }
        public string SelectedProperty { get; set; }
        public string ValueProperty { get; set; }
        public string TextProperty { get; set; }
    }
}