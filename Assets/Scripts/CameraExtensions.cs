using UnityEngine;

public static class CameraExtensions
{
    #region Orthographic
    public static Vector2 BoundsMin()
    {
        return (Vector2)Camera.main.transform.position - Extents();
    }

    public static Vector2 BoundsMax()
    {
        return (Vector2)Camera.main.transform.position + Extents();
    }

    public static Vector2 Extents()
    {
        if (Camera.main.orthographic)
        {
            return new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize);
        }
        else
        {
            Debug.LogError("Camera is not orthographic!");
            return new Vector2();
        }
    }
    #endregion
}
