namespace Users.API.ViewModels;

public record UserViewModel(Guid Id, string Name, string Email, string Document)
{
    public string DocumentType
    {
        get
        {
            if (Document.Length == 14)
                return "cnpj";

            return "cpf";
        }
    }
}