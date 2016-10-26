﻿using OpenData.Data.Core;
using OpenData.Framework.Core.Entity;
namespace OpenData.Framework.Core
{
    public interface IMemberService
    {
        void CreateAccount(string email, string phoneNumber, DynamicEntity data);
        void Import(System.Data.DataSet ds, Site site);
        //Bzway.Business.Model.MemberSearchViewModel SearchMember(Bzway.Business.Model.MemberSearchViewModel model);
    }
}
