using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioService.Application.Models
{
    public class GoogleVoiceModel
    {
        public List<Voice> voices { get; set; }
    }

    public class Voice
    {
        public List<string> languageCodes { get; set; }
        public string name { get; set; }
        public string ssmlGender { get; set; }
        public int naturalSampleRateHertz { get; set; }
    }


}
