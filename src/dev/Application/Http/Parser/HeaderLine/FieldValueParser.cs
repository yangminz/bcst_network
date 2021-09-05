// BCST - Computer Networks
// Author:      yangminz@outlook.com
// Github:      https://github.com/yangminz/bcst_networks
// Bilibili:    https://space.bilibili.com/4564101
// Zhihu:       https://www.zhihu.com/people/zhao-yang-min
// This project (code repository and videos) is exclusively owned by yangminz 
// and shall not be used for commercial and profitting purpose 
// without yangminz's permission.

using System;

namespace Networks.Application.Http
{
    /// <summary>
    /// The url parser
    /// </summary>
    public sealed class FieldValueParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static FieldValueParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.FieldValue;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<FieldValueParser> lazy = new Lazy<FieldValueParser>(
            () => new FieldValueParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private FieldValueParser()
        {
        }

        /// <inheritdoc />
        public IHttpParser StreamParse(char input)
        {
            if (('a' <= input && input <= 'z') ||
                ('A' <= input && input <= 'Z') ||
                ('0' <= input && input <= '9') ||
                input == ':' || input == '/' || input == '.' ||
                input == '?' || input == '%' || input == '*' ||
                input == '=' || input == '&' || input == '~' ||
                input == '(' || input == ')' || input == '\"' ||
                input == '<' || input == '>' || input == '-' ||
                input == ' ')
            {
                return FieldValueParser.Instance;
            }
            else if (input == '\r')
            {
                // end of processing one header line
                return HeaderLineCRParser.Instance;
            }

            throw new HttpParsingException("Illegal char for header value <" + input + ">");
        }
    }
}
