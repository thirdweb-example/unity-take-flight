using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private Vector3 _offset = new Vector3(0, 2, -5);

    private Vector3 _currentVelocity;
    private Quaternion _currentRotation;

    private void LateUpdate()
    {
        Vector3 targetPosition = _player.position + _player.TransformDirection(_offset);
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _currentVelocity,
            0.1f
        );

        Quaternion targetRotation = _player.rotation;
        transform.rotation = QuaternionSmoothDamp(
            transform.rotation,
            targetRotation,
            ref _currentRotation,
            0.1f
        );
    }

    public static Quaternion QuaternionSmoothDamp(
        Quaternion rot,
        Quaternion target,
        ref Quaternion deriv,
        float time
    )
    {
        if (Time.deltaTime < Mathf.Epsilon)
            return rot;
        // account for double-cover
        var Dot = Quaternion.Dot(rot, target);
        var Multi = Dot > 0f ? 1f : -1f;
        target.x *= Multi;
        target.y *= Multi;
        target.z *= Multi;
        target.w *= Multi;
        // smooth damp (nlerp approx)
        var Result = new Vector4(
            Mathf.SmoothDamp(rot.x, target.x, ref deriv.x, time),
            Mathf.SmoothDamp(rot.y, target.y, ref deriv.y, time),
            Mathf.SmoothDamp(rot.z, target.z, ref deriv.z, time),
            Mathf.SmoothDamp(rot.w, target.w, ref deriv.w, time)
        ).normalized;

        // ensure deriv is tangent
        var derivError = Vector4.Project(new Vector4(deriv.x, deriv.y, deriv.z, deriv.w), Result);
        deriv.x -= derivError.x;
        deriv.y -= derivError.y;
        deriv.z -= derivError.z;
        deriv.w -= derivError.w;

        return new Quaternion(Result.x, Result.y, Result.z, Result.w);
    }
}
