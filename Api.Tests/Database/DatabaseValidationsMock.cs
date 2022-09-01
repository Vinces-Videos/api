using Database;

namespace Database.Mongo;

public class DatabaseValidationsMock : IDatabaseValidations
{
    public bool IsValidId(string id) => true;
}