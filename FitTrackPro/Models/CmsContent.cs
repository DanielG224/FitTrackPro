using System.ComponentModel.DataAnnotations;

namespace FitTrackPro.Models
{
    public class CmsContent
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Identyfikator sekcji jest wymagany")]
        [Display(Name = "Klucz sekcji (np. Home_Welcome)")]
        public string SectionKey { get; set; }

        [Required(ErrorMessage = "Treść jest wymagana")]
        [Display(Name = "Treść")]
        public string ContentValue { get; set; }
    }
}