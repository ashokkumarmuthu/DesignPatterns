namespace DesignPatterns.Behavioral.Command;

public interface ISmartHomeCommand
{
    string Description { get; }
    void Execute();
    void Undo();
}
