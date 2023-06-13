namespace TimeMachine.Models;

public class Person
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string City { get; set; }

    public IEnumerable<PhoneNumber> PhoneNumbers { get; set; }
}

