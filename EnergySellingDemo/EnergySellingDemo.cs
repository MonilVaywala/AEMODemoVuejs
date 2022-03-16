using Newtonsoft.Json.Converters;
using System;
using System.Text.Json.Serialization;


namespace EnergySellingDemo
{
    public class EnergySellingDTO
    {
        public DateTime Date { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
        public EnergyType Type { get; set; }

        public int Price { get; set; }
        public bool isDiscount { get; set; }
    }

    public enum EnergyType
    {
        Electricity,
        Gas
    }
}
