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
    /// The interface for Http Parsing Node
    /// We are building a parsing graph
    /// These parser nodes are STATELESS, which means they do not store the accepted characters
    /// It's the message's responsibility to store data
    /// </summary>
    public interface IHttpParser
    {
        /// <summary>
        /// Parser reads one character
        /// This is the edge of nodes in parsing graph
        /// </summary>
        /// <param name="input">The input character</param>
        /// <returns>The node after accepting this character</returns>
        public IHttpParser StreamParse(char input);

        /// <summary>
        /// The symbol of current node
        /// </summary>
        public HttpParserType ParserType { get; }
    }
}
