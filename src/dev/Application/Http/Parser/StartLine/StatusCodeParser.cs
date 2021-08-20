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
    public sealed class StatusCodeParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static StatusCodeParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.StatusCode;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<StatusCodeParser> lazy = new Lazy<StatusCodeParser>(
            () => new StatusCodeParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private StatusCodeParser()
        {
        }

        /// <summary>
        /// Parse the character from stream
        /// </summary>
        /// <param name="input">The input character</param>
        /// <returns>The parser after accepting this character</returns>
        public IHttpParser StreamParse(char input)
        {
            if ('0' <= input && input <= '9')
            {
                // TODO: support linear white spaces
                return StatusCodeParser.Instance;
            }
            else if (input == ' ')
            {
                return ReasonPhraseParser.Instance;
            }

            throw new HttpParsingException("StatusCode cannot parse char <" + input + ">");

        }
    }
}
