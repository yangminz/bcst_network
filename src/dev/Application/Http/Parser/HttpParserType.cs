/* BCST - Computer Networks
 * Author:      yangminz@outlook.com
 * Github:      https://github.com/yangminz/bcst_networks
 * Bilibili:    https://space.bilibili.com/4564101
 * Zhihu:       https://www.zhihu.com/people/zhao-yang-min
 * This project (code repository and videos) is exclusively owned by yangminz 
 * and shall not be used for commercial and profitting purpose 
 * without yangminz's permission.
 */

namespace Networks.Application.Http
{
    /// <summary>
    /// The parsing status of a HTTP symbol
    /// </summary>
    public enum HttpParserType
    {
        StartLine,
        RequestLine,
        HttpMethod,
        Url,
        HttpVersion,
        StatusLine,
        StatusCode,
        ReasonPhrase,
        HeaderLine,
        FieldKey,
        FieldValue,
    }
}
