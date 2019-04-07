namespace CSP.Interfaces
{
    public interface IConstraint
    {
        bool IsSatisfied();
        bool IsConsistent();
    }
}
