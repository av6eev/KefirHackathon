using System.Collections.Generic;
using Entities.Animation;
using Entities.Player.Animator;
using Entities.Specification;
using Reactive.Event;
using Reactive.Field;
using SceneManagement;
using SimpleJson;

namespace Entities.Player
{
    public class PlayerModel : EntityModel, IPlayerModel
    {
        public ReactiveEvent ChangeEvent { get; } = new();

        public const string Id = "Player";
        public const string HudId = "player_hud_inventory";
        public string SaveId => Id;
        public string BaseLocationId { get; private set; } = SceneConst.HubId;
        
        public int BaseAmnesiaValue { get; private set; }

        public bool IsRunning;
        public ReactiveField<bool> InDash { get; } = new();

        public PlayerModel(EntitySpecification entitySpecification) : base(entitySpecification)
        {
            Resources.Add(EntityResourceType.Essence, new EntityResource(EntityResourceType.Essence, 0));
            Resources.Add(EntityResourceType.Amnesia, new EntityResource(EntityResourceType.Amnesia, 0));
        }

        public IDictionary<string, object> GetSaveData()
        {
            var saveData = new Dictionary<string, object>();
            
            foreach (var resource in Resources.GetModels())
            {
                saveData.Add(resource.Type.ToString(), resource.Amount.Value);
            }
            
            saveData.Add("base_location", BaseLocationId);
            saveData.Add("base_amnesia_value", BaseAmnesiaValue);
            
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
    }
}