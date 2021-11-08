using Project.Map;

namespace Project.Actors
{
    //Implements the FOV methods for the ActorTile
    public interface IFov
    {
        public Cell[] GetVisibleCells();
    }
}