﻿using Bzway.Data.Core;
using Bzway.Framework.Connect.Entity;

namespace Bzway.Module.MemberClub
{
    public interface IMemberService
    {
        void CreateAccount(string email, string phoneNumber, DynamicEntity data);
        void CreateUserCard(UserCard entity);
        void DeleteCard(string id);
        UserCard FindoCardByID(string id);
        void UpdateCard(UserCard model);
    }
}