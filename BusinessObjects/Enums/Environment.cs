using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BusinessObjects.Enums
{
    public enum AppEnvironment
    {
        [Description("pps")]
        PPS,
        [Description("american-equity")]
        American_Equity,
        [Description("principal-annuities")]
        Principal_Annuities,
        [Description("default")]
        Default,
    }
}
