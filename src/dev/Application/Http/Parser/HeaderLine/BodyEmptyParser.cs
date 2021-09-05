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
    public sealed class BodyEmptyParser : IHttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static BodyEmptyParser Instance => lazy.Value;

        /// <summary>
        /// The symbol type of this node
        /// </summary>
        public HttpParserType ParserType => HttpParserType.Body;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<BodyEmptyParser> lazy = new Lazy<BodyEmptyParser>(
            () => new BodyEmptyParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private BodyEmptyParser()
        {
        }

        /// <inheritdoc />
        public IHttpParser StreamParse(char input)
        {
            // This body byte parser works as terminator of
            // the parsing of start line and header lines
            // When the type is found to be Body,
            // the caller should stop character parsing
            return BodyEmptyParser.Instance;
        }
    }
}
