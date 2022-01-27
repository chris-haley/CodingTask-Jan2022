using System;
using System.Threading.Tasks;
using LunchPriceCharging.Domain;

namespace LunchPriceCharging.Repository
{
    public interface IStudentRepository
    {
        public Student GetStudent(Guid studentId);

        public Task SetStudent(Student student);
    }
}