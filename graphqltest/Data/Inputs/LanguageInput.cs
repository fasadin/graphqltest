namespace graphqltest.Data.Inputs
{
    public class LanguageInput
    {
        public LanguageInput(string indicator)
        {
            Indicator = indicator;
        }
        
        public string Indicator { get; set; } 
    }
}