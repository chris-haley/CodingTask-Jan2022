using System;
using LunchPriceCharging.Repository;

namespace LunchPriceCharging.Domain
{
    public interface IStudent
    {
        public Guid Id { get; set; }
        public bool FreeMeal { get; set; }
        public void AssignCorrectFreeMealStatus();
    }

    public class Student : IStudent
    {
        private static readonly IStudentRepository StudentRepositoryInstance = new StudentRepository();

        public static Student GetStudent(Guid studentId) => StudentRepositoryInstance.GetStudent(studentId);

        public void WriteStudent()
        {
            StudentRepositoryInstance.SetStudent(this);
        }

        public Guid Id { get; set; }

        public YearGroup YearGroup;
        public bool FreeMeal { get; set; }

        public bool FreeMealDueToYearGroup => YearGroup.FreeMeal;

        public void AssignCorrectFreeMealStatus()
        {
            throw new NotImplementedException();
        }
    }
}
