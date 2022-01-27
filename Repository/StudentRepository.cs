using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using LunchPriceCharging.Domain;

namespace LunchPriceCharging.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private const string FilePath = "C:\\data.txt";
        private readonly Dictionary<Guid, Student> _data;

        public StudentRepository()
        {
            // we assume for this exercise the file exists and is already populated with students
            // there would be other code to add and remove students

            var content = File.ReadAllText(FilePath);
            _data = JsonSerializer.Deserialize<Dictionary<Guid, Student>>(content);
        }

        public Student GetStudent(Guid studentId)
        {
            return _data[studentId];
        }

        public async Task SetStudent(Student student)
        {
            if (student == null)
                throw new ArgumentNullException();

            _data[student.Id] = student;

            await File.WriteAllTextAsync(FilePath, JsonSerializer.Serialize(_data));
        }
    }
}
