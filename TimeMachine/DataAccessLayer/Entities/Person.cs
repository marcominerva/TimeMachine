namespace TimeMachine.DataAccessLayer.Entities;

public class Person
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid CityId { get; set; }

    public virtual City City { get; set; }

    public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
}

