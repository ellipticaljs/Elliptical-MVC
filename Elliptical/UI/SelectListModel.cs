using System.Collections.Generic;
using System.Web.Mvc;

namespace Elliptical.Mvc
{
    public interface ISelectListModel<T>
    {
        ICollection<T> Options { get; set; }
        string SelectedProperty { get; set; }
        string ValueProperty { get; set; }
        string TextProperty { get; set; }
    }

    public class SelectListModel : ISelectListModel<SelectListItem>
    {
        public SelectListModel()
        {
            ValueProperty = null;
            TextProperty = null;
        }

        public ICollection<SelectListItem> Options { get; set; }
        public string SelectedProperty { get; set; }
        public string ValueProperty { get; set; }
        public string TextProperty { get; set; }
    }

    public class SelectBinding
    {
        public string Property { get; set; }
        public string CollectionProperty { get; set; }
    }

    
}