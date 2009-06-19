namespace nValid.Framework
{
    public interface IRule
    {
        string Message { get; }
        string Resource { get; }
        RuleExecutionResult Validate(object instance);
    }
}
