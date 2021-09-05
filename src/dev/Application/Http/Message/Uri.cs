using System;
using System.Collections.Generic;
using System.Text;

namespace Networks.Application.Http
{
    /// <summary>
    /// An object representation of a uniform resource identifier (URI)
    /// </summary>
    public class Uri
    {
        /// <summary>
        /// The absolute path of the URI
        /// </summary>
        public string AbsolutePath;

        /// <summary>
        /// The absolute URI
        /// </summary>
        public string AbsoluteUri;

        /// <summary>
        /// The host component of the instance
        /// e.g. for http://www.contoso.com:8080/shownew.htm?date=today
        /// the host is www.contoso.com
        /// </summary>
        public string Host;

        /// <summary>
        /// The port number of this URI
        /// It's 80 by default for HTTP protocol
        /// e.g. http://www.contoso.com:8080/shownew.htm?date=today
        /// has port number 8080
        /// while http://www.contoso.com/shownew.htm?date=today
        /// is having 80
        /// </summary>
        public int Port;

        /// <summary>
        /// The scheme for this URI in lowercase
        /// e.g. http://www.contoso.com:8080/shownew.htm?date=today
        /// is having scheme http
        /// https://www.contoso.com:8080/shownew.htm?date=today
        /// is having scheme https
        /// </summary>
        public string Scheme;

        /// <summary>
        /// constructor for string url input
        /// </summary>
        /// <param name="urlString">url string</param>
        public Uri(string urlString)
        {
        }
    }
}
