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
    public sealed class HeaderLineCRLFCRParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static HeaderLineCRLFCRParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.HeaderLine;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<HeaderLineCRLFCRParser> lazy = new Lazy<HeaderLineCRLFCRParser>(
            () => new HeaderLineCRLFCRParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private HeaderLineCRLFCRParser()
        {
        }

        /// <inheritdoc />
        public IHttpParser StreamParse(char input)
        {
            if (input == '\n')
            {
                // end of processing headers
                return BodyEmptyParser.Instance;
            }

            throw new HttpParsingException("Header lines: should get <CR>_<LF>_ as end of all headers but get <" + input + ">");
        }
    }
}
