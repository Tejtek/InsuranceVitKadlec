using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InsuranceVitKadlec.Models
{
    public class Article
    {

        public int Id { get; set; }
        [DisplayName("Název článku")]
        public string Title { get; set; }

        [DisplayName("Autor")]
        public string Author { get; set; }
        [DisplayName("Datum")]
        public  DateTime CreatedDate { get; set; }
        [DisplayName("Obsah")]
        public string Content { get; set; }  
  

    }
}
