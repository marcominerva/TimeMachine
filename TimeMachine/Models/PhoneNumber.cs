using TimeMachine.Models.Enum;

namespace TimeMachine.Models;

public class PhoneNumber
{
    public Guid Id { get; set; }

    public PhoneNumberType Type { get; set; }

    public string Number { get; set; }
}