namespace HVACrate.Presentation.Models.Common
{
    public class BaseQueryFormModel
    {
        public string? SearchParam { get; set; }

        public int Page { get; set; } = 1;

        public int Limit { get; set; } = 3;
    }
}
