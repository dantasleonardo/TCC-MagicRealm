using UnityEngine.Rendering;

namespace SaveSystem
{
    public class User
    {
        public string userName;
        public LanguageKey languageKey;
        public float volume;
        public bool[] levelsCompleted;

        public User(string userName, LanguageKey languageKey, float volume, bool[] levelsCompleted)
        {
            this.userName = userName;
            this.languageKey = languageKey;
            this.volume = volume;
            this.levelsCompleted = levelsCompleted;
        }
    }
}
