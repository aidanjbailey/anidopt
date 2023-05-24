﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Anidopt.Models;

public class AnidoptUser : IdentityUser
{
    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public virtual ICollection<UserOrganisationLink>? UserOrganisationLinks { get; set; }
}