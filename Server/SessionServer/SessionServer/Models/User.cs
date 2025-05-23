﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SessionServer.Models;

[Table("user")]
public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}