using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Core.Domain.Workspace
{
    public class Folder : EntityBase
    {
        private string _name;
        private string? _description;
        private List<Folder> _subfolders;
        private List<Guid> _projects;

        public Folder(string name, string? description = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            _subfolders = new List<Folder>();
            _projects = new List<Guid>();

            _name = name;
            _description = description;
        }




        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public string? Description
        {
            get => _description;
            private set => _description = value;
        }

        public List<Folder> Subfolders
        {
            get => _subfolders;
            private set => _subfolders = new List<Folder>(value);
        }

        public List<Guid> Projects
        {
            get => _projects;
            private set => _projects = new List<Guid>(value);
        }








        public void DeleteProject(Guid projectId)
        {
            Projects.Remove(projectId);
        }


        public Folder? DeleteNestedFolder(Guid folderId)
        {
            if (Id == folderId)
            {
                return this;
            }

            foreach (var subfolder in Subfolders)
            {
                var folder = Subfolders.FirstOrDefault(x => x.Id == folderId);
                if (folder != null)
                {
                    Subfolders.Remove(folder);
                    return folder;
                }
                else
                {
                    var res = subfolder.DeleteNestedFolder(folderId);
                    if (res != null)
                    {
                        return res;
                    }
                }
            }

            return null;
        }

        public List<Guid> FindProjectIds(Guid foderId)
        {
            var result = new List<Guid>();
            result.AddRange(Projects);

            foreach (var subfolder in Subfolders)
            {
                var ids = subfolder.FindProjectIds(foderId);
                result.AddRange(ids);
            }

            return result;
        }

        public Folder? Find(Guid id)
        {
            if (Id == id) return this;

            foreach (var subfolder in Subfolders)
            {
                var folder = subfolder.Find(id);
                if (folder != null) return folder;
            }

            return null;
        }

        public void PatchDescription(string name, string description)
        {
            _name = name;
            _description = description;

            ModifiedAt = DateTime.UtcNow;
        }
    }
}
