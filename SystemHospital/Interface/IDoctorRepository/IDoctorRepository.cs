using VetPetcare.Models;
public interface IDoctorRepository
{
    Doctor Create(Doctor doctor);
    Doctor GetById(int document);
    IEnumerable<Doctor> GetAll();
    bool Update(Doctor doctor, int document);
    bool DeleteById(int document);
}