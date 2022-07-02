using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Buzagmod
{
    internal class Mod
    {
        public Mod()
        {

        }

        public Mod ParseFromJsonString(string jsonString)
        {
            return JsonConvert.DeserializeObject<Mod>(jsonString);
        }        

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string md5 { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public string author { get; set; }

        public List<String> files { get; set; }       
    }
}
