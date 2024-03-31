namespace CfpService.Models;

public class Application
{
    public Guid Id { get; set; }
    public Guid Author { get; set; }
    public string Activity { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Outline { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? SubmittedAt { get; set; }
}