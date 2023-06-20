using System;
using System.Collections.Generic;

namespace MyGate.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string? Token { get; set; }
}
