﻿using Behapy.Presentation.Areas.Identity.Data;

namespace Behapy.Presentation.Models;

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Position { get; set; } = null!;

    public string? UserId { get; set; }
    public User? User { get; set; }
}