namespace TimeMachine.Models;

public class Person
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public City City { get; set; }

    public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
}

