using System.ComponentModel.DataAnnotations;

namespace graphqltest.Common.Data
{
    public class Language
    {
        public string Text { get; set; }
        [Key]
        public string Indicator { get; set; }
    }
}