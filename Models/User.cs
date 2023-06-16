using System;
using System.Collections.Generic;

namespace MyWayNet.Models;

public partial class User
{
    public long UserId { get; set; }

    public string UserFirstname { get; set; } = null!;

    public string UserLastname { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public DateTime UserBirthdate { get; set; }

    public string UserAddress { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public string UserPassword { get; set; } = null!;
}
