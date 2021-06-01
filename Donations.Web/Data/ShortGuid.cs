using System;

namespace Donations.Web.Data
{
    /* A shorter and URL Friendly GUID
     * It basically just converts a GUID into a base64 string and shortens it a bit.
     * https://www.madskristensen.net/blog/A-shorter-and-URL-friendly-GUID
     */
    public static class GuidEncoder
    {
        public static string Encode(Guid guid, int length = 22)
        {
            string enc = Convert.ToBase64String(guid.ToByteArray());
            enc = enc.Replace("/", "_");
            enc = enc.Replace("+", "-");
            return enc.Substring(0, length);
        }
    }
}
