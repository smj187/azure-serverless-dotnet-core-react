using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Core.Domain.Workspace
{
    public class Workspace : AggregateBase
    {
        private Guid _userId;
        private List<Folder> _subfolders;
        private List<Guid> _projects;
        private string _name;
        private WorkspaceType _workspaceType;

        public Workspace(Guid userId, string name, WorkspaceType workspaceType)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            _userId = userId;
            _subfolders = new List<Folder>();
            _projects = new List<Guid>();

            _name = name;
            _workspaceType = workspaceType;
        }



        public Guid UserId
        {
            get => _userId;
            private set => _userId = value;
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

        public string Name
        {
            get => _name;
            private set => _name = value;
        }

        public WorkspaceType WorkspaceType
        {
            get => _workspaceType;
            private set => _workspaceType = value;
        }




        public void PatchName(string name)
        {
            _name = name;
        }


        public IEnumerable<Folder> GetSubfolders(IEnumerable<Folder> list)
        {
            foreach (var folder in list)
            {
                yield return folder;
                foreach (var subfolder in GetSubfolders(folder.Subfolders))
                {
                    yield return subfolder;
                }
            }
        }

        public List<Guid> FindAllProjectIds()
        {
            var projectIds = new List<Guid>();
            projectIds.AddRange(_projects);
            foreach (var folder in _subfolders)
            {
                projectIds.AddRange(folder.FindProjectIds(folder.Id));
            }

            return projectIds;
        }

        public void AddWorkspaceProject(Guid projectId)
        {
            _projects.Add(projectId);
        }

        public void RemoveWorkspaceProject(Guid projectId)
        {
            _projects.Remove(projectId);
        }

        public Folder? FindWorkspaceSubfolder(Guid id)
        {

            foreach (var subfolder in _subfolders)
            {
                var res = subfolder.Find(id);
                if (res != null) return res;
            }

            return null;
        }

        public List<Guid> DeleteFolderContent(Guid folderId)
        {
            var folder = FindWorkspaceSubfolder(folderId);
            if (folder == null)
            {
                throw new NotImplementedException("folder does not exist");
            }

            // find project ids for deletion
            var projectIds = new List<Guid>();
            if (folder != null)
            {
                projectIds = folder.FindProjectIds(folderId);
            }

            var rootFolder = _subfolders.FirstOrDefault(x => x.Id == folderId);
            if (rootFolder != null)
            {
                _subfolders.Remove(rootFolder);
            }

            foreach (var subfolder in _subfolders)
            {
                var deleteFolder = subfolder.DeleteNestedFolder(folderId);
                // TODO???
                //if (deleteFolder == null)
                //{
                //    throw new NotImplementedException();
                //}
            }

            return projectIds;
        }
    }
}
