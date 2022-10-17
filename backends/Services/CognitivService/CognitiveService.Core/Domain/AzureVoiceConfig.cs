using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Core.Domain
{
    public class AzureVoiceConfig
    {
        private int _sampleRateHertz;
        private string _voiceType;
        private int _wordsPerMinute;
        private List<string>? _styleList;
        private List<string>? _rolePlayList;
        private bool _isHighQuality48K;

        public AzureVoiceConfig(int sampleRateHertz, string voiceType, int wordsPerMinute, List<string>? styleList = null, List<string>? rolePlayList = null, bool? isHighQuality48K = null)
        {
            _sampleRateHertz = sampleRateHertz;
            _voiceType = voiceType;
            _wordsPerMinute = wordsPerMinute;
            _styleList = styleList;
            _rolePlayList = rolePlayList;
            _isHighQuality48K = isHighQuality48K ?? false;
        }

        public int SampleRateHertz { get => _sampleRateHertz; private set => _sampleRateHertz = value; }
        public string VoiceType { get => _voiceType; private set => _voiceType = value; }
        public int WordsPerMinute { get => _wordsPerMinute; private set => _wordsPerMinute = value; }
        public List<string>? StyleList { get => _styleList; private set => _styleList = value != null ? new List<string>(value) : null; }
        public List<string>? RolePlayList { get => _rolePlayList; private set => _rolePlayList = value != null ? new List<string>(value) : null; }
        public bool IsHighQuality48K { get => _isHighQuality48K; private set => _isHighQuality48K = value; }
    }
}
