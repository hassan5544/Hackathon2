using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Hackathon2.Models
{
    public class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Topic { get; set; }
        public decimal TempC { get; set; }
        public decimal Humi { get; set; }
        public decimal DsmConsentrate { get; set; }
        public decimal DsmParticle { get; set; }
        public string AirQualityLabel { get; set; }
        public int SensorValue { get; set; }
        public DateTime ReceivedAt { get; set; } = DateTime.Now;
    }
}
