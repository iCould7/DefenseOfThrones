using System;
using ICouldGames.DefenseOfThrones.World.Design.Constants;
using ICouldGames.DefenseOfThrones.World.Level.Self.DirectoryPath.Constants;
using ICouldGames.DefenseOfThrones.World.Level.Self.Enums;
using ICouldGames.DefenseOfThrones.World.Level.Self.Id;

namespace ICouldGames.DefenseOfThrones.World.Level.Self.DirectoryPath
{
    public static class WorldLevelPathUtil
    {
        public static string GetDesignedPrefabPath(WorldLevelId levelId)
        {
            return GetDesignedPrefabDirectoryPath(levelId.Type) + "/" + GetDesignedPrefabName(levelId) + ".prefab";
        }

        public static string GetDesignedPrefabDirectoryPath(WorldLevelType levelType)
        {
            if (levelType == WorldLevelType.Normal)
                return WorldDesignConstants.DESIGNED_NORMAL_LEVELS_DIRECTORY;
            if (levelType == WorldLevelType.Endless)
                return WorldDesignConstants.DESIGNED_ENDLESS_LEVELS_DIRECTORY;

            throw new Exception("Not supported WorldLevelType");
        }

        public static string GetPrefabName(WorldLevelId levelId)
        {
            if (levelId.Type == WorldLevelType.Normal)
                return "WorldLevel_Normal_" + levelId.Subtype;
            if (levelId.Type == WorldLevelType.Endless)
                return "WorldLevel_Endless_" + levelId.Subtype;

            throw new Exception("Not supported WorldLevelType");
        }

        public static string GetProcessedPrefabName(WorldLevelId levelId)
        {
            return "Processed_" + GetPrefabName(levelId);
        }

        public static string GetDesignedPrefabName(WorldLevelId levelId)
        {
            return "Designed_" + GetPrefabName(levelId);
        }

        public static string GetProcessedPrefabPath(WorldLevelId levelId)
        {
            return GetProcessedPrefabDirectoryPath(levelId.Type) + "/" + GetProcessedPrefabName(levelId) + ".prefab";
        }

        public static string GetProcessedPrefabResourcesPath(WorldLevelId levelId)
        {
            return GetProcessedPrefabResourcesDirectoryPath(levelId.Type) + "/" + GetProcessedPrefabName(levelId);
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