using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using LunchPriceCharging.Domain;
using LunchPriceCharging.Repository;

namespace LunchPriceCharging.Api
{
    public class LunchPriceApi
    {
        private readonly int _freeMealPrice;
        private readonly StudentRepository _studentRepository;

        #region constructor

        public LunchPriceApi(int freeMealPrice)
        {
            _freeMealPrice = freeMealPrice;
            _studentRepository = new StudentRepository();
        }
        #endregion
        
        #region get/set free meal status
        public bool GetFreeMealStatus(Guid studentId)
        {
            var student = Student.GetStudent(studentId);

            if (student == null)
                throw new DataException();

            return student.FreeMeal;
        }

        public void SetFreeMealStatus(Guid studentId, bool newFreeMealStatus)
        {
            var student = Student.GetStudent(studentId);

            var origFreeMealStatus = student.FreeMeal;

            bool bUpdated = true;
            if (origFreeMealStatus != student.FreeMeal)
            {
                bUpdated = false;
                student.FreeMeal = newFreeMealStatus;
            }
            // TODO: figure out why it doesn't save the student when FreeMeal is updated - we'll save it always for now
            student.WriteStudent();

            if (bUpdated)
                student.WriteStudent();

        }
        #endregion

        #region Compute price for student(s)
        public int ComputePriceForStudent(Guid studentId, int fullPriceInPence)
        {
            // TODO: why does this not recognize changes set immediately before with SetFreeMealStatus??? Some weird serializing bug??
            var student = _studentRepository.GetStudent(studentId);

            if (student.FreeMeal) return 0;

            if (student.FreeMealDueToYearGroup) return 0;

            return fullPriceInPence;
        }

        public IEnumerable<Tuple<Guid, int>> ComputePricesForStudentGroup(List<Guid> studentIds,
            int fullPriceInPence)
        {
            var students = studentIds.Select(Student.GetStudent).ToList();

            return students.Select(student => new Tuple<Guid, int>(student.Id, CalcSingle(student, fullPriceInPence)));
        }
        #endregion

        #region helpers
        private int CalcSingle(Student student, int fullPriceInPence)
        {
            if (student.FreeMeal || student.FreeMealDueToYearGroup) return _freeMealPrice;

            return fullPriceInPence;
        }
        #endregion
    }
}
