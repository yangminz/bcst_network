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
    /// Start Line accepted '\r'
    /// </summary>
    public sealed class StartLineCRParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static StartLineCRParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.StartLine;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<StartLineCRParser> lazy = new Lazy<StartLineCRParser>(
            () => new StartLineCRParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private StartLineCRParser()
        {
        }

        /// <summary>
        /// Parse the character from stream
        /// only accept '\n'
        /// </summary>
        /// <param name="input">The input character</param>
        /// <returns>The parser after accepting this character</returns>
        public IHttpParser StreamParse(char input)
        {
            if (input == '\n')
            {
                // end of parsing start line (request line or status line)
                return FieldKeyParser.Instance;
            }

            throw new HttpParsingException("StartLine ends with '<CR><" + input + ">'");
        }
    }
}
