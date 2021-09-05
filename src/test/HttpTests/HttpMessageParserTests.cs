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
    public class HttpMessageParserTests
    {
        [Test]
        public void TestRequest()
        {
            string str = "GET / HTTP/1.1\r\ncontent-length:4\r\nuser-agent:chrome\r\n\r\nHTML";
            IHttpParser p = HttpParser.Instance.Start;

            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.Body, p.ParserType);
        }

        [Test]
        public void TestResponse()
        {
            string str = "HTTP/1.1 200 OK\r\ncontent-length:4\r\nuser-agent:chrome\r\n\r\nHTML";
            IHttpParser p = HttpParser.Instance.Start;

            foreach (char c in str)
            {
                p = p.StreamParse(c);
            }
            Assert.AreEqual(HttpParserType.Body, p.ParserType);
        }

        [Test]
        public void TestBadRequest()
        {
            IHttpParser p = HttpParser.Instance.Start;
            Assert.Throws<HttpParsingException>(
                () =>
                {
                    foreach (char c in "GET / HTTPabcd/1.1\r\ncontent-length:4\r\nuser-agent:chrome\r\n\r\nHTML")
                    {
                        p = p.StreamParse(c);
                    }
                });

            p = HttpParser.Instance.Start;
            Assert.Throws<HttpParsingException>(
                () =>
                {
                    foreach (char c in "GET / HTTP/1.1\rhello\ncontent-length:4\r\nuser-agent:chrome\r\n\r\nHTML")
                    {
                        p = p.StreamParse(c);
                    }
                });

            p = HttpParser.Instance.Start;
            Assert.Throws<HttpParsingException>(
                () =>
                {
                    foreach (char c in "GET / HTTP/1.1\r\ncontent-length:4\r\nuser-agent\r\n\r\nHTML")
                    {
                        p = p.StreamParse(c);
                    }
                });
        }
    }
}
