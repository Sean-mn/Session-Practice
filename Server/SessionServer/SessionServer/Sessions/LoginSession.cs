using System.Text.Json;

namespace SessionServer.Sessions;

public class LoginSession : Session
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;

    public override string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static LoginSession FromJson(string json)
    {
        return JsonSerializer.Deserialize<LoginSession>(json);
    }
}