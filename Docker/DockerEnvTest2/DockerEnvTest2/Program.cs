using DockerEnvTest2.Models;
using System;

namespace DockerEnvTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("학생 명단 출력");
            using var db = new SchoolDBContext("Server=sql-dev;Database=School;User Id=sa;Password=yourStrong(!)Password;");
            db.Database.EnsureCreated();
            //db.Student.Add(new Student() { Name = "홍길동", Grade = 1 });
            //db.Student.Add(new Student() { Name = "홍길순", Grade = 1 });
            //db.Student.Add(new Student() { Name = "김철수", Grade = 2 });
            //db.Student.Add(new Student() { Name = "김영희", Grade = 2 });
            //db.SaveChanges();
            foreach (var student in db.Student)
            {
                Console.WriteLine($"이름 : {student.Name}, 학년 : {student.Grade}");
            }
        }
    }
}
