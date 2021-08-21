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
    public class StartLineParserTests
    {
        [Test]
        public void TestMethod()
        {
            string[] methods = new string[]
            {
                "OPTIONS",
                "GET",
                "POST",
                "HEAD",
                "PUT",
                "DELETE",
                "TRACE",
                "CONNECT",
            };

            foreach (string method in methods)
            {
                foreach (char c in method)
                {
                    Assert.AreEqual(HttpParserType.HttpMethod,
                        HttpMethodParser.Instance.StreamParse(c).ParserType);
                }
            }
        }

        [Test]
        public void TestUrl()
        {
            string url = "https://cn.bing.com/search?q=plato%20republic&qs=n&form=QBRE&sp=-1&pq=plato%20republic&sc=8-14&sk=";
            foreach (char c in url)
            {
                Assert.AreEqual(HttpParserType.Url,
                    UrlParser.Instance.StreamParse(c).ParserType);
            }
        }

        [Test]
        public void TestVersion()
        {
            string version = "HTTP/1.1";
            foreach (char c in version)
            {
                Assert.AreEqual(HttpParserType.HttpVersion,
                    HttpVersionParser.Instance.StreamParse(c).ParserType);
            }
        }

        [Test]
        public void TestStatusCode()
        {
            for (int code = 100; code < 1000; ++ code)
            {
                foreach (char c in code.ToString())
                {
                    Assert.AreEqual(HttpParserType.StatusCode,
                        StatusCodeParser.Instance.StreamParse(c).ParserType);
                }
            }
        }

        [Test]
        public void TestReasonPhrase()
        {
            string reason = "Bad Request";
            foreach (char c in reason)
            {
                Assert.AreEqual(HttpParserType.ReasonPhrase,
                    ReasonPhraseParser.Instance.StreamParse(c).ParserType);
            }
        }

        [Test]
        public void TestGetRequestLine()
        {
            string method = "GET";
            string url = "https://helloworld.com:443?query=computer%20science";
            string version = "Http/1.1";
            string requestLine = method + " " + url + " " + version + "\r\n";

            IHttpParser p = HttpParser.Instance.Start;
            Assert.AreEqual(HttpParserType.StartLine, p.ParserType);

            int i = 0;
            foreach (char c in requestLine)
            {
                p = p.StreamParse(c);

                if (0 <= i && i < method.Length)
                {
                    Assert.AreEqual(HttpParserType.HttpMethod, p.ParserType);
                }
                else if (method.Length <= i && i < 1 + method.Length + url.Length)
                {
                    Assert.AreEqual(HttpParserType.Url, p.ParserType);
                }
                else if (1 + method.Length + url.Length <= i && i < 2 + method.Length + url.Length + version.Length)
                {
                    Assert.AreEqual(HttpParserType.HttpVersion, p.ParserType);
                }
                else if (i == 2 + method.Length + url.Length + version.Length)
                {
                    Assert.AreEqual(HttpParserType.StartLine, p.ParserType);
                }
                else if (i == 3 + method.Length + url.Length + version.Length)
                {
                    Assert.AreEqual(HttpParserType.FieldKey, p.ParserType);
                }

                i += 1;
            }
        }

        [Test]
        public void TestHeadRequestLine()
        {
            string method = "head";
            string url = "https://helloworld.com:443?query=computer%20science";
            string version = "Http/1.1";
            string requestLine = method + " " + url + " " + version + "\r\n";

            IHttpParser p = HttpParser.Instance.Start;
            Assert.AreEqual(HttpParserType.StartLine, p.ParserType);

            int i = 0;
            foreach (char c in requestLine)
            {
                p = p.StreamParse(c);

                if (0 == i)
                {
                    Assert.AreEqual(HttpParserType.StartLine, p.ParserType);
                }
                if (1 <= i && i < method.Length)
                {
                    Assert.AreEqual(HttpParserType.HttpMethod, p.ParserType);
                }
                else if (method.Length <= i && i < 1 + method.Length + url.Length)
                {
                    Assert.AreEqual(HttpParserType.Url, p.ParserType);
                }
                else if (1 + method.Length + url.Length <= i && i < 2 + method.Length + url.Length + version.Length)
                {
                    Assert.AreEqual(HttpParserType.HttpVersion, p.ParserType);
                }
                else if (i == 2 + method.Length + url.Length + version.Length)
                {
                    Assert.AreEqual(HttpParserType.StartLine, p.ParserType);
                }
                else if (i == 3 + method.Length + url.Length + version.Length)
                {
                    Assert.AreEqual(HttpParserType.FieldKey, p.ParserType);
                }

                i += 1;
            }
        }

        [Test]
        public void TestStatusLine()
        {
            string version = "http/1.1";
            string code = "500";
            string reason = "internal error";
            string statusLine = version + " " + code + " " + reason + "\r\n";

            IHttpParser p = HttpParser.Instance.Start;
            Assert.AreEqual(HttpParserType.StartLine, p.ParserType);

            int i = 0;
            foreach (char c in statusLine)
            {
                p = p.StreamParse(c);

                if (0 == i)
                {
                    Assert.AreEqual(HttpParserType.StartLine, p.ParserType);
                }
                if (1 <= i && i < version.Length)
                {
                    Assert.AreEqual(HttpParserType.HttpVersion, p.ParserType);
                }
                else if (version.Length <= i && i < 1 + version.Length + code.Length)
                {
                    Assert.AreEqual(HttpParserType.StatusCode, p.ParserType);
                }
                else if (1 + version.Length + code.Length <= i && i < 2 + version.Length + code.Length + reason.Length)
                {
                    Assert.AreEqual(HttpParserType.ReasonPhrase, p.ParserType);
                }
                else if (i == 2 + version.Length + code.Length + reason.Length)
                {
                    Assert.AreEqual(HttpParserType.StartLine, p.ParserType);
                }
                else if (i == 3 + version.Length + code.Length + reason.Length)
                {
                    Assert.AreEqual(HttpParserType.FieldKey, p.ParserType);
                }

                i += 1;
            }
        }
    }
}
