using UnityEngine;
using System;

namespace EyeTracker.Infrastructure
{
    // 실시간 모니터링 및 진단 로그 관리 시스템
    // 터미널(ADB Logcat)을 통해 엔진의 내부 상태를 시각화합니다.
    public class MonitoringController : MonoBehaviour
    {
        [Header("Diagnostic Settings")]
        public bool logGazeData = false;
        public bool logClinicalThresholds = true;

        void Start()
        {
            Debug.Log("[Monitor] EyeTracker Monitoring System Initialized.");
            Debug.Log($"[Monitor] Device Name: {SystemInfo.deviceName}");
            Debug.Log($"[Monitor] Battery Level: {SystemInfo.batteryLevel:P}");
        }

        public void LogGaze(Vector3 direction, bool isHit)
        {
            if (logGazeData)
            {
                string hitStatus = isHit ? "HIT" : "MISS";
                Debug.Log($"[Gaze] Dir: {direction}, Status: {hitStatus}");
            }
        }

        public void LogClinical(float contrast, float diopter)
        {
            if (logClinicalThresholds)
            {
                Debug.Log($"[Clinical] Current Contrast: {contrast:P}, Required Diopter: {diopter}D");
            }
        }
        
        // 치명적 오류 발생 시 터미널에 사출
        public void LogCriticalError(string errorMsg)
        {
            Debug.LogError($"[CRITICAL] {DateTime.Now:HH:mm:ss} - {errorMsg}");
        }
    }
}
