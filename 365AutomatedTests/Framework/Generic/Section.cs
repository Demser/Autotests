using System;
using Coypu;

namespace _365AutomatedTests.Framework.Generic
{
    using static Crawler;

    using static TimeSpan;

    public abstract class Section
    {
        protected static readonly Options LongTimeout = new Options { Timeout = FromSeconds(30) };

        protected Section()
        {
        }

        protected Section(string locator)
        {
            Scope = Session.FindCss(locator);
        }

        protected ElementScope Scope { get; set; }
    }
}
