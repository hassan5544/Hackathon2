namespace Hackathon2.Models
{
    using Newtonsoft.Json;

    public class SensorData
    {
        [JsonProperty("tempC")]
        public decimal TempC { get; set; }

        [JsonProperty("humi")]
        public decimal Humi { get; set; }

        [JsonProperty("dsm_consentrate")]
        public decimal DsmConsentrate { get; set; }

        [JsonProperty("dsm_particle")]
        public decimal DsmParticle { get; set; }

        [JsonProperty("air_quality_label")]
        public string AirQualityLabel { get; set; }

        [JsonProperty("sensor_value")]
        public int SensorValue { get; set; }
    }

}
