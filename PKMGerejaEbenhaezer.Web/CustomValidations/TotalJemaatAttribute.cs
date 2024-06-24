using PKMGerejaEbenhaezer.Web.Areas.Dashboard.Models.Rayon;
using System.ComponentModel.DataAnnotations;

namespace PKMGerejaEbenhaezer.Web.CustomValidations
{
    public sealed class TotalJemaatAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IRayonVM rayonVM = (IRayonVM)validationContext.ObjectInstance;

            if (rayonVM.TotalBerdasarkanKelamin != rayonVM.TotalBerdasarkanUsia)
            {
                return new ValidationResult("Total jemaat berdasarkan kelamin dan usia tidak sama");
            }

            return ValidationResult.Success;
        }
    }
}
