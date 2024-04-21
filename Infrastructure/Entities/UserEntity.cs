using Microsoft.AspNetCore.Identity;

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Infrastructure.Entities;

public class UserEntity : IdentityUser
{
    [ProtectedPersonalData]
    public string FirstName { get; set; } = null!;

    [ProtectedPersonalData]
    public string LastName { get; set; } = null!;

    public string? Biography { get; set; }

    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public bool IsExternalAccount { get; set; } = false;


    [Column(TypeName = "nvarchar(max)")]
    public string SavedCoursesIdListJson { get; set; } = "[]";

    [NotMapped] 
    public List<int> SavedCoursesIdList
    {
        get => JsonSerializer.Deserialize<List<int>>(SavedCoursesIdListJson)!;
        set => SavedCoursesIdListJson = JsonSerializer.Serialize(value);
    }
}
