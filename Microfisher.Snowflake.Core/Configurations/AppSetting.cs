using System;
using System.Collections.Generic;

namespace Microfisher.Snowflake.Core.Configurations
{
    public class AppSetting
    {
        public string AppName { get; set; }
        public Jwt Jwt { get; set; }
        public Redis Redis { get; set; }
        public Database Database { get; set; }
        public RabbitMQ RabbitMQ { get; set; }
        public List<string> CORS { get; set; }
    }

    public class Jwt
    {
        public int Expired { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

    }

    public class Redis
    {
        public string ConnectionString { get; set; }
    }

    public class Database
    {
        public string ConnectionString { get; set; }
    }

    public class RabbitMQ
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

}