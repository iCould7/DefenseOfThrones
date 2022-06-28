using System;
using ICouldGames.DefenseOfThrones.World.Design.Constants;
using ICouldGames.DefenseOfThrones.World.Level.DirectoryPath.Constants;
using ICouldGames.DefenseOfThrones.World.Level.Enums;
using ICouldGames.DefenseOfThrones.World.Level.Id;

namespace ICouldGames.DefenseOfThrones.World.Level.DirectoryPath
{
    public static class WorldLevelPathUtil
    {
        public static string GetDesignedPrefabPath(WorldLevelType levelType, int levelSubtype)
        {
            return GetDesignedPrefabDirectoryPath(levelType) + "/" + GetDesignedPrefabName(levelType, levelSubtype) + ".prefab";
        }

        public static string GetDesignedPrefabDirectoryPath(WorldLevelType levelType)
        {
            if (levelType == WorldLevelType.Normal)
                return WorldDesignConstants.DESIGNED_NORMAL_LEVELS_DIRECTORY;
            if (levelType == WorldLevelType.Endless)
                return WorldDesignConstants.DESIGNED_ENDLESS_LEVELS_DIRECTORY;

            throw new Exception("Not supported WorldLevelType");
        }

        public static string GetPrefabName(WorldLevelType levelType, int levelSubtype)
        {
            if (levelType == WorldLevelType.Normal)
                return "WorldLevel_Normal_" + levelSubtype;
            if (levelType == WorldLevelType.Endless)
                return "WorldLevel_Endless_" + levelSubtype;

            throw new Exception("Not supported WorldLevelType");
        }

        public static string GetProcessedPrefabName(WorldLevelType levelType, int levelSubtype)
        {
            return "Processed_" + GetPrefabName(levelType, levelSubtype);
        }

        public static string GetDesignedPrefabName(WorldLevelType levelType, int levelSubtype)
        {
            return "Designed_" + GetPrefabName(levelType, levelSubtype);
        }

        public static string GetProcessedPrefabPath(WorldLevelType levelType, int levelSubtype)
        {
            return GetProcessedPrefabDirectoryPath(levelType) + "/" + GetProcessedPrefabName(levelType, levelSubtype) + ".prefab";
        }

        public static string GetProcessedPrefabResourcesPath(WorldLevelId levelId)
        {
            return GetProcessedPrefabResourcesDirectoryPath(levelId.Type) + "/" + GetProcessedPrefabName(levelId.Type, levelId.Subtype);
        }

        public static string GetProcessedPrefabDirectoryPath(WorldLevelType levelType)
        {
            if (levelType == WorldLevelType.Normal)
                return WorldDesignConstants.PROCESSED_NORMAL_LEVELS_DIRECTORY;
            if (levelType == WorldLevelType.Endless)
                return WorldDesignConstants.PROCESSED_ENDLESS_LEVELS_DIRECTORY;

            throw new Exception("Not supported WorldLevelType");
        }

        public static string GetProcessedPrefabResourcesDirectoryPath(WorldLevelType levelType)
        {
            if (levelType == WorldLevelType.Normal)
                return WorldLevelResourcesPathConstants.PROCESSED_NORMAL_LEVELS_RESOURCES_DIRECTORY;
            if (levelType == WorldLevelType.Endless)
                return WorldLevelResourcesPathConstants.PROCESSED_ENDLESS_LEVELS_RESOURCES_DIRECTORY;

            throw new Exception("Not supported WorldLevelType");
        }
    }
}