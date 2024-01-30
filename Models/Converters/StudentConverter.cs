using MyDiary.Models.Domains;
using MyDiary.Models.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace MyDiary.Models.Converters
{
    public static class StudentConverter
    {
        public static StudentWrapper ToStudentWrapper(this Student model)
        {
            return new StudentWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                AdditionalClasses = model.AdditionalClasses,
                Group = new GroupWrapper
                {
                    Id = model.Group.Id,
                    Name = model.Group.Name
                },
                Math = string.Join(",", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.Math)
                    .Select(y => y.Rate)),

                Physics = string.Join(",", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.Physics)
                    .Select(y => y.Rate)),

                Technology = string.Join(",", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.Technology)
                    .Select(y => y.Rate)),

                PolishLanguage = string.Join(",", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.PolishLanguage)
                    .Select(y => y.Rate)),

                ForeignLanguage = string.Join(",", model.Ratings
                    .Where(y => y.SubjectId == (int)Subject.ForeignLanguage)
                    .Select(y => y.Rate)),
            };
        }

        public static Student ToStudentDao(this StudentWrapper model)
        {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                AdditionalClasses = model.AdditionalClasses,
                GroupId = model.Group.Id
            };
        }

        public static List<Rating> ToRatingDao(this StudentWrapper model)
        {
            var ratings = new List<Rating>();

            if (!string.IsNullOrWhiteSpace(model.Math))

                model.Math.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Math,
                    }
                    ));

            if (!string.IsNullOrWhiteSpace(model.Physics))

                model.Physics.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Physics,
                    }));

            if (!string.IsNullOrWhiteSpace(model.Technology))

                model.Technology.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.Technology,
                    }));

            if (!string.IsNullOrWhiteSpace(model.PolishLanguage))

                model.PolishLanguage.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.PolishLanguage,
                    }));

            if (!string.IsNullOrWhiteSpace(model.ForeignLanguage))

                model.ForeignLanguage.Split(',').ToList().ForEach(x =>
                ratings.Add(
                    new Rating
                    {
                        Rate = int.Parse(x),
                        StudentId = model.Id,
                        SubjectId = (int)Subject.ForeignLanguage,
                    }));

            return ratings;
        }
    }
}
