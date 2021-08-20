// BCST - Computer Networks
// Author:      yangminz@outlook.com
// Github:      https://github.com/yangminz/bcst_networks
// Bilibili:    https://space.bilibili.com/4564101
// Zhihu:       https://www.zhihu.com/people/zhao-yang-min
// This project (code repository and videos) is exclusively owned by yangminz 
// and shall not be used for commercial and profitting purpose 
// without yangminz's permission.

using System;
using System.Collections.Generic;

namespace Networks.Application.Http
{
    /// <summary>
    /// The static class of Http constants
    /// </summary>
    public static class HttpConstants
    {
        /// <summary>
        /// CR char
        /// </summary>
        public const char CR = '\r';

        /// <summary>
        /// LF char
        /// </summary>
        public const char LF = '\n';

        /// <summary>
        /// CLRF string
        /// </summary>
        public const string CRLF = "\r\n";

        /// <summary>
        /// Comma string
        /// </summary>
        public const string Comma = ":";

        /// <summary>
        /// Http version string
        /// </summary>
        public const string HttpVersion = "HTTP/1.1";

        /// <summary>
        /// Header key : content-length
        /// </summary>
        public const string ContentLength = "Content-Length";

        /// <summary>
        /// Header key : user-agent
        /// </summary>
        public const string UserAgent = "User-Agent";

        /// <summary>
        /// Header key : connection
        /// </summary>
        public const string Connection = "Connection";

        /// <summary>
        /// Header value of <connection> : close
        /// </summary>
        public const string Close = "close";

        /// <summary>
        /// Header value of <connection> : keep-alive
        /// </summary>
        public const string KeepAlive = "Keep-Alive";

        /// <summary>
        /// Mapping from HttpStatusCodeType to its string
        /// </summary>
        public static readonly Dictionary<HttpStatusCodeType, string> HttpCodeMap = new Dictionary<HttpStatusCodeType, string>()
        {
            { (HttpStatusCodeType)100, "Continue" },
            { (HttpStatusCodeType)101, "SwitchingProtocols" },
            { (HttpStatusCodeType)200, "OK" },
            { (HttpStatusCodeType)201, "Created" },
            { (HttpStatusCodeType)202, "Accepted" },
            { (HttpStatusCodeType)203, "Non-AuthoritativeInformation" },
            { (HttpStatusCodeType)204, "NoContent" },
            { (HttpStatusCodeType)205, "ResetContent" },
            { (HttpStatusCodeType)206, "PartialContent" },
            { (HttpStatusCodeType)300, "MultipleChoices" },
            { (HttpStatusCodeType)301, "MovedPermanently" },
            { (HttpStatusCodeType)302, "Found" },
            { (HttpStatusCodeType)303, "SeeOther" },
            { (HttpStatusCodeType)304, "NotModified" },
            { (HttpStatusCodeType)305, "UseProxy" },
            { (HttpStatusCodeType)307, "TemporaryRedirect" },
            { (HttpStatusCodeType)400, "BadRequest" },
            { (HttpStatusCodeType)401, "Unauthorized" },
            { (HttpStatusCodeType)402, "PaymentRequired" },
            { (HttpStatusCodeType)403, "Forbidden" },
            { (HttpStatusCodeType)404, "NotFound" },
            { (HttpStatusCodeType)405, "MethodNotAllowed" },
            { (HttpStatusCodeType)406, "NotAcceptable" },
            { (HttpStatusCodeType)407, "ProxyAuthenticationRequired" },
            { (HttpStatusCodeType)408, "RequestTime-out" },
            { (HttpStatusCodeType)409, "Conflict" },
            { (HttpStatusCodeType)410, "Gone" },
            { (HttpStatusCodeType)411, "LengthRequired" },
            { (HttpStatusCodeType)412, "PreconditionFailed" },
            { (HttpStatusCodeType)413, "RequestEntityTooLarge" },
            { (HttpStatusCodeType)414, "Request-URITooLarge" },
            { (HttpStatusCodeType)415, "UnsupportedMediaType" },
            { (HttpStatusCodeType)416, "Requestedrangenotsatisfiable" },
            { (HttpStatusCodeType)417, "ExpectationFailed" },
            { (HttpStatusCodeType)500, "InternalServerError" },
            { (HttpStatusCodeType)501, "NotImplemented" },
            { (HttpStatusCodeType)502, "BadGateway" },
            { (HttpStatusCodeType)503, "ServiceUnavailable" },
            { (HttpStatusCodeType)504, "GatewayTime-out" },
            { (HttpStatusCodeType)505, "HTTPVersionnotsupported" }
        };

        /// <summary>
        /// The mapping from method string to its type (ignore case)
        /// </summary>
        public static readonly Dictionary<string, HttpMethodType> HttpMethodMap = new Dictionary<string, HttpMethodType>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "GET", HttpMethodType.GET },
            { "HEAD", HttpMethodType.HEAD },
            { "POST", HttpMethodType.POST },
            { "PUT", HttpMethodType.PUT },
            { "DELETE", HttpMethodType.DELETE },
            { "TRACE", HttpMethodType.TRACE },
            { "CONNECT", HttpMethodType.CONNECT },
        };

        /// <summary>
        /// Check if the string is "Http/1.1"
        /// </summary>
        /// <param name="str">The input string</param>
        /// <returns>true if it is</returns>
        public static bool HttpVersionMatch(string str)
        {
            return 0 == string.Compare(str, HttpConstants.HttpVersion,
                ignoreCase: true);
        }
    }
}
