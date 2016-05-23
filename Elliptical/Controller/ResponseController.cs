using System;
using System.Web.Mvc;

namespace Elliptical.Mvc
{
    public class ResponseController : Controller
    {

        protected virtual void PDFResponseWrite(string fileName, byte[] buffer)
        {
            string contentType = "application/pdf";
            ResponseWrite(contentType, fileName, buffer);
        }

        protected virtual void ResponseWrite(string contentType,string fileName, byte[] buffer)
        {
            Response.ContentType = contentType;
            Response.AddHeader("content-disposition", "inline;filename=" + fileName);
            Response.OutputStream.Write(buffer,0,buffer.Length);
            Response.End();
        }
    }
}
