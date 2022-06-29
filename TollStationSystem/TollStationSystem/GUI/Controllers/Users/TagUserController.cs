using System.Collections.Generic;
using TollStationSystem.Core.Users.Model;
using TollStationSystem.Core.Users.Service;

namespace TollStationSystem.GUI.Controller.Users
{
    public class TagUserController
    {
        ITagUserService tagUserService;

        public TagUserController(ITagUserService tagUserService)
        {
            this.tagUserService = tagUserService;
        }

        public List<TagUser> TagUsers { get => tagUserService.TagUsers; }

        public void Add(TagUser tagUser)
        {
            tagUserService.Add(tagUser);
        }

        public TagUser FindByJmbg(string jmbg)
        {
            return tagUserService.FindByJmbg(jmbg);
        }

        public void Load()
        {
            tagUserService.Load();
        }

        public void Serialize()
        {
            tagUserService.Serialize();
        }
    }
}
