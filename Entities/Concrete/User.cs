using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Entities;
using Newtonsoft.Json;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public User()
        {
            if (UserId == 0)
            {
                RecordDate = DateTime.Now;
            }
            UpdateContactDate = DateTime.Now;
            Status = true;
        }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserClaim> UserClaims { get; set; }

       public int UserId { get; set; }
        public long? CitizenId { get; set; } // Kullan�c� Ad�
        public string FullName { get; set; } // Ad�n�z - Soyad�n�z
        public string MobilePhones { get; set; } // Telefon
        public string Email { get; set; } // E-posta
        [JsonIgnore]
        public string RefreshToken { get; set; }

        public bool Status { get; set; } // Kullan�c� aktif/pasif durumu
        public DateTime RecordDate { get; set; } // Kullan�c�n�n kay�t tarihi
        public DateTime UpdateContactDate { get; set; } // Son g�ncelleme tarihi

        /// <summary>
        /// This is required when encoding token. Not in db. The default is Person.
        /// </summary>
        [NotMapped]
        public string AuthenticationProviderType { get; set; } = "Person";

        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public bool UpdateMobilePhone(string mobilePhone)
        {
            if (mobilePhone == MobilePhones)
            {
                return false;
            }

            MobilePhones = mobilePhone;
            return true;
        }
    }
}
