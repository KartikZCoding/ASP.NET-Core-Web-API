namespace CollegeApp.Model
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student> {
                new Student
                { Id = 1,
                    StudentName = "Kartik",
                    Email = "Kartik123@gmail.com",
                    Address = "Hyd, India"
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Aryan",
                    Email = "Aryan123@gmail.com",
                    Address = "Banglore, India"
                }
            };
    }
}
