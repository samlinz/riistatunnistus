using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BackEnd.Model {
	internal class SpeciesFileDescription {
		internal IList<SpeciesClassMapping> SpeciesClassMappings { get; set; }
	}

	internal class SpeciesClassMapping {
		public String SpeciesClassName { get; set; }
		public IList<SpeciesMapping> SpeciesClassSpeciesMappings { get; set; }
	}

	internal class SpeciesMapping {
		public String SpeciesName { get; set; }
		public String SpeciesImageDirectory { get; set; }
	}

	internal class SpeciesFileDescriptionConverter : JsonConverter {
		public override void WriteJson(JsonWriter writer, Object value, JsonSerializer serializer) {
			throw new NotImplementedException();
		}

		public override Object ReadJson(JsonReader reader, Type objectType, Object existingValue, JsonSerializer serializer) {
			if (reader.TokenType != JsonToken.StartObject)
				return null;

			SpeciesFileDescription speciesFileDescription = new SpeciesFileDescription();
			
			JObject rootObject = JObject.Load(reader);
			if (rootObject == null) return null;

			IList<SpeciesClassMapping> speciesClasses = new List<SpeciesClassMapping>();

			foreach (JProperty classMapping in rootObject.Children<JProperty>()) {
				if (classMapping.Value?.Type != JTokenType.Object)
					continue;

				SpeciesClassMapping speciesClass = new SpeciesClassMapping {
					SpeciesClassName = classMapping.Name,
					SpeciesClassSpeciesMappings = new List<SpeciesMapping>()
				};

				JObject classSpeciesMappings = (JObject) classMapping.Value;
				foreach (JProperty speciesMapping in classSpeciesMappings.Children<JProperty>()) {
					if (speciesMapping.Value.Type != JTokenType.String)
						continue;

					SpeciesMapping species = new SpeciesMapping {
						SpeciesName = speciesMapping.Name,
						SpeciesImageDirectory = speciesMapping.Value.ToObject<string>()
					};

					speciesClass
						.SpeciesClassSpeciesMappings
						.Add(species);
				}

				if (speciesClass
						.SpeciesClassSpeciesMappings
						.Count == 0)
					continue;

				speciesClasses.Add(speciesClass);
			}

			speciesFileDescription.SpeciesClassMappings = speciesClasses;
			return speciesClasses.Count == 0 ? null : speciesFileDescription;
		}

		public override Boolean CanRead { get; } = true;
		public override Boolean CanWrite { get; } = false;

		public override Boolean CanConvert(Type objectType)
			=> typeof(SpeciesFileDescription).IsAssignableFrom(objectType);
	}
}