using BuildingBlocks.Domain;
using Microsoft.AspNetCore.Http;
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
        private AzureFile? _avatarImage;
        private AzureFile? _previewAudio;

        private VoiceProvider _voiceProvider;
        private VoiceType _voiceType;
        private AwsVoiceConfig? _awsVoiceConfig;
        private GoogleVoiceConfig? _googleVoiceConfig;
        private AzureVoiceConfig? _azureVoiceConfig;

        private bool _isAvailable;

        public Voice(string displayName, string internalName, string locale, string gender, VoiceProvider provider, VoiceType voiceType, AwsVoiceConfig? awsVoiceConfig = null, GoogleVoiceConfig? googleVoiceConfig = null, AzureVoiceConfig? azureVoiceConfig = null)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            ModifiedAt = null;

            _displayName = displayName;
            _internalName = internalName;
            _locale = locale;
            _gender = gender;
            _avatarImage = null;
            _previewAudio = null;

            _voiceProvider = provider;
            _voiceType = voiceType;

            _isAvailable = false;
            _awsVoiceConfig = awsVoiceConfig;
            _googleVoiceConfig = googleVoiceConfig;
            _azureVoiceConfig = azureVoiceConfig;
        }

        public string DisplayName { get => _displayName; set => _displayName = value; }
        public string InternalName { get => _internalName; set => _internalName = value; }
        public string Locale { get => _locale; set => _locale = value; }
        public string Gender { get => _gender; set => _gender = value; }
        public AzureFile? AvatarImage { get => _avatarImage; set => _avatarImage = value; }
        public AzureFile? PreviewAudio { get => _previewAudio; set => _previewAudio = value; }
        public VoiceProvider VoiceProvider { get => _voiceProvider; set => _voiceProvider = value; }
        public VoiceType VoiceType { get => _voiceType; set => _voiceType = value; }
        public AwsVoiceConfig? AwsVoiceConfig { get => _awsVoiceConfig; set => _awsVoiceConfig = value; }
        public GoogleVoiceConfig? GoogleVoiceConfig { get => _googleVoiceConfig; set => _googleVoiceConfig = value; }
        public AzureVoiceConfig? AzureVoiceConfig { get => _azureVoiceConfig; set => _azureVoiceConfig = value; }

        public bool IsAvailable { get => _isAvailable; set => _isAvailable = value; }

        public bool IsAwsVoice() => _voiceProvider.IsAwsProvider();
        public bool IsGoogleVoice() => _voiceProvider.IsGoogleProvider();

        public void PatchVoiceAvailability()
        {
            _isAvailable = !_isAvailable;
        }

        public void PatchVoiceNameName(string newDisplayName)
        {
            _displayName = newDisplayName;
        }


        public void CreateVoicePreview(string url, string name, IFormFile image)
        {
            _previewAudio = new AzureFile(url, name, image.ContentType, image.Length);
        }

        public void ResetVoicePreview() => _previewAudio = null;

        public void CreateVoiceAvatar(string url, string name, IFormFile image)
        {
            _avatarImage = new AzureFile(url, name, image.ContentType, image.Length);
        }

        public void ResetVoiceAvatar() => _avatarImage = null;

        public void PatchVoicePreview(AzureFile? previewAudio)
        {
            _previewAudio = previewAudio;
        }

        public void PatchVoiceType(int value)
        {
            _voiceType = VoiceType.CreateFromValue(value);
        }
    }
}
