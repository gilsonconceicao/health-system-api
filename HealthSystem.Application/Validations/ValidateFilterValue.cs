namespace HealthSystem.Application.Validations;
#nullable disable
public class ValidateFilterValue<T>
{
    private DateTime? birthDate;


    public T Value { get; set; }

    public ValidateFilterValue(T valueFilter)
    {
        Value = valueFilter;
    }

    public ValidateFilterValue(DateTime? birthDate)
    {
        this.birthDate = birthDate;
    }


    public bool IsValidField()
    {
        if (Value == null) return false;
        var valueToString = Value.ToString();
        if (
            valueToString.Length == 0 ||
            String.IsNullOrEmpty(valueToString)
        )
        {
            return false;
        }
        return true;
    }
}

