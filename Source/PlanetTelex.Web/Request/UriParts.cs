/**
 * Copyright (c) 2012 Planet Telex Inc. all rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace PlanetTelex.Web.Request
{
    /// <summary>
    /// A URL that has been parsed it into many discrete properties for easy analysis.
    /// </summary>
    public class UriParts
    {
        /// <summary>
        /// localhost
        /// </summary>
        public const string LOCALHOST = "localhost";

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UriParts"/> class with the Current.Request.RawUrl.
        /// </summary>
        public UriParts()
        {
            _rawString = HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.RawUrl.TrimEnd('/');
            UriScheme uriScheme = new UriScheme(_rawString);
            _scheme = uriScheme.SchemeType;
            _uriWithoutScheme = _rawString.Replace(uriScheme.Prefix, string.Empty);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriParts"/> class with the provided URI.
        /// </summary>
        /// <param name="uriString">The URI string.</param>
        public UriParts(string uriString)
        {
            _rawString = uriString;
            UriScheme uriScheme = new UriScheme(_rawString);
            _scheme = uriScheme.SchemeType;
            _uriWithoutScheme = _rawString.Replace(uriScheme.Prefix, string.Empty);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current URI parts.
        /// </summary>
        public static UriParts Current
        {
            get
            {
                return new UriParts();
            }
        }

        /// <summary>
        /// Gets the URI scheme.
        /// </summary>
        public UriSchemeType Scheme
        {
            get
            {
                return _scheme;
            }
        }
        private readonly UriSchemeType _scheme;

        /// <summary>
        /// Gets the URI without the scheme part.
        /// </summary>
        public string UriWithoutScheme
        {
            get
            {
                return _uriWithoutScheme;
            }
        }
        private readonly string _uriWithoutScheme;

        /// <summary>
        /// Gets the URI without scheme and query parts.
        /// </summary>
        public string UriWithoutSchemeAndQuery
        {
            get
            {
                return (_uriWithoutScheme.IndexOf("?", System.StringComparison.Ordinal) > -1) ? _uriWithoutScheme.Substring(0, _uriWithoutScheme.IndexOf("?", System.StringComparison.Ordinal)) : _uriWithoutScheme;
            }
        }

        /// <summary>
        /// Get the path part of the URI, which includes the page. No domain or query part.
        /// </summary>
        public string UriPath
        {
            get
            {
                string urlPath = UriWithoutSchemeAndQuery.IndexOf('/') >= 0 ? UriWithoutSchemeAndQuery.Substring(UriWithoutSchemeAndQuery.IndexOf('/')) : string.Empty;
                return IsLocalhost ? urlPath.Substring(urlPath.IndexOf('/', 2) >=0 ? urlPath.IndexOf('/', 2) : 0) : urlPath;
            }
        }

        /// <summary>
        /// Get the path part of the URI without the page. No domain or or query part.
        /// </summary>
        public string UriPathWithoutPage
        {
            get
            {
                return UriPath.Substring(0, UriPath.LastIndexOf('/') + 1);
            }
        }

        /// <summary>
        /// Gets all the nodes (delimited by the '/' character) in the path.
        /// </summary>
        public string[] Nodes
        {
            get
            {
                if (_nodes == null)
                    _nodes = UriPath.Split('/');

                return _nodes;
            }
        }
        private string[] _nodes;

        /// <summary>
        /// Gets a value indicating whether this URI's domain is localhost.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if the first url node is "localhost"; otherwise, <c>false</c>.
        /// </value>
        public bool IsLocalhost
        {
            get
            {
                return Nodes[0].ToLower().Contains(LOCALHOST);
            }
        }

        /// <summary>
        /// Gets the index of first node that can be considered a directory (is not "localhost").
        /// </summary>
        /// <value>
        /// The index of first node that can be considered a directory.
        /// </value>
        public int FirstDirectoryNodeIndex
        {
            get
            {
                int nodeIndex = (IsLocalhost) ? 2 : 1;
                if (Nodes.Length > nodeIndex)
                    return (Nodes[nodeIndex].IndexOf(".", System.StringComparison.Ordinal) > -1) ? -1 : nodeIndex;
                
                return -1;
            }
        }

        /// <summary>
        /// Gets the first node that can be considered a directory (is not "localhost").
        /// </summary>
        public string FirstDirectory
        {
            get
            {
                return (FirstDirectoryNodeIndex > -1) ? Nodes[FirstDirectoryNodeIndex] : string.Empty;
            }
        }

        /// <summary>
        /// Gets the second node that can be concidered a directory (is not "localhost").
        /// </summary>
        public string SecondDirectory
        {
            get
            {
                int nodeIndex = FirstDirectoryNodeIndex + 1;
                if (Nodes.Length > nodeIndex && Nodes[nodeIndex].IndexOf(".", System.StringComparison.Ordinal) == -1)
                    return Nodes[nodeIndex];

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the index of the last node that can be considered a directory (is not a page).
        /// </summary>
        /// <value>
        /// The index of the last node that can be considered a directory.
        /// </value>
        public int LastDirectoryNodeIndex
        {
            get
            {
                int returnVal = -1;
                if (FirstDirectoryNodeIndex > -1)
                {
                    if (Nodes[Nodes.Length - 1].IndexOf(".", System.StringComparison.Ordinal) > -1)
                        returnVal = Nodes.Length - 2;
                    else
                        returnVal = Nodes.Length - 1;
                }
                return (returnVal < FirstDirectoryNodeIndex) ? FirstDirectoryNodeIndex : returnVal;
            }
        }

        /// <summary>
        /// Gets the last node that can be considered a directory (is not a page).
        /// </summary>
        public string LastDirectory
        {
            get
            {
                return (LastDirectoryNodeIndex > -1) ? Nodes[LastDirectoryNodeIndex] : string.Empty;
            }
        }

        /// <summary>
        /// Gets the domain of the current request.  If localhost, this value will include the virtual directory.
        /// </summary>
        public string Domain
        {
            get
            {
                return IsLocalhost ? Nodes[0] + "/" + Nodes[1] : Nodes[0];
            }
        }

        /// <summary>
        /// Gets the correct relative root url element, taking into account if this is running under "localhost".
        /// </summary>
        public string RelativeRoot
        {
            get
            {
                return IsLocalhost ? "/" + Nodes[1] + "/" : "/";
            }
        }

        /// <summary>
        /// Gets the query string of the request.
        /// </summary>
        public string QueryPart
        {
            get
            {
                return (_uriWithoutScheme.IndexOf("?", System.StringComparison.Ordinal) > -1) ? _uriWithoutScheme.Substring(_uriWithoutScheme.IndexOf("?", System.StringComparison.Ordinal) + 1) : string.Empty;
            }
        }

        /// <summary>
        /// Gets a name/value dictionary of parameters from the query part.
        /// </summary>
        public Dictionary<string, string> Parameters
        {
            get
            {
                if (_parameters == null && QueryPart.Length > 0)
                {
                    _parameters = new Dictionary<string, string>();
                    foreach (string nameValuePair in QueryPart.Split("&".ToCharArray()))
                    {
                        if (nameValuePair.IndexOf("=", System.StringComparison.Ordinal) > -1)
                        {
                            string[] split = nameValuePair.Split("=".ToCharArray());
                            if (!_parameters.ContainsKey(split[0].ToLower()) && split.Length == 2)
                                _parameters.Add(split[0].ToLower(), split[1]);
                        }
                    }
                }
                return _parameters;
            }
        }
        private Dictionary<string, string> _parameters;

        /// <summary>
        /// Gets the requested page including its file extension.
        /// </summary>
        public string Page
        {
            get
            {
                return Path.GetFileName(UriWithoutSchemeAndQuery);
            }
        }

        /// <summary>
        /// Gets the requested page without its file extension.
        /// </summary>
        public string PageWithoutExtension
        {
            get
            {
                return Path.GetFileNameWithoutExtension(UriWithoutSchemeAndQuery);
            }
        }

        /// <summary>
        /// Gets the requested page file extension.
        /// </summary>
        public string PageExtension
        {
            get
            {
                // ReSharper disable PossibleNullReferenceException
                return Path.GetExtension(UriWithoutSchemeAndQuery).ToLower();
                // ReSharper restore PossibleNullReferenceException
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return _rawString;
        }
        private readonly string _rawString;

        /// <summary>
        /// Gets the path from the specified start index onward.
        /// </summary>
        /// <param name="startIndex">The node index to start at.</param>
        /// <returns>A string.</returns>
        public string GetUriPathFromIndex(int startIndex)
        {
            string[] newNodes = new string[Nodes.Length - startIndex];
            System.Array.Copy(Nodes, startIndex, newNodes, 0, Nodes.Length - startIndex);
            return string.Join("/", newNodes);
        }

        /// <summary>
        /// Gets the node at the given index, discouting "localhost" from the considered nodes.
        /// </summary>
        /// <param name="nodeIndex">The node index.</param>
        /// <returns>A string.</returns>
        public string GetUriPathNodeAt(int nodeIndex)
        {
            if (FirstDirectoryNodeIndex != -1 && nodeIndex <= LastDirectoryNodeIndex)
                return IsLocalhost ? Nodes[nodeIndex+1] : Nodes[nodeIndex];

            return string.Empty;
        }

        #endregion
    }
}