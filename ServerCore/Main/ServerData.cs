using System;
using System.Collections.Generic;
using System.Linq;
using ServerCore.Main.Property;
using ServerCore.Main.Utilities;

namespace ServerCore.Main
{
    public class ServerData : IServerData
    {
        public string Id { get; }
        public Dictionary<string, IProperty> Properties { get; } = new();
        public Dictionary<string, IServerData> Dataset = new();

        public ServerData(string id)
        {
            Id = id;
        }
    
        public void Read(Protocol protocol)
        {
            protocol.Get(out string variant);

            ushort dataCount;
            ushort propertyCount;
        
            switch (variant)
            {
                case "P":
                    protocol.Get(out propertyCount);
                
                    for (var i = 0; i < propertyCount; i++)
                    {
                        protocol.Get(out string propertyName);
                        Properties[propertyName].SetFromProtocol(protocol);
                    }
                    break;
                case "D":
                    protocol.Get(out dataCount);
                
                    for (var i = 0; i < dataCount; i++)
                    {
                        protocol.Get(out string dataName);
                        Dataset[dataName].Read(protocol);
                    }
                    break;
                case "DP":
                    protocol.Get(out dataCount);
                
                    for (var i = 0; i < dataCount; i++)
                    {
                        protocol.Get(out string dataName);
                        Dataset[dataName].Read(protocol);
                    }
                
                    protocol.Get(out propertyCount);
                
                    for (var i = 0; i < propertyCount; i++)
                    {
                        protocol.Get(out string propertyName);
                        Properties[propertyName].SetFromProtocol(protocol);
                    }
                    break;
            }
        }

        public bool Write(Protocol protocol)
        {
            ushort changedDataCount = 0;
            var changedDataset = new List<IServerData>();
        
            foreach (var data in Dataset)
            {
                if (data.Value.Properties.Any(property => property.Value.IsChanged))
                {
                    changedDataCount++;
                    changedDataset.Add(data.Value);
                }
            }

            ushort changedPropertyCount = 0;
            var changedProperties = new List<IProperty>();
        
            foreach (var property in Properties.Where(property => property.Value.IsChanged))
            {
                changedPropertyCount++;
                changedProperties.Add(property.Value);
            }

            if (changedDataCount > 0 && changedPropertyCount > 0)
            {
                protocol.Add("DP");
                protocol.Add(changedDataCount);
            
                foreach (var data in changedDataset)
                {
                    protocol.Add(data.Id);
                    data.Write(protocol);
                }
            
                protocol.Add(changedPropertyCount);

                foreach (var property in changedProperties)
                {
                    protocol.Add(property.Id);
                    property.GetForProtocol(protocol);
                }
            
                return true;
            }
            else if (changedDataCount > 0)
            {
                protocol.Add("D");            
                // Console.WriteLine("Add D");
                protocol.Add(changedDataCount);
                // Console.WriteLine($"D count: {changedDataCount}");

                foreach (var data in changedDataset)
                {
                    protocol.Add(data.Id);
                    // Console.WriteLine($"Add {data.Id}");
                    data.Write(protocol);
                }
            
                return true;
            }
            else if (changedPropertyCount > 0)
            {
                protocol.Add("P");
                // Console.WriteLine("Add P");
                protocol.Add(changedPropertyCount);
                // Console.WriteLine($"P count: {changedPropertyCount}");

                foreach (var property in changedProperties)
                {
                    protocol.Add(property.Id);
                    // Console.WriteLine($"Add {property.Id}");
                    property.GetForProtocol(protocol);
                }

                return true;
            }

            return false;
        }
    }
}