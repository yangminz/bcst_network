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
    /// The Http request class
    /// Check Microsoft.AspNetCore.Http.HttpRequest
    /// </summary>
    public class HttpRequest
    {
        /// <summary>
        /// The body of the request
        /// </summary>
        public byte[] Body { get; }

        /// <summary>
        /// The length of content sent by the client in bytes
        /// </summary>
        public int ContentLength { get; }

        /// <summary>
        /// Headers of Http Request
        /// </summary>
        public NameValueCollection Headers { get; }

        /// <summary>
        /// The Http data transfer method (such as GET, POST, or HEAD)
        /// </summary>
        public HttpMethodType HttpMethod { get; }

        /// <summary>
        /// The virtual path of the current request
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The protocol of the request. E.g. HTTP/1.1
        /// </summary>
        public string Protocol { get; }

        /// <summary>
        /// The scheme for this url in lowercase
        /// e.g. http://www.contoso.com:8080/shownew.htm?date=today
        /// is having scheme http
        /// https://www.contoso.com:8080/shownew.htm?date=today
        /// is having scheme https
        /// </summary>
        public string Scheme { get; }
    }
}
