using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace EyeTracker.UI
{
    // 헤더형 대시보드: 실시간 시능 데이터(대비 임계값, 시선 정확도) 정밀 시각화
    public class PerformanceDashboard : MonoBehaviour
    {
        [Header("Clinical Data Tracking")]
        // 1. 대비 감도(Contrast Threshold): Staircase 알고리즘(0.10)의 최종 도달값
        // 2. 시선 정확도(Gaze Accuracy): 칼만 필터(0.03)의 잔차(Residual) 데이터
        // 3. 순응 속도(Adaptation Rate): 훈련 시작 대비 성공 구간 도달 시간
        public RectTransform chartArea;
        public GameObject linePointPrefab;
        public Text infoText;

        private List<GameObject> activePoints = new List<GameObject>();

        // 데이터 로그 시스템으로부터 기록을 불러와 그래프를 그림
        public void RenderPerformanceGraph(List<float> records)
        {
            ClearGraph();
            
            if (records == null || records.Count == 0)
            {
                infoText.text = "훈련 데이터가 부족합니다.";
                return;
            }

            float width = chartArea.rect.width;
            float height = chartArea.rect.height;
            float xStep = width / (records.Count > 1 ? records.Count - 1 : 1);

            for (int i = 0; i < records.Count; i++)
            {
                float x = i * xStep;
                float y = records[i] * height; // 대비율(0~1) 기준 스케일링

                CreatePoint(new Vector2(x, y));
            }
            
            infoText.text = "최근 10세션 대비 감도 변화 그래프";
        }

        private void CreatePoint(Vector2 pos)
        {
            GameObject point = Instantiate(linePointPrefab, chartArea);
            RectTransform rt = point.GetComponent<RectTransform>();
            rt.anchoredPosition = pos;
            activePoints.Add(point);
        }

        private void ClearGraph()
        {
            foreach (var p in activePoints) if (p != null) Destroy(p);
            activePoints.Clear();
        }
        
        public void ShowDashboard()
        {
            this.gameObject.SetActive(true);
            // 의 더미 데이터 테스트 기능 추가 가능
            RenderPerformanceGraph(new List<float> { 0.8f, 0.75f, 0.6f, 0.5f, 0.45f, 0.3f, 0.25f });
        }
    }
}
