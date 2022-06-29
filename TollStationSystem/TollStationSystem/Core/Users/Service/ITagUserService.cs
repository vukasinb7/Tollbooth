using System.Collections.Generic;
using TollStationSystem.Core.Users.Model;

namespace TollStationSystem.Core.Users.Service
{
    public interface ITagUserService
    {
        List<TagUser> TagUsers { get; }

        void Add(TagUser tagUser);

        TagUser FindByJmbg(string jmbg);

        void Load();

        void Serialize();
    }
}