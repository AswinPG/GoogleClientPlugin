using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.Maui.GoogleClient
{
    public class GoogleUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
}
