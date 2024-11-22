

using System.Data;

namespace MyApp
{
    public class Subject:Student
    {
        private int _ID;
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _studentId;
        public int StudentId
        {
            get { return _studentId; }
            set { _studentId = value; }
        }

        private int _grade;
        public int Grade
        {
            get { return _grade; }
            set
            {
                if (value <= 100 && value >= 0)
                {
                    _grade = value;
                }
                else
                {
                    throw new Exception("Grade is incorrect");
                }

            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }
        public List<Subject> Fill()
        {
            List<Subject> TestSubjects = new List<Subject>
            {
                new Subject()
                {
                    ID = 1,
                    Name = "Math",
                    StudentId = 1,
                    Grade = 70,
                    Date = DateTime.Now,
                },
                new Subject()
                {
                    ID = 2,
                    Name = "PE",
                    StudentId = 2,
                    Grade = 40,
                    Date = DateTime.Now,
                },
                new Subject()
                {
                    ID = 3,
                    Name = "Chemistry",
                    StudentId = 1,
                    Grade = 50,
                    Date = DateTime.Now,
                },
                new Subject()
                {
                    ID = 4,
                    Name = "Biology",
                    StudentId = 2,
                    Grade = 10,
                    Date = DateTime.Now,
                },
                new Subject()
                {
                    ID = 5,
                    Name = "History",
                    StudentId = 2,
                    Grade = 90,
                    Date = DateTime.Now,
                }
            };
            return TestSubjects;
        }

        public static List<Subject> GetByStudentId(List<Subject> subjects, int studentId)
        {
            return subjects.Where(s => s.StudentId == studentId).ToList();
        }

    }

    public class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public Dictionary<int, int> Subjects { get; set; } = new Dictionary<int, int>(); 
        public double AverageGrade { get; private set; }
        public TypeOfGrant Grant { get; private set; }

        public enum TypeOfGrant
        {
            None,
            Regular,
            Increased
        }
        public List<Student> Fill()
        {
            return new List<Student>
        {
            new Student { ID = 1, FirstName = "Oleh", SecondName = "Ivanov", Age = 20 },
            new Student { ID = 2, FirstName = "Maria", SecondName = "Petrova", Age = 21 }
        };
        }

        public void SetSubjects(List<Subject> subjects)
        {
            var studentSubjects = subjects.Where(s => s.StudentId == ID).ToList();
            foreach (var subject in studentSubjects)
            {
                Subjects[subject.ID] = subject.Grade; 
            }
        }

        public void CalculateAverageGrade()
        {
            if (Subjects.Any())
            {
                AverageGrade = Subjects.Average(s => s.Value);
            }
        }

        public void SetGrant()
        {
            if (AverageGrade < 60)
                Grant = TypeOfGrant.None;
            else if (AverageGrade < 90)
                Grant = TypeOfGrant.Regular;
            else
                Grant = TypeOfGrant.Increased;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Subject subject = new Subject();
            List<Subject> subjects = subject.Fill();

            Student student = new Student();
            List<Student> students = student.Fill();
            foreach (var item in students)
            {
                item.SetSubjects(subjects);

                item.CalculateAverageGrade();

                item.SetGrant();
            }
            var studentToDisplay = students.Last();
            Console.WriteLine($"Student: {studentToDisplay.FirstName} {studentToDisplay.SecondName}");
            Console.WriteLine($"Age: {studentToDisplay.Age}");
            Console.WriteLine($"Average Grade: {studentToDisplay.AverageGrade}");
            Console.WriteLine($"Grant: {studentToDisplay.Grant}");
            Console.WriteLine("Subjects:");
            foreach (var item in studentToDisplay.Subjects)
            {
                Console.WriteLine($"- {subjects.First(x => x.ID == item.Key).Name}: {subjects.First(x => x.Grade == item.Value).Grade}");
            }
        }
    }

}