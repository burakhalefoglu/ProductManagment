using System;
using Core.Entities;

namespace Entities.Dtos
{
    public class ContractDto: IDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ArtistName { get; set; }

        public string ArtistEmail { get; set; }

        public string ArtistTel { get; set; }

        public DateTime Date { get; set; }

        public string Name { get; set; }

        public string Doc { get; set; }

        public override string ToString()
        {
            return $"ContractDto {{ Id: {Id}, Name: {Name}, Doc: {Doc}, UserId: {UserId}, ArtistName: {ArtistName}, ArtistEmail: {ArtistEmail}, ArtistTel: {ArtistTel} }}";
        }
    }
}