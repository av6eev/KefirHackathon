using System.Collections.Generic;
using Entities.Specification;
using Reactive.Event;
using Reactive.Field;
using SceneManagement;
using ServerCore.Main.Utilities;
using ServerManagement.Test;

namespace Entities.Player
{
    public class PlayerModel : EntityModel, IPlayerModel
    {
        public ReactiveEvent ChangeEvent { get; } = new();
        public ReactiveEvent DeathEvent { get; } = new();

        public const string ConstId = "Player";
        public const string HudId = "player_hud_inventory";
        public string SaveId => ConstId;
        public string Id { get; set; }
        public string BaseLocationId { get; } = SceneConst.HubId;
        
        public bool IsRunning;
        public bool IsInputInverse { get; private set; }

        public ReactiveField<float> AfkTime { get; } = new(0);
        public ReactiveField<bool> IsAfk { get; } = new(false);
        public ReactiveField<bool> InDash { get; } = new();
        public ReactiveField<int> KillCount { get; } = new();
        
        public PlayerModel(EntitySpecification entitySpecification) : base(entitySpecification)
        {
            Resources.Add(EntityResourceType.Essence, new EntityResource(EntityResourceType.Essence, 0));
            Resources.Add(EntityResourceType.Amnesia, new EntityResource(EntityResourceType.Amnesia, 10, 100, 10));
        }

        public IDictionary<string, object> GetSaveData()
        {
            var saveData = new Dictionary<string, object>();
            
            foreach (var resource in Resources.GetModels())
            {
                saveData.Add(resource.Type.ToString(), resource.Amount.Value);
            }
            
            saveData.Add("base_location", BaseLocationId);
            
            return saveData;
        }

        public void SetSaveData(IDictionary<string, object> node)
        {
            // Resources.GetModel(EntityResourceType.Essence).Amount.Value = node.GetInt(EntityResourceType.Essence.ToString());
            // Resources.GetModel(EntityResourceType.Amnesia).Amount.Value = node.GetInt(EntityResourceType.Amnesia.ToString());
            // Resources.GetModel(EntityResourceType.Health).Amount.Value = node.GetInt(EntityResourceType.Health.ToString());
            
            // BaseLocationId = node.GetString("base_location");
            // BaseAmnesiaValue = node.GetInt("base_amnesia_value");
        }

        public void Death()
        {
            DeathEvent.Invoke();   
        }

        public void InverseInput(bool state)
        {
            IsInputInverse = state;
        }
    }
}