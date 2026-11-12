var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

List<AssignmentDto> assignments =
[
    new(1, "Bai tap REST API", "Thiet ke endpoint chuan REST", true),
    new(2, "Bai tap Client", "Viet console client voi HttpClient", true),
    new(3, "Bai tap da dong", "Bai tap het han nop", false)
];

app.MapGet("/", () => Results.Ok(new { service = "Submission.Api", status = "running" }));

app.MapGet("/api/assignments", () => Results.Ok(assignments));

app.MapGet("/api/assignments/{assignmentId:int}", (int assignmentId) =>
{
    var assignment = assignments.FirstOrDefault(a => a.Id == assignmentId);
    return assignment is not null
        ? Results.Ok(assignment)
        : Results.NotFound(new ApiError("ASSIGNMENT_NOT_FOUND", "Khong tim thay bai tap."));
});

app.Run();

record AssignmentDto(int Id, string Title, string Description, bool IsOpen);
record ApiError(string Code, string Message);
