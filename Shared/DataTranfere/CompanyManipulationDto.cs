using System.ComponentModel.DataAnnotations;

namespace Shared.DataTranfere
{
public abstract record CompanyManipulationDto
{
   
    [Required(ErrorMessage = "Company name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
    public string? Name {get;init;}
    [Required(ErrorMessage = "Company Address is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Address is 100 characters.")]
    public string? Address{get; init;}
    [Required(ErrorMessage = "Country name is a required field.")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Country Name is 30 characters.")]
    public string? Country{get;init;}
    IEnumerable<EmployeeCreationDto>? Employees;
 }
}
