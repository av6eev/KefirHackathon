﻿using System;
using ServerCore.Main.Friends;
using ServerCore.Main.Party;
using ServerCore.Main.Property;

namespace ServerCore.Main.Users
{
    public class UserData : ServerData
    {
        public readonly Property<string> PlayerId = new("id", string.Empty);
        public readonly Property<string> PlayerNickname = new("nickname", string.Empty);
        public readonly Property<string> CurrentLocationId = new("location_id", string.Empty);
        public readonly Property<bool> IsTimeout = new("is_timeout", false);

        public readonly DataCollection<string, PartyInviteData> PartyInvites = new("party_invites");
        public readonly DataCollection<string, FriendInviteData> FriendInvites = new("friend_invites");

        public readonly PartyData PartyData = new();
        public readonly FriendsData FriendsData = new("friends");
        
        public UserData() : base("user_data")
        {
            Properties.Add(PlayerId.Id, PlayerId);
            Properties.Add(PlayerNickname.Id, PlayerNickname);
            Properties.Add(CurrentLocationId.Id, CurrentLocationId);
            Properties.Add(IsTimeout.Id, IsTimeout);
            
            Dataset.Add(PartyInvites.Id, PartyInvites);
            Dataset.Add(PartyData.Id, PartyData);
            Dataset.Add(FriendInvites.Id, FriendInvites);
            Dataset.Add(FriendsData.Id, FriendsData);
        }
    }
}