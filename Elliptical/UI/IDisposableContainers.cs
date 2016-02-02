using System;
using System.IO;

namespace Elliptical.Mvc
{
    public class FormContainer : IDisposable
    {
        private readonly bool _closeFormTag;
        private readonly string _tagName;
        private readonly TextWriter _writer;

        public FormContainer(TextWriter writer, string tagName, bool closeFormTag)
        {
            _writer = writer;
            _tagName = tagName;
            _closeFormTag = closeFormTag;
        }

        public void Dispose()
        {
            var closeTags = "";
            if (_closeFormTag)
            {
                closeTags = "</form>" + Environment.NewLine;
            }
            closeTags += "</" + _tagName + ">" + Environment.NewLine;
            _writer.Write(closeTags);
        }
    }

    public class HtmlTagContainer : IDisposable
    {
        private readonly string _tagName;
        private readonly TextWriter _writer;

        public HtmlTagContainer(TextWriter writer, string tagName)
        {
            _writer = writer;
            _tagName = tagName;
        }

        public void Dispose()
        {
            var closingTag = "</" + _tagName + ">" + Environment.NewLine;
            _writer.Write(closingTag);
        }
    }

    public class TemplateSectionContainer : IDisposable
    {
        private readonly string _section;
        private readonly TextWriter _writer;

        public TemplateSectionContainer(TextWriter writer, string section)
        {
            _writer = writer;
            _section = section;
        }

        public void Dispose()
        {
            var closing = "{/" + _section + "}" + Environment.NewLine;
            _writer.Write(closing);
        }
    }
}