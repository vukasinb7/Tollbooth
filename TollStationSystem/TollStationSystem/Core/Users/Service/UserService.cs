using System.Collections.Generic;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.Core.Users.Repository;

namespace TollStationSystem.Core.Users.Service
{
    public class UserService : IUserService
    {
        IUserRepo userRepo;
        IBossService bossService;

        public UserService(IUserRepo userRepo, IBossService bossService)
        {
            this.userRepo = userRepo;
            this.bossService = bossService;
        }

        public List<User> Users { get => userRepo.Users; }

        public void Add(User user)
        {
            userRepo.Add(user);
        }

        public User FindByJmbg(string jmbg)
        {
            return userRepo.FindByJmbg(jmbg);
        }

        public void Load()
        {
            userRepo.Load();
        }

        public void Serialize()
        {
            userRepo.Serialize();
        }

    }

}
