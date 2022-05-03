using UnityEngine;

public class RecoilHead : MonoBehaviour
{
    Vector3 RecoilSmooth;
    Vector3 ToSmooth;
    [SerializeField] float Smooth = 7f;
    void LateUpdate()
    {
        ToSmooth = Vector3.Lerp(ToSmooth, Vector3.zero, (Time.deltaTime * Smooth * 3));
        RecoilSmooth = Vector3.Slerp(RecoilSmooth, ToSmooth, Time.deltaTime * Smooth);
        transform.localRotation = Quaternion.Euler(RecoilSmooth);
    }

    public void SetRecoil(float Multiplier, float Range)
    {
        Vector3 _Range = new Vector3(
            Random.Range(-Range, Range), //X
            Random.Range(-Range, Range), //Y
            0f //Z
        ) * Multiplier;

        RecoilSmooth += _Range;
    }
}
