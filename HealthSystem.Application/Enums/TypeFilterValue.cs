using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthSystem.Application.DTOs.Enums;
#nullable disable
public enum TypeFilterValue
{
    [Description("Int")]
    Int = 0,
    [Description("String")]
    String = 1
}
