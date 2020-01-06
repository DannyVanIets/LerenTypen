using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LerenTypen.UnitTests
{
    class Database
    { 
        public static void Connect()
        {
            SshClient client = new SshClient("145.44.233.184", "student", "toor2019");
            try
            {
                client.Connect();
                var port = new ForwardedPortLocal("127.0.0.1", 1433, "localhost", 1433);
                client.AddForwardedPort(port);
                port.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
