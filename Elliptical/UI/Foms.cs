namespace Elliptical.Mvc
{
    /// <summary>
    /// </summary>
    public class HtmlForm
    {
        public HtmlForm()
        {
            SubmitLabel = new SubmitLabel();
        }

        public SubmitLabel SubmitLabel { get; set; }
    }

    /// <summary>
    /// </summary>
    public class SubmitLabel
    {
        public SubmitLabel()
        {
            Css = "";
            CssDisplay = "";
            Message = "";
        }

        public string Css { get; set; }
        public string CssDisplay { get; set; }
        public string Message { get; set; }
    }

   
}