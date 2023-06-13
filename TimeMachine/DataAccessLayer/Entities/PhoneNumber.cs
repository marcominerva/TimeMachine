namespace TimeMachine.DataAccessLayer.Entities;

public class PhoneNumber
{
    public Guid Id { get; set; }

    public Guid PersonId { get; set; }

    public string Number { get; set; }

    public virtual Person Person { get; set; }
}