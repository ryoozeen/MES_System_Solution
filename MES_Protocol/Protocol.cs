// Shared protocol models aligned to line-delimited JSON used by server/clients
using System.Text.Json;

namespace MES.Common;

public sealed class ProfileRow
{
    public string employee_id { get; set; } = "";
    public string name { get; set; } = "";
    public string department { get; set; } = "";
    public string position { get; set; } = "";
    public string? equipment_id { get; set; }
    public int role { get; set; } = 0;
    public string status { get; set; } = "재직중";
}

public sealed class ProductionData
{
    public string equipment_id { get; set; } = "";
    public string equipment_name { get; set; } = ""; // ⭐ 설비명 추가 (equipment 테이블에서 조회)
    public int production_count { get; set; } = 0;
    public int faulty_count { get; set; } = 0;
    public double faulty_rate { get; set; } = 0;
    public int operating_time { get; set; } = 0;
    public int downtime { get; set; } = 0;
    public int total_time { get; set; } = 0;
    public double operating_rate { get; set; } = 0;
    public string current_status { get; set; } = "미가동";
}

public static class MsgTypes
{
    public const string Ping = "Ping";                 // { type, body: { ts } }
    public const string Pong = "Pong";                 // { type, body: { ts } }

    public const string LoginReq = "LoginReq";         // { type, body: { id } }
    public const string LoginAck = "LoginAck";         // { type, body: { ok, id } }

    public const string QueryProfileReq = "QueryProfileReq";   // { type, body: { employee_id } }
    public const string QueryProfileAck = "QueryProfileAck";   // { type, body: { ok, employee_id?, name?, department?, position? } }

    public const string EquipmentRegister = "EquipmentRegister"; // { type, body: { equipment_id, name?, model? } }
    public const string EquipmentRegisterAck = "EquipmentRegisterAck"; // { type, body: { ok } }

    public const string StateReport = "State.Report";  // { type, body: { equipment_id, state, reason? } }

    public const string UpdateProductionData = "UpdateProductionData";  // Equipment → Server
    public const string GetProductionDataReq = "GetProductionDataReq";  // Client → Server
    public const string GetProductionDataAck = "GetProductionDataAck";  // Server → Client
    
    public const string GetEquipmentDataReq = "GetEquipmentDataReq";    // Equipment → Server (오늘 데이터 조회)
    public const string GetEquipmentDataAck = "GetEquipmentDataAck";    // Server → Equipment

    public const string RecordStopAlertReq = "RecordStopAlertReq";      // Equipment → Server (멈춤 알림 기록)
    public const string RecordStopAlertAck = "RecordStopAlertAck";      // Server → Equipment

    public const string CommandReq = "Command.Request";   // generic future use
    public const string CommandAck = "Command.Result";

    public const string Error = "Error";               // { type, body: { code, message } }
}

// Minimal DTOs for common exchanges
public record LoginReq(string id);
public record LoginAck(bool ok, string id);

public record QueryProfileReq(string employee_id);
public record QueryProfileAck(bool ok, string? employee_id, string? name, string? department, string? position, int role = 0);
public record AddProfileReq(string employee_id, string name, string department, string position, string? equipment_id);
public record AddProfileAck(bool ok, string? message);
public record UpdateProfileReq(string employee_id, string name, string department, string position, string? equipment_id);
public record UpdateProfileAck(bool ok, string? message);
public record DeleteProfileReq(string employee_id);
public record DeleteProfileAck(bool ok, string? message);
public record ListProfilesReq();
public record ListProfilesAck(bool ok, ProfileRow[] profiles);
public record GetNextEmployeeIdReq();
public record GetNextEmployeeIdAck(bool ok, string? next_employee_id);
public record RetireEmployeeReq(string employee_id);
public record RetireEmployeeAck(bool ok, string? message);
public record GetEquipmentListReq();
public record GetEquipmentListAck(bool ok, string[] equipment_ids);

public record EquipmentRegister(string equipment_id, string? equipment_name, string? model);
public record EquipmentRegisterAck(bool ok);

public record StateReport(string equipment_id, string state, string? reason);

public record UpdateProductionDataReq(
    string equipment_id,
    int production_count,
    int faulty_count,
    double faulty_rate,
    int operating_time,
    int downtime,
    int total_time,
    double operating_rate,
    string current_status
);

public record GetProductionDataReq();
public record GetProductionDataAck(bool ok, ProductionData[] data);

public record RecordStopAlertReq(string equipment_id, string employee_id, string stop_reason);
public record RecordStopAlertAck(bool ok);

public record PingPong(long ts);
public record ErrorBody(string code, string message);

public static class JsonMsg
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = false
    };

    public static string Wrap<T>(string type, T body)
        => JsonSerializer.Serialize(new { type, body }, Options);

    public static bool TryGetType(string json, out string? type)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("type", out var tp))
            {
                type = tp.GetString();
                return true;
            }
        }
        catch { }
        type = null;
        return false;
    }

    public static T? ReadBody<T>(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (!doc.RootElement.TryGetProperty("body", out var body)) return default;
            return body.Deserialize<T>(Options);
        }
        catch { return default; }
    }
}
