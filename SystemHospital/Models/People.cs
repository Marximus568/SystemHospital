namespace VetPetcare.Models;

public abstract class People(string FirstName,string LastName, DateTime DateOfBirth, string phone, string Email, string Address, int Document)
{
    private static int _lastId = 1;
    public int Id { get; protected set; } = _lastId++;
    public string FirstName { get; protected set; } = FirstName;
    public string LastName { get; protected set; } = LastName;
    public int Document { get; protected set; } = Document;
    public DateTime DateOfBirth { get; protected set; } = DateOfBirth;
    public string Phone { get; protected set; } = phone;
    public string Email { get; protected set; } = Email;
    public string Address { get; protected set; } = Address;
    
    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
    public string GetInfo()
    {
        return  $"ID: {Id}\n"+ 
                $"Name: {FirstName} {LastName}\n" +
                $"Email: {Email}\n" +
                $"Address: {Address}\n" +
                $"Phone: {Phone}\n" +
                $"Birth date: {DateOfBirth:dd/MM/yyyy}";
    }
}