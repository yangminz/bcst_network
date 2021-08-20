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
    public sealed class HttpVersionParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static HttpVersionParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.HttpVersion;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<HttpVersionParser> lazy = new Lazy<HttpVersionParser>(
            () => new HttpVersionParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private HttpVersionParser()
        {
        }

        /// <summary>
        /// The http version has 2 positions:
        /// 1. At the end of a request line
        /// 2. At the head of a status line
        /// it's caller's responsibility to do the regex check
        /// </summary>
        /// <param name="input">The input character</param>
        /// <returns>The parser after accepting this character</returns>
        public IHttpParser StreamParse(char input)
        {
            if (input == 'T' || input == 't' ||
                input == 'P' || input == 'p' ||
                input == '/' || input == '1' || input == '.')
            {
                // this only accepts HTTP/1.1
                return HttpVersionParser.Instance;
            }
            else if (input == ' ')
            {
                // TODO: support linear white space
                // status line:
                // HTTP version <sp> 3-digit <sp> reason phrase
                return StatusCodeParser.Instance;
            }
            else if (input == '\r')
            {
                // TODO: support linear white space
                // request line:
                // HTTP method <sp> url <sp> HTTP version
                return StartLineCRParser.Instance;
            }

            throw new HttpParsingException("HttpVersion cannot parse char <" + input + ">");
        }
    }
}
