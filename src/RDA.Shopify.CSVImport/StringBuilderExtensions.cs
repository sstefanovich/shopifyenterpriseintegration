using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace RDA.Shopify.CSVImport
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendQuoted(this StringBuilder builder, string value)
        {
            if (string.IsNullOrEmpty(value))
                return builder.Append(string.Empty);
            else
                return builder.Append($"\"{value}\"");
        }
    }
}
