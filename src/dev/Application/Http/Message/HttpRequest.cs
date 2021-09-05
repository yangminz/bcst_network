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
        public byte[] Body { get; private set; }

        /// <summary>
        /// The length of content sent by the client in bytes
        /// </summary>
        public int ContentLength { get; private set; }

        /// <summary>
        /// Headers of Http Request
        /// </summary>
        public NameValueCollection Headers { get; private set; }

        /// <summary>
        /// The Http data transfer method (such as GET, POST, or HEAD)
        /// </summary>
        public HttpMethodType HttpMethod { get; private set; }

        /// <summary>
        /// The url of the current request
        /// </summary>
        public Uri Url { get; private set; }

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
        public string Scheme
        {
            get
            {
                return this.Url?.Scheme;
            }
        }

        /// <summary>
        /// The buffer for receving bytes
        /// </summary>
        private Queue<char> buffer;

        /// <summary>
        /// The parser
        /// </summary>
        private IHttpParser parser = HttpParser.Instance.Start;

        /// <summary>
        /// The buffer for header key
        /// </summary>
        private string headerKeyBuffer;

        /// <summary>
        /// The number of body bytes have been received
        /// </summary>
        private int receivedBodyBytes;

        /// <summary>
        /// Constructor for receiving bytes from stream
        /// </summary>
        public HttpRequest()
        {
            this.Body = null;
            this.Headers = new NameValueCollection(); // TODO: ignore case
            this.Url = null;
            this.ContentLength = 0;

            this.buffer = new Queue<char>();
            this.receivedBodyBytes = 0;
        }

        /// <summary>
        /// Receive a byte and try parse it
        /// </summary>
        /// <param name="b">The byte</param>
        /// <returns>True if this byte can be accepted</returns>
        public bool Receive(byte b)
        {
            HttpParserType oldType = this.parser.ParserType;

            if (this.receivedBodyBytes < this.ContentLength)
            {
                char c = (char)b;
                try
                {
                    this.parser = this.parser.StreamParse(c);
                }
                catch (HttpParsingException ex)
                {
                    // TODO: notificate the bad request
                    return false;
                }

                // parsing is successful, no exception is thrown
                HttpParserType newType = this.parser.ParserType;

                switch (oldType)
                {
                    // StartLineParser
                    // StartLineHParser
                    // StartLineCRParser
                    case HttpParserType.StartLine:
                        switch (newType)
                        {
                            case HttpParserType.StartLine:
                            // 'H'
                            case HttpParserType.HttpMethod:
                                // method
                                this.buffer.Enqueue(c);
                                break;
                            case HttpParserType.FieldKey:
                                // CRLF
                                break;
                            default:
                                // maybe a version from StartLineHParser
                                throw new HttpParsingException("Http request parsing request line error");
                        }
                        break;
                    case HttpParserType.HttpMethod:
                        switch (newType)
                        {
                            case HttpParserType.HttpMethod:
                                // from method to method
                                this.buffer.Enqueue(c);
                                break;
                            case HttpParserType.Url:
                                // space from method to url
                                // end of parsing method
                                // set method
                                string methodStr = new string(this.buffer.ToArray());
                                this.HttpMethod = HttpConstants.HttpMethodMap[methodStr];
                                // reset buffer
                                this.buffer.Clear();
                                break;
                            default:
                                throw new HttpParsingException("Http request parsing http method error");
                        }
                        break;
                    case HttpParserType.Url:
                        switch (newType)
                        {
                            case HttpParserType.Url:
                                // from url to url
                                this.buffer.Enqueue(c);
                                break;
                            case HttpParserType.HttpVersion:
                                // space from url to version
                                // end of parsing url
                                string urlStr = new string(this.buffer.ToArray());
                                this.Url = new Uri(urlStr);
                                // reset buffer
                                this.buffer.Clear();
                                break;
                            default:
                                throw new HttpParsingException("Http request parsing url error");
                        }
                        break;
                    case HttpParserType.HttpVersion:
                        switch (newType)
                        {
                            case HttpParserType.HttpVersion:
                                // from version to version
                                this.buffer.Enqueue(c);
                                break;
                            case HttpParserType.StartLine:
                                // '\r' from version to end
                                // end of parsing version
                                string versionStr = new string(this.buffer.ToArray());
                                // TODO: we are matching the version http/1.1 directly
                                // make it more OO
                                if (HttpConstants.HttpVersionMatch(versionStr) == false)
                                {
                                    throw new HttpParsingException("Http request parsing version not match");
                                }
                                else
                                {
                                    // version matches
                                    break;
                                }
                            default:
                                throw new HttpParsingException("Http request parsing version error");
                        }
                        break;
                    case HttpParserType.FieldKey:
                        switch (newType)
                        {
                            case HttpParserType.FieldKey:
                                // from key to key
                                this.buffer.Enqueue(c);
                                break;
                            case HttpParserType.FieldValue:
                                // ':' from key to value
                                // end of processing key
                                string keyStr = new string(this.buffer.ToArray());
                                // check if key is already in headers
                                if (this.Headers[keyStr] != null)
                                {
                                    // TODO: think about if this is a bad request
                                }
                                else
                                {
                                    // be ready to accept the value
                                    this.headerKeyBuffer = keyStr;
                                }
                                // reset the buffer
                                this.buffer.Clear();
                                break;
                            default:
                                throw new HttpParsingException("Http request parsing header key error");
                        }
                        break;
                    case HttpParserType.FieldValue:
                        switch (newType)
                        {
                            case HttpParserType.FieldValue:
                                // from value to value
                                this.buffer.Enqueue(c);
                                break;
                            case HttpParserType.HeaderLine:
                                // '\r' end of this header key-value pair
                                string valueStr = new string(this.buffer.ToArray());
                                this.Headers.Add(this.headerKeyBuffer, valueStr);
                                // reset buffer
                                this.buffer.Clear();
                                this.headerKeyBuffer = null;
                                break;
                            default:
                                throw new HttpParsingException("Http request parsing header value error");
                        }
                        break;
                    case HttpParserType.HeaderLine:
                        switch (newType)
                        {
                            case HttpParserType.HeaderLine:
                                // from header line to header line
                                // processing the CRLFCRLF
                                break;
                            case HttpParserType.Body:
                                // the end of parsing characters
                                // check the content length
                                if (this.Headers != null)
                                {
                                    if (this.Headers[HttpConstants.ContentLength] != null)
                                    {
                                        // try parsing the integer
                                        string contentLengthStr = this.Headers[HttpConstants.ContentLength];
                                        int contentLength = 0;
                                        if (int.TryParse(contentLengthStr, out contentLength) == false)
                                        {
                                            // cannot convert the string to content length
                                            // reset it to 0
                                            this.ContentLength = 0;
                                            // TODO: is it a bad request?
                                        }
                                        else
                                        {
                                            // succesfully converted
                                            if (0 <= contentLength && contentLength <= HttpConstants.MaxContentLength)
                                            {
                                                this.ContentLength = contentLength;
                                            }
                                            else
                                            {
                                                this.ContentLength = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // no content length from the request
                                        // reset it to 0
                                        this.ContentLength = 0;
                                    }
                                }
                                else
                                {
                                    // no headers
                                    // TODO: is it possible?
                                    this.ContentLength = 0;
                                }

                                if (this.ContentLength != 0)
                                {
                                    this.Body = new byte[this.ContentLength];
                                }
                                else
                                {
                                    this.Body = null;
                                }

                                break;
                            default:
                                throw new HttpParsingException("Http request header lines ending error");
                        }
                        break;
                    default:
                        throw new HttpParsingException("Http request parsing header value error");
                }
            }
            else
            {
                // body will receive this byte
                this.Body[this.receivedBodyBytes] = b;
                this.receivedBodyBytes += 1;
            }

            return true;
        }
    }
}
