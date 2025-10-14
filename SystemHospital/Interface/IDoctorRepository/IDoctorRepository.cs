using VetPetcare.Models;
public interface IDoctorRepository
{
    Doctor Create(Doctor doctor);
    Doctor GetByDocument(string document);
    IEnumerable<Doctor> GetAll();
    bool Update(Doctor doctor, string document);
    bool DeleteByDocument(string document);
}