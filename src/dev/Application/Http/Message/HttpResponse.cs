// BCST - Computer Networks
// Author:      yangminz@outlook.com
// Github:      https://github.com/yangminz/bcst_networks
// Bilibili:    https://space.bilibili.com/4564101
// Zhihu:       https://www.zhihu.com/people/zhao-yang-min
// This project (code repository and videos) is exclusively owned by yangminz 
// and shall not be used for commercial and profitting purpose 
// without yangminz's permission.

using System.Collections.Specialized;

namespace Networks.Application.Http
{
    /// <summary>
    /// The Http response class
    /// Check Microsoft.AspNetCore.Http.HttpResponse
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// The body of the response
        /// </summary>
        public byte[] Body { get; }

        /// <summary>
        /// The content length of the body
        /// </summary>
        public int ContentLength;

        /// <summary>
        /// The headers
        /// </summary>
        public NameValueCollection Headers;

        /// <summary>
        /// The http status code
        /// </summary>
        public HttpStatusCodeType StatusCode;
    }
}
