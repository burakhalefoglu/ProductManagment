using Core.Entities;
using System;

namespace Entities.Dtos
{
    public class EvaluationDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string StageDiscipline { get; set; } = null!;       // Örn: "Mükemmel"
        public string TotalAudience { get; set; } = null!;         // Örn: "Yeterli"
        public string ImageManagement { get; set; } = null!;       // Örn: "Güçlü"
        public string BookingPotential { get; set; } = null!;      // Örn: "Yüksek"
    }
}
