using Exercise.BizLogic.ViewModels.User;
using Exercise.Domain;
using System;

namespace Exercise.Tests.Fixture
{
    public static class UserFixture
    {
        public static CreateUserModel GetSingleCreateUser()
        {
            return new CreateUserModel()
            {
                Email = "Skullcandy Hesh Evo",
                Password = "$2a$11$5hnpm7KctsyXmf0mnKvJD.6qNv2lR4nNZ5BtPVerbEOzshiChEyrq"
            };
        }

        public static CreateUserModel GetSingleCreateUserWithErrors()
        {
            return new CreateUserModel()
            {
                Email = "sfdasfsdf",
                Password = string.Empty
            };
        }

        public static User GetUserEntity()
        {
            return new User("Skullcandy Hesh Evo", "$2a$11$5hnpm7KctsyXmf0mnKvJD.6qNv2lR4nNZ5BtPVerbEOzshiChEyrq");
        }
    }
}
