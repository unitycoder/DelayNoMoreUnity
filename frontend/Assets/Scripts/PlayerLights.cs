using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using DG.Tweening.Core;

public class PlayerLights : MonoBehaviour {

    public Light2D front;
    public Light2D back;
    private DOGetter<Vector4> frontIntensityGetter, backIntensityGetter;
    private DOSetter<Vector4> frontIntensitySetter, backIntensitySetter;
    private Vector4 positiveVal = new Vector4(1.0f, 0.2f, 64, 120);
    private Vector4 negativeVal = new Vector4(0.0f, 1.0f, 0, 0);
    private float switchDurationSeconds = 0.8f;
    private int dirX;

    // Start is called before the first frame update
    void Start() {
        frontIntensityGetter = () => new Vector4(front.intensity, front.falloffIntensity, front.pointLightInnerRadius, front.pointLightOuterRadius);
        backIntensityGetter = () => new Vector4(back.intensity, back.falloffIntensity, back.pointLightInnerRadius, back.pointLightOuterRadius);
        frontIntensitySetter = (x) => {
            front.intensity = x[0];
            front.falloffIntensity = x[1];
            front.pointLightOuterRadius = x[3];
            front.pointLightInnerRadius = x[2];
        };
        backIntensitySetter = (x) => {
            back.intensity = x[0];
            back.falloffIntensity = x[1];
            back.pointLightOuterRadius = x[3];
            back.pointLightInnerRadius = x[2];
        };
        DOTween.To(frontIntensityGetter, frontIntensitySetter, positiveVal, switchDurationSeconds);
        DOTween.To(backIntensityGetter, backIntensitySetter, negativeVal, switchDurationSeconds);
        /**
        TODO: Reuse the animation objects by "SetAutoKill(false)", e.g. 

        frontPositive = DOTween.To(frontIntensityGetter, frontIntensitySetter, positiveVal, switchDurationSeconds).SetAutoKill(false);
        */
    }

    // Update is called once per frame
    void Update() {
    }

    public void setDirX(int newDirX) {
        if (dirX != newDirX) {
            if (0 < newDirX) {
                DOTween.To(frontIntensityGetter, frontIntensitySetter, positiveVal, switchDurationSeconds);
                DOTween.To(backIntensityGetter, backIntensitySetter, negativeVal, switchDurationSeconds);
            } else if (0 > newDirX) {
                DOTween.To(frontIntensityGetter, frontIntensitySetter, negativeVal, switchDurationSeconds);
                DOTween.To(backIntensityGetter, backIntensitySetter, positiveVal, switchDurationSeconds);
            }
        }
        dirX = newDirX;
    }
}
