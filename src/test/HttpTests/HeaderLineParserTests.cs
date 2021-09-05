// BCST - Computer Networks
// Author:      yangminz@outlook.com
// Github:      https://github.com/yangminz/bcst_networks
// Bilibili:    https://space.bilibili.com/4564101
// Zhihu:       https://www.zhihu.com/people/zhao-yang-min
// This project (code repository and videos) is exclusively owned by yangminz 
// and shall not be used for commercial and profitting purpose 
// without yangminz's permission.

using NUnit.Framework;

namespace Networks.Application.Http.Tests
{
    /// <summary>
    /// The test class for start line parsing
    /// </summary>
    public class HeaderLineParserTests
    {
        [Test]
        public void TestHeaderLines()
        {
            string str = "User-Agent";
            IHttpParser p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.FieldKey, p.ParserType);

            str = "User-Agent:";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.FieldValue, p.ParserType);

            str = "User-Agent:Mozilla\r";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.HeaderLine, p.ParserType);

            str = "User-Agent:Mozilla\r\n";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.HeaderLine, p.ParserType);

            str = "User-Agent:Mozilla\r\nC";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.FieldKey, p.ParserType);

            str = "User-Agent:Mozilla\r\nContent-Length:";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.FieldValue, p.ParserType);

            str = "User-Agent:Mozilla\r\nContent-Length:0";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.FieldValue, p.ParserType);

            str = "User-Agent:Mozilla\r\nContent-Length:0\r";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.HeaderLine, p.ParserType);

            str = "User-Agent:Mozilla\r\nContent-Length:0\r\n";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.HeaderLine, p.ParserType);

            str = "User-Agent:Mozilla\r\nContent-Length:0\r\n\r";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.HeaderLine, p.ParserType);

            str = "User-Agent:Mozilla\r\nContent-Length:0\r\n\r\n";
            p = FieldKeyParser.Instance;
            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.Body, p.ParserType);
        }
    }
}
