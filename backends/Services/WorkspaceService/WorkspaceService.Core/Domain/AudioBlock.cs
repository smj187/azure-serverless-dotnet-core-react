using BuildingBlocks.Domain;

namespace WorkspaceService.Core.Domain
{
    public class AudioBlock : EntityBase
    {
        private int _index;
        private string? _voiceId;
        private string? _value;
        private AudioFile? _audioFile;

        public AudioBlock(int index, string? voiceId = null, string? value = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            _index = index;
            _voiceId = voiceId;
            _value = value;
            _audioFile = null;
        }

        public int Index { get => _index; private set => _index = value; }
        public string? VoiceId { get => _voiceId; private set => _voiceId = value; }
        public string? Value { get => _value; private set => _value = value; }
        public AudioFile? AudioFile { get => _audioFile; private set => _audioFile = value; }

        public void DecreaseIndex() => _index--;
        public void IncreaseIndex() => _index++;
        public void AdjustIndex(int newIndex) => _index = newIndex;

        public void PatchValue(string? value)
        {
            _value = value;
            ModifiedAt = DateTimeOffset.UtcNow;
        }
        public void PatchVoice(string? voiceId)
        {
            _voiceId = voiceId;
            ModifiedAt = DateTimeOffset.UtcNow;
        }

        public bool CanGenerateAudio() => _voiceId != null && _audioFile != null && _value != null;
        public bool RequireRegenerateAudio() => true;

        public void GenerateAudio(string url)
        {
            if (_voiceId == null || _value == null)
            {
                throw new NotImplementedException("cannot create audio if any is null");
            }

            _audioFile = new AudioFile(_voiceId, url, _value);
        }
    }
}
