using System;

namespace EnemyManager
{
    /// <summary>
    /// Defines the expected behavior from a difficulty level
    /// </summary>
    public interface IDifficulty
    {
        /// <summary>
        /// Returns the minimum amount of enemies to spawn
        /// </summary>
        int MinEnemyCount();

        /// <summary>
        /// Returns the maximum amount of enemies to spawn
        /// </summary>
        int MaxEnemyCount();

        /// <summary>
        /// Returns the spawn interval minimum amount of seconds
        /// </summary>
        float MinSpawnSeconds();

        /// <summary>
        /// Returns the spawn interval maximum amount of seconds 
        /// </summary>
        float MaxSpawnSeconds();

        /// <summary>
        /// Returns the minimum amount of gesture patterns
        /// </summary>
        int MinGesturePatterns();

        /// <summary>
        /// Returns the maximum amount of gesture patterns 
        /// </summary>
        int MaxGesturePatterns();

        /// <summary>
        /// Returns the enemy speed multiplier  
        /// </summary>
        float EnemySpeedMultiplier();
    }

    /// <summary>
    /// Enumerates the available levels of difficulty
    /// </summary>
    public enum DifficultyLevel
    {
        Easy,
        Medium,
        Hard,
    }

    public static class DifficultyBuilder
    {
        public static IDifficulty Build(DifficultyLevel level)
        {
            switch (level)
            {
                case DifficultyLevel.Easy:
                    return new Easy();
                case DifficultyLevel.Medium:
                    return new Medium();
                case DifficultyLevel.Hard:
                    return new Hard();
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, "Unexpected level of difficulty");
            }
        }
    }

    public class Easy : IDifficulty
    {
        public int MinEnemyCount() => 1;
        public int MaxEnemyCount() => 1;
        public float MinSpawnSeconds() => 1.5f;
        public float MaxSpawnSeconds() => 2f;
        public int MinGesturePatterns() => 1;
        public int MaxGesturePatterns() => 2;
        public float EnemySpeedMultiplier() => 0.9f;
    }

    public class Medium : IDifficulty
    {
        public int MinEnemyCount() => 1;
        public int MaxEnemyCount() => 1;
        public float MinSpawnSeconds() => 1.2f;
        public float MaxSpawnSeconds() => 1.5f;
        public int MinGesturePatterns() => 1;
        public int MaxGesturePatterns() => 3;
        public float EnemySpeedMultiplier() => 1f;
    }

    public class Hard : IDifficulty
    {
        public int MinEnemyCount() => 1;
        public int MaxEnemyCount() => 1;
        public float MinSpawnSeconds() => 1f;
        public float MaxSpawnSeconds() => 1.2f;
        public int MinGesturePatterns() => 2;
        public int MaxGesturePatterns() => 3;
        public float EnemySpeedMultiplier() => 1.2f;
    }
}