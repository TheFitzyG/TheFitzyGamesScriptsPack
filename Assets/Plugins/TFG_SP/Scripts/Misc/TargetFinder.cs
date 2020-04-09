using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TFG_SP
{
    public class TargetFinder : MonoBehaviour
    {

        [Header("Accuisition")]
        public float ViewRadius = 3f;
        [Range(0, 360)]
        public float ViewAngle = 50f;
        public LayerMask TargetMask;
        public LayerMask ObstacleMask;

        public List<Transform> VisibleTargetPoints = new List<Transform>();
        public int ChecksPerSecond = 5;

        [Header("Selection")]
        public static Transform Target;

        [Header("Default")]
        public Vector3 DefaultTargetOffset;
        private Transform defaultTarget;

        private void Start()
        {

            StartCoroutine(CheckTargets());
            defaultTarget = new GameObject().transform;

        }

        private void Update()
        {

            defaultTarget.position = transform.TransformPoint(Vector3.forward + DefaultTargetOffset);

            if (VisibleTargetPoints.Count > 0)
            {

                Target = VisibleTargetPoints[TargetIndex()].transform;

            }
            else
            {

                Target = defaultTarget;

            }

        }


        IEnumerator CheckTargets()
        {

            while (true)
            {

                VisibleTargetPoints.Clear();
                FindVisibleTargets();

                yield return new WaitForSeconds(1f / ChecksPerSecond);

            }


        }



        private void FindVisibleTargets()
        {

            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, ViewRadius, TargetMask);

            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {

                Transform target = targetsInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                try
                {
                    if (Vector3.Angle(transform.forward, dirToTarget) < ViewAngle / 2)
                    {

                        if (target.gameObject.GetComponent<Renderer>().isVisible)
                        {

                            float distToTarget = Vector3.Distance(transform.position, target.position);

                            if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, ObstacleMask))
                            {


                                VisibleTargetPoints.Add(target.transform);


                            }
                        }


                    }
                }
                catch
                {

                    Debug.Log("Something went wrong Identifying: " + targetsInViewRadius[i].gameObject);

                }

            }


        }


        int TargetIndex()
        {


            if (VisibleTargetPoints.Count < 1)
            {

                return -1;

            }

            float[] distances = new float[VisibleTargetPoints.Count];

            for (int i = 0; i < VisibleTargetPoints.Count; i++)
            {
                distances[i] = Vector2.Distance(Camera.main.WorldToScreenPoint(VisibleTargetPoints[i].transform.position), new Vector2(Screen.width / 2, Screen.height / 2));
            }

            float minDistance = Mathf.Min(distances);
            int index = 0;



            for (int i = 0; i < distances.Length; i++)
            {
                if (minDistance == distances[i])
                    index = i;
            }

            return index;

        }



        public Vector3 DirFromAngle(float angleInDegrees, bool globalAngle = false)
        {


            if (!globalAngle)
            {

                angleInDegrees += transform.eulerAngles.y;

            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

        }

        private void OnDrawGizmos()
        {

            if (Application.isPlaying)
            {

                if (Target != null)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawWireSphere(Target.position, 0.3f);

                    Gizmos.DrawLine(transform.position, Target.position);

                    Gizmos.color = Color.magenta;
                }


                Gizmos.DrawWireSphere(defaultTarget.position, 0.2f);
            }


        }

    }
}
