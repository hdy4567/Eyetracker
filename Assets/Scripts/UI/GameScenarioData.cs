using UnityEngine;
using UnityEngine.Events;

namespace EyeTracker.UI
{
    // 0.18 단계: 게임 시나리오 데이터 아키텍처 (ScriptableObject)
    // 새로운 게임 추가 시 코딩 없이 '데이터 파일'만 생성하여 카드를 추가할 수 있게 합니다.
    [CreateAssetMenu(fileName = "NewGameScenario", menuName = "EyeTracker/GameScenarioData")]
    public class GameScenarioData : ScriptableObject
    {
        [Header("Card Metadata")]
        public string gameTitle;
        public string gameDescription;
        public Sprite gameIcon;

        [Header("Executive Settings")]
        public string sceneName; // 전환할 씬 이름 (필요시)
        public GameType type;

        public enum GameType
        {
            Shooter,
            Pursuit,
            Saccade,
            Flicker,
            Crowding,
            Priming,
            PressureRelief,
            Custom // 향후 추가될 범용 타입
        }
    }
}
