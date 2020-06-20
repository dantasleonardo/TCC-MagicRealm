namespace MissionScripts
{
    public interface IMission
    {
        void Init(Mission mission);
        bool MissionCompleted();
    }
}