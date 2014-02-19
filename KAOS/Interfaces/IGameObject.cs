
namespace KAOS.Interfaces
{
    public interface IGameObject
    {
        void Update(float elapsedTime, float aspect);
        void Render();
    }
}
