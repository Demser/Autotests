using NUnit.Framework;
using System;
using Coypu;

namespace _365AutomatedTests.Framework.Generic
{
    using static Crawler;
    using static TestContext;

    public abstract class Page
    {
        private readonly Func<string> _url = () => Session.Location.AbsoluteUri;

        protected Page()
        {
            Scope = Session.FindXPath("/html");
            LogUrl();
        }

        protected Page(ElementScope scope)
        {
            Scope = scope;
            LogUrl();
        }

        protected Page(string css)
        {
            Scope = Session.FindCss(css);
            LogUrl();
        }

        protected ElementScope Scope { get; }

        public string Url
        {
            get
            {
                LogUrl();
                return _url();
            }
        }

        private void LogUrl() => WriteLine(string.Concat("Current ", GetType().Name, " url: ", _url()));
    }
}
