using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Core.Domain
{
    public class GoogleVoiceConfig
    {
        private List<string> _languageCodes;
        private int _naturalSampleRateHertz;

        public GoogleVoiceConfig(List<string> languageCodes, int naturalSampleRateHertz)
        {
            _languageCodes = languageCodes;
            _naturalSampleRateHertz = naturalSampleRateHertz;
        }

        public List<string> LanguageCodes { get => _languageCodes; private set => _languageCodes =new List<string>(value); } 
        public int NaturalSampleRateHertz { get => _naturalSampleRateHertz; private set => _naturalSampleRateHertz = value; }

        public string DefaultLanguageCode
        {
            get
            {
                return LanguageCodes.First();
            }
        }
    }
}
