using UnityEngine;


namespace TFG_SP
{
    //Keeps sprites in camera view

    [RequireComponent(typeof(SpriteRenderer))]
    public class KeepSpriteInCameraViewOrthographic : MonoBehaviour
    {
        private Camera MainCamera;
        private Vector2 screenBounds;
        private float objectWidth;
        private float objectHeight;

        [Tooltip("how much extra space do ya want?")]
        public Vector2 Margin = new Vector2(1, 1);


        void OnEnable()
        {

            MainCamera = Camera.main;

            if (!MainCamera.orthographic)
            {

                this.enabled = false;

            }

            screenBounds = MainCamera.transform.position + MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));

            screenBounds.x -= Margin.x;
            screenBounds.y -= Margin.y;


            objectWidth = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
            objectHeight = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.y;


        }


        void LateUpdate()
        {
            Vector3 viewPos = transform.position;
            viewPos.x = Mathf.Clamp(viewPos.x, (screenBounds.x * -1) + objectWidth, screenBounds.x - objectWidth);
            viewPos.y = Mathf.Clamp(viewPos.y, (screenBounds.y * -1) + objectHeight, screenBounds.y - objectHeight);
            transform.position = viewPos;

        }
    }
}