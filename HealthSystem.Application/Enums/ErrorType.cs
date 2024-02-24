using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthSystem.Application.DTOs.Enums;
#nullable disable
public enum ErrorType
{
    [Description("Registro não encontrado")]
    NOT_FOUND_REGISTER = 0,
    [Description("Campo inválido ou precisa ser preenchido")]
    INVALID_FIELD = 1,
    [Description("Quantidade expirada de registros")]
    EXPIRED_QUANTITY = 2,
}
