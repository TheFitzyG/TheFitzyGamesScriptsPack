using UnityEngine;

namespace TFG_SP
{

    [RequireComponent(typeof(Camera))]
    public class CameraBoundsViewerOrthographic : MonoBehaviour
    {

        //Shows the viewport of an orthographic camera without it being selected

        Vector2 size;
        Vector3 position;
        Camera Cam;

        [Tooltip("Pick a color, any color")]
        public Color Outline = Color.white;


        private void OnDrawGizmos()
        {
#if UNITY_EDITOR

            Cam = GetComponent<Camera>();

            if (Cam.orthographic)
            {

                size.y = (Cam.orthographicSize);
                size.x = (Cam.aspect * Cam.orthographicSize);

                position = Cam.transform.position;
                position -= new Vector3(size.x, size.y, 0);

                size *= 2;



            }


            Rect rect = new Rect(position, size);

            UnityEditor.Handles.color = Color.white;

            UnityEditor.Handles.DrawSolidRectangleWithOutline(rect, Color.clear, Outline);


#endif

        }



    }
}
