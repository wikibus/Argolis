﻿using System;
using System.Runtime.Serialization;
using Argolis.Hydra.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vocab;

namespace TestHydraApi
{
    [SupportedClass(ClassUri)]
    public class User
    {
        private const string ClassUri = "http://example.api/o#User";

        public static Uri Type { get; set; }

        public static JObject Context
        {
            get
            {
                var context = new JObject(
                    "firstName".IsProperty(Foaf.givenName),
                    "lastName".IsProperty(Foaf.lastName));

                return new AutoContext<User>(context, new Uri(ClassUri));
            }
        }

        public string Id { get; set; }

        [JsonProperty("firstName")]
        public string Name { get; set; }

        public string LastName { get; set; }

        [JsonProperty("with_attribute")]
        public string NotInContextWithAttribute { get; set; }

        [JsonIgnore]
        public string JsonIgnored { get; set; }

        [IgnoreDataMember]
        public string DataMemberIgnored { get; set; }
    }
}