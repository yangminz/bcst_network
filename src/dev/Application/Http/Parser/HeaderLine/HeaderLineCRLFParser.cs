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
    public sealed class HeaderLineCRLFParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static HeaderLineCRLFParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.HeaderLine;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<HeaderLineCRLFParser> lazy = new Lazy<HeaderLineCRLFParser>(
            () => new HeaderLineCRLFParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private HeaderLineCRLFParser()
        {
        }

        /// <inheritdoc />
        public IHttpParser StreamParse(char input)
        {
            if (input == '-' ||
                ('a' <= input && input <= 'z') ||
                ('A' <= input && input <= 'Z'))
            {
                // get another header line
                return FieldKeyParser.Instance;
            }
            else if (input == '\r')
            {
                // going to the end of processing all header lines
                return HeaderLineCRLFCRParser.Instance;
            }

            throw new HttpParsingException("Header lines: should be one new header or end of headers but get <" + input + ">");
        }
    }
}
