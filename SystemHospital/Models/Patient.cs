using System.ComponentModel.DataAnnotations;
using VetPetcare.Models;

public class Patient(
    string firstName,
    string lastName,
    DateTime dateOfBirth,
    string phone,
    string email,
    string address,
    int document)
    : People(firstName, lastName, dateOfBirth, phone, email, address, document)
{
    public int PatientId
    {
        get => base.Id;
        internal set => base.Id = value;
    }
}