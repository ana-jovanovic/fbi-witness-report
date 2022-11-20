using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace WitnessReport.Models
{
    [Serializable]
    public class Fugitive
    {
        [JsonProperty("age_range")]
        public string AgeRange { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("occupations")]
        public IList<string> Occupations { get; set; }

        [JsonProperty("field_offices")]
        public IList<string> Offices { get; set; }

        [JsonProperty("locations")]
        public IList<string> Locations { get; set; }

        [JsonProperty("reward_text")]
        public string RewardText { get; set; }

        [JsonProperty("hair")]
        public string Hair { get; set; }

        [JsonProperty("dates_of_birth_used")]
        public IList<string> UsedDateBirths { get; set; }

        [JsonProperty("caution")]
        public string Caution { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("age_min")]
        public string AgeMin { get; set; }

        [JsonProperty("age_max")]
        public string AgeMax { get; set; }

        [JsonProperty("scars_and_marks")]
        public string ScarsAndMarks { get; set; }

        [JsonProperty("subjects")]
        public IList<string> Subjects { get; set; }

        [JsonProperty("aliases")]
        public IList<string> Aliases { get; set; }

        [JsonProperty("race_raw")]
        public string Race { get; set; }

        [JsonProperty("publication")]
        public DateTime? PublicationDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("coordinates")]
        public IList<Coordinates> Coordinates { get; set; }

        [JsonProperty("eyes")]
        public string Eyes { get; set; }

        [JsonProperty("person_classification")]
        public string Classification { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("images")]
        public IList<Image> Images { get; set; }

        [JsonProperty("possible_countries")]
        public IList<string> PossibleCountries { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("place_of_birth")]
        public string PlaceOfBirth { get; set; }

        [JsonProperty("height_min")]
        public string HeightMin { get; set; }

        [JsonProperty("height_max")]
        public string HeightMax { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("ncic")]
        public string NCIC { get; set; }

        [JsonProperty("warning_message")]
        public string WarningMessage { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("lat")]
        public long Latitude { get; set; }

        [JsonProperty("lng")]
        public long Longitude { get; set; }

        [JsonProperty("formatted")]
        public string City { get; set; }
    }

    public class Image
    {
        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("caption")]
        public string Caption { get; set; }

        [JsonProperty("thumb")]
        public string Thumbnail { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }
    }
}
