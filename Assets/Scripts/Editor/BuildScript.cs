using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace EyeTracker.Editor
{
    // 터미널(CLI) 환경에서 유니티 빌드를 자동화하기 위한 스크립트
    // 사용법: Unity.exe -batchmode -quit -executeMethod EyeTracker.Editor.BuildScript.PerformBuild -projectPath [프로젝트경로]
    public class BuildScript
    {
        public static void PerformBuild()
        {
            string[] scenes = { "Assets/Scenes/MainTrainingScene.unity" };
            string buildPath = "Builds/Android/EyeTracker.apk";

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = scenes;
            buildPlayerOptions.locationPathName = buildPath;
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = BuildOptions.None;

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("Build failed");
            }
        }
    }
}
