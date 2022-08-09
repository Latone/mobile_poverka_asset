using System;
using System.Collections.Generic;
using System.Text;

namespace mobile_poverka_asset.Models
{
    public class ConnectionProfile
    {
        public string Id { get; set; }
        public string ProfileName { get; set; } = "default";
        public string Server { get; set; } = "";
        public string Port { get; set; } = "";
        public string Database { get; set; } = "";
        public string UserId { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
