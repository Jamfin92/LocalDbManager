using MartinCostello.SqlLocalDb;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDbManager
{
    public class Instance
    {
        public string InstanceName { get; private set; }
        public string ConnectionString { get; private set; }
        public Instance(string instanceName)
        {
            InstanceName = instanceName;
            GetOrCreate(InstanceName);
        }

        private void GetOrCreate(string name)
        {
            using var localDB = new SqlLocalDbApi();

            ISqlLocalDbInstanceInfo instance = localDB.GetOrCreateInstance(name);
            ISqlLocalDbInstanceManager manager = instance.Manage();

            if (!instance.IsRunning)
            {
                manager.Start();
            }

            var connectionStringSet = !string.IsNullOrWhiteSpace(ConnectionString);
            if (!connectionStringSet)
            {
                ConnectionString = instance.GetConnectionString();
            }
        }
        
        private string GetConnectionString()
        {
            return ConnectionString;
        }


    }
}
