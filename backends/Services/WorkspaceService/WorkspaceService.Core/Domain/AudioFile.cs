using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkspaceService.Core.Domain
{
    public class AudioFile : ValueObject
    {
        private string _voiceId;
        private string _audioUrl;
        private string _value;

        public AudioFile(string voiceId, string audioUrl, string value)
        {
            _voiceId = voiceId;
            _audioUrl = audioUrl;
            _value = value;
        }


        public string VoideId { get => _voiceId; private set => _voiceId = value; }
        public string AudioUrl { get => _audioUrl; private set => _audioUrl = value; }
        public string Value { get => _value; private set => _value = value; }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return VoideId;
            yield return AudioUrl;
            yield return Value;
        }
    }
}
