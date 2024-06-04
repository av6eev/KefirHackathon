using ServerCore.Main.Party;

namespace Server.Party.Collection;

public class PartiesCollection
{
    public event Action<PartyModel> OnCreated; 
    public event Action<PartyModel> OnRemoved; 
    
    private readonly Dictionary<string, PartyModel> _parties = new();

    public PartyModel Create(string ownerId)
    {
        var newParty = new PartyModel(Guid.NewGuid().ToString(), ownerId);
        
        _parties.Add(newParty.PartyData.Id, newParty);
        OnCreated?.Invoke(newParty);
        
        return newParty;
    }

    public void Remove(string partyId)
    {
        if (!_parties.TryGetValue(partyId, out var party)) return;
        
        _parties.Remove(party.PartyData.Id);
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