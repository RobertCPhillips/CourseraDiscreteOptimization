namespace Tsp
{
    public interface ITspSolver
    {
        TspSolution Execute(TsPoint[] points);
    }
}