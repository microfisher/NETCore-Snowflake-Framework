using System;
using System.Collections.Generic;
using System.Net;

namespace Microfisher.Snowflake.Core.Utilities
{
    public static class EmailHelper
    {
        public static string ApiKey = "";
        public static string Domain = "";

        //public bool Send(string _subject, string _from, string _to, string _nickName, string _content, bool _ishtml, out string _result)
        //{
        //    _result = "";
        //    ServicePointManager.Expect100Continue = false;
        //    CredentialCache _credentialCache = new CredentialCache();
        //    _credentialCache.Add(new Uri("https://api.mailgun.net/v3"), "Basic", new NetworkCredential("api", ApiKey));
        //    Dictionary<string, string> _headers = new Dictionary<string, string>();
        //    _headers.Add("key", ApiKey);
        //    Dictionary<string, string> _formdata = new Dictionary<string, string>();
        //    _formdata.Add("domain", Domain);
        //    _formdata.Add("from", _from);
        //    _formdata.Add("to", $"{_nickName} <{_to}>");
        //    _formdata.Add("subject", _subject);
        //    if (!_ishtml)
        //        _formdata.Add("text", _content);
        //    else
        //        _formdata.Add("html", _content);
        //    if (Lion.Net.HttpClient.PostAsFormData($"https://api.mailgun.net/v3/{Domain}/messages", _headers, _formdata, out _result, _credentialCache, 120 * 1000))
        //    {
        //        return !string.IsNullOrWhiteSpace(JObject.Parse(_result)["id"].ToString());
        //    }
        //    return false;
        //}
    }
}
