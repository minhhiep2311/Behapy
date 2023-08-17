﻿namespace Behapy.Presentation.Models;

public class Employee
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Position { get; set; } = null!;
}