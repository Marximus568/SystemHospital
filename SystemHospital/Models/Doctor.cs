namespace VetPetcare.Models;

public class Doctor(string FirstName,
    string LastName,
    DateTime DateOfBirth, 
    string phone, 
    string Email, 
    string Address,
    string Document) : People(FirstName,
    LastName, DateOfBirth, phone, Email, Address,Document)
{
    public int DoctorId
    {
        get => base.Id;
        internal set => base.Id = value;
    }
    public string speciality{get; private set;}

    public Doctor(string FirstName, 
        string LastName, 
        DateTime DateOfBirth, 
        string phone, 
        string Email,
        string Address, 
        string speciality,
        string Document) : this(FirstName, LastName, DateOfBirth, phone, Email, Address,Document)
    {
        this.speciality = speciality;
    }
}