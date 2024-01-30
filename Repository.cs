using MyDiary.Models.Domains;
using MyDiary.Models.Wrappers;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using MyDiary.Models.Converters;

namespace MyDiary
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }

        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context.Students
                    .Include(s => s.Group)
                    .Include(s => s.Ratings)
                    .AsQueryable();

                if (groupId != 0)
                {
                    students = students.Where(s => s.GroupId == groupId);

                }

                return students
                      .ToList()
                      .Select(s => s.ToStudentWrapper())
                      .ToList();
            }
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToStudentDao();

            var ratings = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);

                ratings.ForEach(r =>
                {
                    r.StudentId = dbStudent.Id;
                    context.Ratings.Add(r);
                });

                context.SaveChanges();
            }
        }


        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var student = context.Students.Find(id);
                context.Students.Remove(student);
                context.SaveChanges();
            };

        }

        public void UpdateStudent(StudentWrapper studentwrapper)
        {
            var studentDao = studentwrapper.ToStudentDao();
            var ratingsDao = studentwrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateStudentProperties(studentDao, context);

                var currentStudentRatings = GetStudentRatings(studentDao, context);

                UpdateRate(studentDao, ratingsDao, currentStudentRatings, Subject.Math, context);
                UpdateRate(studentDao, ratingsDao, currentStudentRatings, Subject.Physics, context);
                UpdateRate(studentDao, ratingsDao, currentStudentRatings, Subject.Technology, context);
                UpdateRate(studentDao, ratingsDao, currentStudentRatings, Subject.PolishLanguage, context);
                UpdateRate(studentDao, ratingsDao, currentStudentRatings, Subject.ForeignLanguage, context);

                context.SaveChanges();
            }

        }

        private static List<Rating> GetStudentRatings(Student student, ApplicationDbContext context)
        {
            return context.Ratings.Where(x => x.StudentId == student.Id).ToList();
        }

        private void UpdateStudentProperties(Student studentDao, ApplicationDbContext context)
        {
            var studentToUpdate = context.Students.Find(studentDao.Id);
            studentToUpdate.FirstName = studentDao.FirstName;
            studentToUpdate.LastName = studentDao.LastName;
            studentToUpdate.Comments = studentDao.Comments;
            studentToUpdate.AdditionalClasses = studentDao.AdditionalClasses;
            studentToUpdate.GroupId = studentDao.GroupId;
        }

        private static void UpdateRate(Student studentDao, List<Rating> ratingsDao,
            List<Rating> currentStudentRatings, Subject subject, ApplicationDbContext context)
        {
            var currentSubjectRatings = currentStudentRatings
                .Where(x => x.SubjectId == (int)subject)
                .Select(x => x.Rate);

            var newSubjectRatings = ratingsDao
                .Where(x => x.SubjectId == (int)subject)
                .Select(x => x.Rate);

            var subjectRatingsToDelete = currentSubjectRatings.Except(newSubjectRatings).ToList();
            var subjectRatingsToAdd = newSubjectRatings.Except(currentSubjectRatings).ToList();

            subjectRatingsToDelete.ForEach(x =>
            {
                var ratingToDelete = context.Ratings.First(y =>
                y.Rate == x &&
                y.StudentId == studentDao.Id &&
                y.SubjectId == (int)subject);

                context.Ratings.Remove(ratingToDelete);
            });

            subjectRatingsToAdd.ForEach(x =>
            {
                var ratingToAdd = new Rating
                {
                    Rate = x,
                    StudentId = studentDao.Id,
                    SubjectId = (int)subject
                };
                context.Ratings.Add(ratingToAdd);
            });
        }
    }
}
