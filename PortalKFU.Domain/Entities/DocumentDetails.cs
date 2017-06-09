using System.ComponentModel.DataAnnotations;

namespace PortalKFU.Domain.Entities
{
    public class DocumentDetails
    {
        [Required(ErrorMessage = "Укажите как вас зовут")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Статья по теме")]
        [Display(Name = "Выберите первую тему")]
        public string Line1 { get; set; }
        [Display(Name = "Выберите вторую тему")]
        public string Line2 { get; set; }
        [Display(Name = "Выберите третью тему")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Укажите тип")]
        [Display(Name = "Укажите тип")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Комментарий")]
        [Display(Name = "Комментарий")]
        public string Comment { get; set; }

        public bool GiftWrap { get; set; }
    }
}