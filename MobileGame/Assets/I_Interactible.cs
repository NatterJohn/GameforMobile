using System;
using UnityEngine;
using UnityEngine.UIElements;

public interface I_Interactible
{
    void TapAt(Vector3 worldPosition);
    void MoveIt(Vector3 worldPosition);
    void PinchAt(float start, float end);
    void ScaleIt(float scaleDelta);
    void RotateAt(float rotationDelta);

    void ObjectSelected();
    void ObjectDeselected();
}

