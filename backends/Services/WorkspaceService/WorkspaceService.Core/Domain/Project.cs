using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Core.Domain
{
    public class Project : AggregateBase
    {
        private ProjectType _projectType;
        private string _name;
        private string? _description;
        private string? _imageUrl;
        private List<AudioBlock> _audioBlocks;


        public Project(ProjectType projectType, string name, string? description = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            _projectType = projectType;
            _name = name;
            _description = description;

            _imageUrl = null;
            _audioBlocks = new();
        }

        public ProjectType ProjectType { get => _projectType; private set => _projectType = value; }
        public string Name { get => _name; private set => _name = value; }
        public string? Description { get => _description; private set => _description = value; }
        public string? ImageUrl { get => _imageUrl; private set => _imageUrl = value; }
        public List<AudioBlock> AudioBlocks { get => _audioBlocks; private set => _audioBlocks = new List<AudioBlock>(value); }

        public void PatchName(string name)
        {
            _name = name;
        }

        public void PatchImageUrl(string url)
        {
            _imageUrl = url;
        }

        public AudioBlock AddAudioBlock(int? index)
        {
            AudioBlock block;

            if (index == null)
            {
                block = new AudioBlock(_audioBlocks.Count);
                _audioBlocks.Add(block);
            }
            else
            {
                _audioBlocks
                    .Skip((int)index)
                    .ToList()
                    .ForEach(b => b.IncreaseIndex());

                block = new AudioBlock((int) index);
                _audioBlocks.Insert((int) index, block);
            }

            ModifiedAt = DateTimeOffset.UtcNow;
            return block;
        }

        public bool RemoveAudioBlock(Guid blockId)
        {
            var block = _audioBlocks.FirstOrDefault(b => b.Id == blockId);
            if (block == null)
            {
                throw new NotImplementedException();
            }

            // update index after delete index
            _audioBlocks
                .Skip(block.Index)
                .ToList()
                .ForEach(element => element.DecreaseIndex());

            ModifiedAt = DateTimeOffset.UtcNow;

            return _audioBlocks.Remove(block);
        }

        public AudioBlock PatchAudioBlockIndex(Guid audioBlockId, int newIndex)
        {
            // check if new index is larger that the existing amount of items
            if (newIndex > _audioBlocks.Count)
            {
                throw new NotImplementedException();
            }

            var block = _audioBlocks.FirstOrDefault(b => b.Id == audioBlockId) ?? null;
            if (block == null)
            {
                throw new NotImplementedException();
            }

            var moveUp = newIndex > block.Index;

            if (moveUp)
            {
                var currentIndex = block.Index;
                var elementsToDecreaseIndex = newIndex - block.Index;
                block.AdjustIndex(newIndex);

                // remove element from index
                _audioBlocks.RemoveAt(currentIndex);

                _audioBlocks
                    .Skip(currentIndex)
                    .Take(elementsToDecreaseIndex)
                    .ToList()
                    .ForEach(elem => elem.DecreaseIndex());

                // update element index and insert at wanted index
                _audioBlocks.Insert(newIndex, block);
            }
            else
            {
                var currentIndex = block.Index;
                var elementsToIncrease = block.Index - newIndex;
                block.AdjustIndex(newIndex);

                // move element into new index
                _audioBlocks.RemoveAt(currentIndex);
                _audioBlocks.Insert(newIndex, block);

                // update other elements in between
                _audioBlocks
                    .Skip(newIndex + 1)
                    .Take(elementsToIncrease)
                    .ToList()
                    .ForEach(elem => elem.IncreaseIndex());
            }


            return block;
        }

        public AudioBlock PatchValue(Guid audioBlockId, string? value)
        {
            var block = _audioBlocks.FirstOrDefault(b => b.Id == audioBlockId);
            if (block == null)
            {
                throw new NotImplementedException();
            }

            block.PatchValue(value);
            ModifiedAt = DateTimeOffset.UtcNow;

            return block;
        }

        public AudioBlock PatchVoiceId(Guid audioBlockId, string? voiceId)
        {
            var block = _audioBlocks.FirstOrDefault(b => b.Id == audioBlockId);
            if (block == null)
            {
                throw new NotImplementedException();
            }

            block.PatchVoice(voiceId);
            ModifiedAt = DateTimeOffset.UtcNow;

            return block;
        }
    }
}
