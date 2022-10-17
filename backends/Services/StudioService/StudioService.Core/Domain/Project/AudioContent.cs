using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Core.Domain.Project
{
    public class AudioContent : EntityBase
    {
        private int _index;
        private string? _value;

        public AudioContent(int index)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            _index = index;
            _value = null;
        }

        public int Index
        {
            get => _index;
            private set => _index = value;
        }

        public string? Value
        {
            get => _value;
            private set => _value = value;
        }

        public void ChangeValue(string? value)
        {
            _value = value;
        }

        public void DecreaseIndex()
        {
            _index--;
        }

        public void IncreaseIndex()
        {
            _index++;
        }
        public void ChangeIndex(int index)
        {
            _index = index;
        }
    }
}
