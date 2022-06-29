using System.Collections.Generic;
using TollStationSystem.Core.Users.Model;

namespace TollStationSystem.Core.Users.Repository
{
    public interface ITagUserRepo
    {
        List<TagUser> TagUsers { get; }

        void Add(TagUser tagUser);

        TagUser FindByJmbg(string jmbg);

        void Load();

        void Serialize();
    }
}