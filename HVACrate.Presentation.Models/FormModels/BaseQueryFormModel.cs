namespace HVACrate.Presentation.Models.FormModels
{
    public class BaseQueryFormModel
    {
        public string? SearchParam { get; set; }

        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 5;
    }
}
