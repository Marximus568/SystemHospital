using VetPetcare.Models;

namespace VetPetcare.Interface;

public interface IMedicalAppointment
{
    MedicalAppointment Create(MedicalAppointment MedicalAppointment);
    MedicalAppointment GetById(int id);
    IEnumerable <MedicalAppointment> GetAll();
    bool Update(MedicalAppointment MedicalAppointment, int id);
    bool DeleteById(int id);
}