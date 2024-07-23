using ServerCore.Main.Party;

namespace Server.Party.Collection;

public class PartiesCollection
{
    public event Action<PartyModel> OnCreated; 
    public event Action<PartyModel> OnRemoved; 
    
    private readonly Dictionary<string, PartyModel> _parties = new();

    public PartyModel Create(string ownerId, string ownerNickname)
    {
        var partyData = new PartyModel(Guid.NewGuid().ToString(), ownerId, ownerNickname);
        
        _parties.Add(partyData.Guid, partyData);
        OnCreated?.Invoke(partyData);
        
        return partyData;
    }

    public void Remove(string partyId)
    {
        if (!_parties.TryGetValue(partyId, out var party)) return;
        
        _parties.Remove(party.Guid);
        OnRemoved?.Invoke(party);
    }

    public bool TryGetParty(string partyId, out PartyModel party)
    {
        if (_parties.TryGetValue(partyId, out var model))
        {
            party = model;
            return true;
        }

        party = null;
        return false;
    }
}