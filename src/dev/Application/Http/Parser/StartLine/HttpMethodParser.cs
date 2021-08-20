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
    /// The Http method parser
    /// </summary>
    public sealed class HttpMethodParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static HttpMethodParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.HttpMethod;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<HttpMethodParser> lazy = new Lazy<HttpMethodParser>(
            () => new HttpMethodParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private HttpMethodParser()
        {
        }

        /// <summary>
        /// Parse the http method
        /// </summary>
        /// <param name="input">The input character</param>
        /// <returns></returns>
        public IHttpParser StreamParse(char input)
        {
            if (('a' <= input && input <= 'z') ||
                ('A' <= input && input <= 'Z'))
            {
                // return itself
                return HttpMethodParser.Instance;
            }
            else if (input == ' ')
            {
                // Caller should check if the parsed string is a legal http method

                // TODO: support linear white space here
                // the current parser can only handle one space situation
                return UrlParser.Instance;
            }

            throw new HttpParsingException("Http Method cannot parse char <" + input + ">");
        }
    }
}
