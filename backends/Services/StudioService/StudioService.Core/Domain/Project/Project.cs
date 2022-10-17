using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Core.Domain.Project
{
    public class Project : AggregateBase
    {
        private string _name;
        private string? _description;
        private string? _imageUrl;


        private Guid _workspaceId;
        private Guid? _folderId;

        private List<AudioContent> _audioContent;


        public Project(string name, Guid workspaceId, Guid? folderId = null, string? description = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            _name = name;
            _description = description;
            _imageUrl = null;


            _folderId = folderId;
            _workspaceId = workspaceId;

            _audioContent = new List<AudioContent>();
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

        public string? ImageUrl
        {
            get => _imageUrl;
            private set => _imageUrl = value;
        }

        public Guid? FolderId
        {
            get => _folderId;
            private set => _folderId = value;
        }

        public Guid WorkspaceId
        {
            get => _workspaceId;
            private set => _workspaceId = value;
        }

        public List<AudioContent> AudioContent
        {
            get => _audioContent;
            private set => _audioContent = new List<AudioContent>(value);
        }

        public void PatchDescription(string name, string? description = null)
        {
            _name = name;
            _description = description;
        }

        public AudioContent? GenerateAudio(Guid contentId)
        {
            var content = _audioContent.FirstOrDefault(c => c.Id == contentId);
            if (content != null)
            {
                // TODO: generate content
            }

            return content;
        }

        public AudioContent? PatchContentValue(Guid contentId, string? value)
        {
            var content = _audioContent.FirstOrDefault(c => c.Id == contentId);
            if (content != null)
            {
                content.ChangeValue(value);
            }

            return content;
        }

        public List<AudioContent> OrderContent(Guid contentId, int newIndex)
        {
            // check if new index is larger that the existing amount of items
            if (newIndex > _audioContent.Count)
            {
                throw new NotImplementedException();
            }

            var audioContent = _audioContent.FirstOrDefault(c => c.Id == contentId) ?? null;
            if (audioContent == null)
            {
                throw new NotImplementedException();
            }


            var moveUp = newIndex > audioContent.Index;

            if (moveUp)
            {
                var currentIndex = audioContent.Index;
                var elementsToDecreaseIndex = newIndex - audioContent.Index;
                audioContent.ChangeIndex(newIndex);

                // remove element from index
                _audioContent.RemoveAt(currentIndex);

                _audioContent
                    .Skip(currentIndex)
                    .Take(elementsToDecreaseIndex)
                    .ToList()
                    .ForEach(elem => elem.DecreaseIndex());

                // update element index and insert at wanted index
                _audioContent.Insert(newIndex, audioContent);
            }
            else
            {
                var currentIndex = audioContent.Index;
                var elementsToIncrease = audioContent.Index - newIndex;
                audioContent.ChangeIndex(newIndex);

                // move element into new index
                _audioContent.RemoveAt(currentIndex);
                _audioContent.Insert(newIndex, audioContent);

                // update other elements in between
                _audioContent
                    .Skip(newIndex + 1)
                    .Take(elementsToIncrease)
                    .ToList()
                    .ForEach(elem => elem.IncreaseIndex());
            }

            return _audioContent;

        }

        public AudioContent RemoveContent(Guid contentId)
        {
            var audioContent = _audioContent.FirstOrDefault(c => c.Id == contentId) ?? null;
            if (audioContent == null)
            {
                throw new NotImplementedException();
            }

            var index = audioContent.Index;
            // update index after delete index
            _audioContent
                .Skip(index)
                .ToList()
                .ForEach(element => element.DecreaseIndex());

            // delete the element
            var isDeleted = _audioContent.Remove(audioContent);
            if (isDeleted == false)
            {
                throw new NotImplementedException();
            }

            return audioContent;

        }

        public AudioContent AddContent(int? index = null)
        {
            AudioContent audioContent;

            if (index == null)
            {
                var insertAtEndIndex = _audioContent.Count;
                audioContent = new AudioContent(insertAtEndIndex);
                _audioContent.Add(audioContent);
            }
            else
            {
                int insertInMiddleIndex = (int)index;
                _audioContent
                    .Skip(insertInMiddleIndex)
                    .ToList()
                    .ForEach(element =>
                    {
                        element.IncreaseIndex();
                    });

                audioContent = new AudioContent(insertInMiddleIndex);
                _audioContent.Insert(insertInMiddleIndex, audioContent);
            }


            ModifiedAt = DateTime.UtcNow;

            return audioContent;
        }
    }
}
