using Database;

namespace Database;

public class DatabaseValidationsMock : IDatabaseValidations
{
    public bool IsValidId(string id) => true;
}