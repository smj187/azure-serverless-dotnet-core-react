using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CognitiveService.Core.Domain
{
    public class AwsVoiceConfig
    {
        private List<string> _specialStyles;
        private List<string> _engines;

        public AwsVoiceConfig(List<string> specialStyles, List<string> engines)
        {
            _specialStyles = specialStyles;
            _engines = engines;
        }

        public List<string> SpecialStyles { get => _specialStyles; private set => _specialStyles = new List<string>(value); }
        public List<string> Engines { get => _engines; private set => _engines = new List<string>(value); }

        public string DefaultEngine
        {
            get
            {
                var neural = Engines.FirstOrDefault(e => e == "neural");
                return neural ?? "standard";
            }
        }
    }
}
