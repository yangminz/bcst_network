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
    /// The http parser
    /// </summary>
    public sealed class HttpParser
    {
        /// <summary>
        /// The static singleton instance
        /// </summary>
        public static HttpParser Instance => lazy.Value;

        /// <summary>
        /// Beign parsing from start line
        /// </summary>
        public IHttpParser Start => StartLineParser.Instance;

        /// <summary>
        /// Use System.Lazy is thread safe and lazy for the singleton
        /// </summary>
        private static readonly Lazy<HttpParser> lazy = new Lazy<HttpParser>(
            () => new HttpParser());

        /// <summary>
        /// private constructor
        /// </summary>
        private HttpParser()
        {
        }
    }
}
