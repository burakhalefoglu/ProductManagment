using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class SubscriptionDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ArtistName { get; set; }
        public string ArtistEmail { get; set; }
        public string ArtistTel { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int SubscriptionStep { get; set; }
    }
}