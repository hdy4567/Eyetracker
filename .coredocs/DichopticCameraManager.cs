using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DichopticCameraManager : MonoBehaviour
{
    [Header("Eye Cameras")]
    public Camera leftEyeCamera;
    public Camera rightEyeCamera;

    [Header("Dichoptic Settings")]
    [Range(0, 1)] public float leftContrast = 1.0f;
    [Range(0, 1)] public float rightContrast = 1.0f;

    private Material dichopticMaterial;

    void Start()
    {
        // AR Camera 하위에 수동으로 눈별 카메라를 배치하거나, 
        // Single Pass Stereo 환경에서는 셰이더 파라미터만 제어함
        SetupDichopticMaterials();
    }

    void Update()
    {
        UpdateContrastParameters();
    }

    private void SetupDichopticMaterials()
    {
        // 씬 내의 모든 Dichoptic 셰이더를 사용하는 머티리얼 검색 또는 할당
        // 실제 구현에서는 전용 클래스나 이벤트를 통해 전달
    }

    private void UpdateContrastParameters()
    {
        // 전역 셰이더 변수로 눈별 대비값 사출
        Shader.SetGlobalFloat("_LeftContrast", leftContrast);
        Shader.SetGlobalFloat("_RightContrast", rightContrast);
    }

    // 사시 교정용: 억제안(Suppression Eye)의 대비를 높이고 우세안의 대비를 낮춤
    public void SetEyeBalance(float nonDominantContrast, float dominantContrast, bool isLeftEyeDominant)
    {
        if (isLeftEyeDominant)
        {
            leftContrast = dominantContrast;
            rightContrast = nonDominantContrast;
        }
        else
        {
            leftContrast = nonDominantContrast;
            rightContrast = dominantContrast;
        }
    }
}
