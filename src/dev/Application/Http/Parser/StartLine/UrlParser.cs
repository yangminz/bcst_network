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
    public sealed class UrlParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static UrlParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.Url;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<UrlParser> lazy = new Lazy<UrlParser>(
            () => new UrlParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private UrlParser()
        {
        }

        /// <summary>
        /// Parse the character from stream
        /// </summary>
        /// <param name="input">The input character</param>
        /// <returns>The parser after accepting this character</returns>
        public IHttpParser StreamParse(char input)
        {
            if (('a' <= input && input <= 'z') ||
                ('A' <= input && input <= 'Z') ||
                ('0' <= input && input <= '9') ||
                input == ':' || input == '/' || input == '.' ||
                input == '?' || input == '%' || input == '*')
            {
                return UrlParser.Instance;
            }
            else if (input == ' ')
            {
                // end of parsing url
                // TODO: support linear white spaces
                return HttpVersionParser.Instance;
            }

            throw new HttpParsingException("Url cannot parse char <" + input + ">");
        }
    }
}
