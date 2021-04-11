using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class ViewportHandler : MonoBehaviour
{
    public Color wireColor;
    public float contentWidth = 1f;
    public float contentHeigth = 1f;

    private Camera cam;
    private float height;
    private float width;

    void Awake()
    {
        cam = GetComponent<Camera>();
        ComputeResolution();
    }

    void Update()
    {
        if (UnityEngine.Application.isEditor) {
            ComputeResolution();
        }
    }

    private void ComputeResolution()
    {
        if (cam.aspect > (contentWidth / contentHeigth)) {
            cam.orthographicSize = contentHeigth / 2f;
        } else {
            cam.orthographicSize = contentWidth / (2f * cam.aspect);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = wireColor;

        Matrix4x4 temp = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        if (cam.orthographic) {
            Gizmos.DrawWireCube(new Vector3(0f, 0f, 0f), new Vector3(2f * cam.orthographicSize * cam.aspect, 2f * cam.orthographicSize, 0f));
        }
        Gizmos.matrix = temp;    }
}
