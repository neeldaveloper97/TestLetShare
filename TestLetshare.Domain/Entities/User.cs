using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestLetshare.Domain.Entities
{
        [Table("tbl_user")]
        public class User
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("middle_name")]
        public string? MiddleName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("telephone")]
        public string? Telephone { get; set; }

        [Column("job_title")]
        public string JobTitle { get; set; }

        [Column("first_access")]
        public bool FirstAccess { get; set; } = true;

        [Column("language_id")]
        public string LanguageId { get; set; }

        [Column("profile_image_id")]
        public string ProfileImageId { get; set; }

        [Column("tenant_id")]
        public string? TenantId { get; set; }

        [Column("enabled")]
        public bool Enabled { get; set; }

        [Column("role")]
        public string Role { get; set; }  

        [Column("calendar_view")]
        public string CalendarView { get; set; }

        }

}

