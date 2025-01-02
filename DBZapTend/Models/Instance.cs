using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DBZapTend.Models;

public partial class Instance
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? UserIduser { get; set; }
    [JsonIgnore]
    public virtual User? UserIduserNavigation { get; set; }
}
