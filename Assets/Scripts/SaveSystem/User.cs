using System;

namespace SaveSystem
{
    [Serializable]
    public class User
    {
        public LanguageKey languageKey;
        public float volume;
        public bool[] levelsCompleted;

        public User(LanguageKey languageKey, float volume, bool[] levelsCompleted)
        {
            this.languageKey = languageKey;
            this.volume = volume;
            this.levelsCompleted = levelsCompleted;
        }

        public override string ToString()
        {
            return
                $"Language: {languageKey}, level 1 is completed: {levelsCompleted[0]}, level 2 is completed: {levelsCompleted[1]}, level 3 is completed: {levelsCompleted[2]}";
        }
    }
}