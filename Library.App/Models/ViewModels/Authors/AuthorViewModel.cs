﻿#nullable disable

namespace Library.App.Models.ViewModels.Authors;

public class AuthorViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string CreatorId { get; set; }
}
