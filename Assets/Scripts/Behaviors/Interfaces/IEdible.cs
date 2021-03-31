public interface IEdible
{
    bool currentlyEdible {get; set;}
    int EatenScore();

    void EatenAction();
}