using System;
using System.Data.Common;
using System.Data.Entity;

namespace SzoftArchHazi.Data;

internal class Program {

    private static void Main(string[] args)
    {
        DbProviderFactories.RegisterFactory("System.Data.SqlClient", System.Data.SqlClient.SqlClientFactory.Instance);

        Console.WriteLine("Demo completed");
    }

}