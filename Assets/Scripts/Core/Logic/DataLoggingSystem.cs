using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;

namespace EyeTracker.Core.Logic
{
    // 0.08 단계: 데이터 로깅 및 분석 모듈
    // 사용자의 훈련 성과를 기록하여 RAG 최적화와 시각 개선 데이터로 활용합니다.
    [Serializable]
    public class SessionData
    {
        public string timestamp;
        public float duration;
        public float finalContrast;
        public int totalHits;
    }

    public class DataLoggingSystem : MonoBehaviour
    {
        private string logPath;
        private SessionData currentSession = new SessionData();

        void Awake()
        {
            logPath = Path.Combine(Application.persistentDataPath, "TrainingLogs.json");
        }

        public void LogSessionSummary(float duration, float contrast, int hits)
        {
            currentSession.timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            currentSession.duration = duration;
            currentSession.finalContrast = contrast;
            currentSession.totalHits = hits;

            SaveToFile();
        }

        private void SaveToFile()
        {
            string json = JsonUtility.ToJson(currentSession, true);
            File.AppendAllText(logPath, json + "\n");
            Debug.Log($"Session logged to: {logPath}");
        }

        // Firebase 연동 인터페이스 (향후 확장용)
        public void SyncToCloud()
        {
            // 0.08 단계: Firebase SDK를 통한 클라우드 업로드 구현 예정
            Debug.Log("Syncing data to Firebase Cloud...");
        }
    }
}
