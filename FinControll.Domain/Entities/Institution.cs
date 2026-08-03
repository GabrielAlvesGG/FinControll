using System;

namespace FinControll.Domain.Entites;

public class Institution
{
    public int Id { get; set; }

    public string CNPJ { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}
