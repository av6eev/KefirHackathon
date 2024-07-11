using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ServerCore.Main.Property;
using ServerCore.Main.Utilities;
using ServerCore.Main.Utilities.Logger;

namespace ServerCore.Main
{
    public class ServerData : IServerData
    {
        public string Id { get; }
        public bool IsDirty { get; protected internal set; }
        public bool IsNew { get; private set; } = true;
        
        public readonly Dictionary<string, IProperty> Properties = new();
        public readonly Dictionary<string, IServerData> Dataset = new();

        public ServerData(string id)
        {
            Id = id;
        }
    
        public Dictionary<string, object> Read(Protocol protocol)
        {
            var result = new Dictionary<string, object>();
            var dataSet = new Dictionary<string, object>();
            var properties = new Dictionary<string, object>();
            
            protocol.Get(out string variant);

            ushort dataCount;
            ushort propertyCount;
            
            switch (variant)
            {
                case "P":
                    protocol.Get(out propertyCount);
                    properties.Add("count", propertyCount);
                    
                    for (var i = 0; i < propertyCount; i++)
                    {
                        protocol.Get(out string propertyName);
                        Properties[propertyName].SetFromProtocol(protocol, out var value);
                        
                        properties.Add(propertyName, value);
                    }
                    
                    result.Add(variant, properties);
                    break;
                case "D":
                    protocol.Get(out dataCount);
                    dataSet.Add("count", dataCount);
                    
                    for (var i = 0; i < dataCount; i++)
                    {
                        protocol.Get(out string dataName);
                        var readData = Dataset[dataName].Read(protocol);
                        
                        dataSet.Add(dataName, readData);
                    }
                    
                    result.Add(variant, dataSet);
                    break;
                case "DP":
                    protocol.Get(out dataCount);
                    dataSet.Add("count", dataCount);

                    for (var i = 0; i < dataCount; i++)
                    {
                        protocol.Get(out string dataName);
                        var readData = Dataset[dataName].Read(protocol);
                        
                        dataSet.Add(dataName, readData);
                    }
                
                    result.Add("D", dataSet);
                    
                    protocol.Get(out propertyCount);
                    properties.Add("count", propertyCount);
                    
                    for (var i = 0; i < propertyCount; i++)
                    {
                        protocol.Get(out string propertyName);
                        Properties[propertyName].SetFromProtocol(protocol, out var value);
                        
                        properties.Add(propertyName, value);
                    }
                    
                    result.Add("P", properties);
                    break;
            }
            
            return result;
        }

        public bool Write(Protocol protocol)
        {
            ushort changedDataCount = 0;
            var changedDataset = new List<IServerData>();
        
            foreach (var data in Dataset)
            {
                if (data.Value.HasChanges())
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

                IsDirty = false;
                
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
            
                IsDirty = false;
                
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

                IsDirty = false;
                
                return true;
            }

            return false;
        }

        public void WriteAll(Protocol protocol)
        {
            IsDirty = false;
            
            var changedDataCount = (ushort)Dataset.Count;
            var changedDataset = Dataset.Select(data => data.Value).ToList();

            var changedPropertyCount = (ushort)Properties.Count;
            var changedProperties = Properties.Select(property => property.Value).ToList();
            
            if (changedDataCount > 0 && changedPropertyCount > 0)
            {
                protocol.Add("DP");
                protocol.Add(changedDataCount);
            
                foreach (var data in changedDataset)
                {
                    protocol.Add(data.Id);
                    // Console.WriteLine(data.Id);
                    data.WriteAll(protocol);
                }
            
                protocol.Add(changedPropertyCount);

                foreach (var property in changedProperties)
                {
                    protocol.Add(property.Id);
                    // Console.WriteLine(property.Id);
                    property.GetForProtocol(protocol);
                }
            }
            else if (changedDataCount > 0)
            {
                protocol.Add("D");            
                protocol.Add(changedDataCount);

                foreach (var data in changedDataset)
                {
                    protocol.Add(data.Id);
                    // Console.WriteLine(data.Id);
                    data.WriteAll(protocol);
                }
            }
            else if (changedPropertyCount > 0)
            {
                protocol.Add("P");
                protocol.Add(changedPropertyCount);

                foreach (var property in changedProperties)
                {
                    protocol.Add(property.Id);
                    // Console.WriteLine(property.Id);
                    property.GetForProtocol(protocol);
                }
            }
        }

        public bool HasChanges()
        {
            foreach (var property in Properties)
            {
                if (property.Value.IsChanged)
                {
                    return true;
                }
            }

            foreach (var data in Dataset)
            {
                if (data.Value.HasChanges())
                {
                    return true;
                }
            }
            
            return false;
        }

        public void ChangeIsNew(bool state)
        {
            IsNew = state;
        }
    }
}