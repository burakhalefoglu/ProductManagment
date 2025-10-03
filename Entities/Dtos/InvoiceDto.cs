using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class InvoiceDto: IDto 
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ArtistName { get; set; }

        public string ArtistTel { get; set; }

        public string ArtistEmail { get; set; }

        public string No { get; set; }

        public DateTime Date { get; set; }

        public double Price { get; set; }

        public string Doc { get; set; }

       
    }
}