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
    /// The start line parser
    /// </summary>
    public sealed class StartLineParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static StartLineParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.StartLine;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<StartLineParser> lazy = new Lazy<StartLineParser>(
            () => new StartLineParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private StartLineParser()
        {
        }

        /// <summary>
        /// Parse the character from stream
        /// it can be request line or status line
        /// </summary>
        /// <param name="input">The input character</param>
        /// <returns>The parser after accepting this character</returns>
        public IHttpParser StreamParse(char input)
        {
            // start line can be
            // request line:
            //      http method <SP> url <SP> http version <CR><LF>
            // status line:
            //      http version <SP> 3-digit status code <SP> reason phrase <CR><LF>
            // we consider the FIRST set of HttpMethod and HttpVersion
            // HttpMethod FIRST set = {'O', 'G', 'P', 'H', 'D', 'T', 'C'}
            // HttpVersion FIRST set = {'H'}

            if (input == 'O' || input == 'o' ||     // OPTION - not implemented here
                input == 'G' || input == 'g' ||     // GET
                input == 'P' || input == 'p' ||     // PUT
                input == 'D' || input == 'd' ||     // DELETE - not implemented here
                input == 'T' || input == 't' ||     // TRACE - not implemented here
                input == 'C' || input == 'c'        // CONNECT - not implemented here
                )
            {
                return HttpMethodParser.Instance;
            }
            else if (input == 'H' || input == 'h')
            {
                // 'H' can be
                // 1. HEAD method in request
                // 2. HTTP/1.1 version in response
                return StartLineHParser.Instance;
            }

            throw new HttpParsingException("StartLine cannot parse char <" + input + ">");
        }
    }
}
