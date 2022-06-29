using System.Collections.Generic;
using TollStationSystem.Core.Users.Model;

namespace TollStationSystem.Core.Users.Service
{
    public interface IUserService
    {
        List<User> Users { get; }

        void Add(User user);

        User FindByJmbg(string jmbg);

        void Load();

        void Serialize();
    }
}