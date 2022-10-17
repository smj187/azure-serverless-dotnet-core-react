using BuildingBlocks.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Core.Domain
{
    public class Voice : AggregateBase
    {
        private string _displayName;
        private string _internalName;
        private string _locale;
        private string _gender;
        private string? _avatarUrl;

        private VoiceProvider _voiceProvider;
        private AwsVoiceConfig? _awsVoiceConfig;
        private GoogleVoiceConfig? _googleVoiceConfig;
        private AzureVoiceConfig? _azureVoiceConfig;

        private bool _isAvailable;

        public Voice(string displayName, string internalName, string locale, string gender, VoiceProvider provider, AwsVoiceConfig? awsVoiceConfig = null, GoogleVoiceConfig? googleVoiceConfig = null, AzureVoiceConfig? azureVoiceConfig = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            _displayName = displayName;
            _internalName = internalName;
            _locale = locale;
            _gender = gender;
            _avatarUrl = null;

            _voiceProvider = provider;

            _isAvailable = false;
            _awsVoiceConfig = awsVoiceConfig;
            _googleVoiceConfig = googleVoiceConfig;
            _azureVoiceConfig = azureVoiceConfig;
        }

        public string DisplayName { get => _displayName; set => _displayName = value; }
        public string InternalName { get => _internalName; set => _internalName = value; }
        public string Locale { get => _locale; set => _locale = value; }
        public string Gender { get => _gender; set => _gender = value; }
        public string? AvatarUrl { get => _avatarUrl; set => _avatarUrl = value; }
        public VoiceProvider VoiceProvider { get => _voiceProvider; set => _voiceProvider = value; }
        public AwsVoiceConfig? AwsVoiceConfig { get => _awsVoiceConfig; set => _awsVoiceConfig = value; }
        public GoogleVoiceConfig? GoogleVoiceConfig { get => _googleVoiceConfig; set => _googleVoiceConfig = value; }
        public AzureVoiceConfig? AzureVoiceConfig { get => _azureVoiceConfig; set => _azureVoiceConfig = value; }

        public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }

        public bool IsAwsVoice() => _voiceProvider.IsAwsProvider();
        public bool IsGoogleVoice() => _voiceProvider.IsGoogleProvider();

        public void PatchAvailability()
        {
            _isAvailable = !_isAvailable;
        }

        public void PatchDisplayName(string newDisplayName)
        {
            _displayName = newDisplayName;
        }
    }
}
