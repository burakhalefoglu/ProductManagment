using Core.Entities;

namespace Entities.Dtos
{
    public class UpdateUserDto : IDto
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string MobilePhones { get; set; }
        public string YoutubeProfile { get; set; }
        public string InstagramProfile { get; set; }
        public string SpotifyProfile { get; set; }
        public string VideoOrSoundUrl { get; set; }
        public string MusicStyle { get; set; }
        public string Manager { get; set; }
        public string Studio { get; set; }
        public string InspirationArtists { get; set; }
        public string Biography { get; set; }
    }
}