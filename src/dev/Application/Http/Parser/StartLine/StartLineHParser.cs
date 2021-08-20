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
    /// Start line cannot decide it's a request or response till now
    /// because the first char is 'H'
    /// </summary>
    public sealed class StartLineHParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static StartLineHParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.StartLine;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<StartLineHParser> lazy = new Lazy<StartLineHParser>(
            () => new StartLineHParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private StartLineHParser()
        {
        }

        /// <summary>
        /// Parse the character from stream
        /// determine if it's a request or response
        /// </summary>
        /// <param name="input">The input character</param>
        /// <returns>The parser after accepting this character</returns>
        public IHttpParser StreamParse(char input)
        {
            // 'H' already accepted
            // "HT" is the prefix of "HTTP/1.1" - response
            // "HE" is the prefix of "HEAD" - request
            if (input == 'T' || input == 't')
            {
                // response line
                return HttpVersionParser.Instance;
            }
            else if (input == 'E' || input == 'e')
            {
                // HEAD method
                return HttpMethodParser.Instance;
            }

            throw new HttpParsingException("StartLine cannot parse char 'H<" + input + ">'");
        }
    }
}
