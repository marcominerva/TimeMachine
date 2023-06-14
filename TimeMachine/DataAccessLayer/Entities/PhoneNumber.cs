using TimeMachine.Models.Enum;

namespace TimeMachine.DataAccessLayer.Entities;

public class PhoneNumber
{
    public Guid Id { get; set; }

    public Guid PersonId { get; set; }

    public PhoneNumberType Type { get; set; }

    public string Number { get; set; }

    public virtual Person Person { get; set; }
}