using UnityEngine;

namespace TFG_SP
{

    //classes for static behavious
    public static class RandomBy
    {

        //class for random number  generation
        public static bool Percent(float chance)
        {

            float clampedChance = Mathf.Clamp(chance, 0f, 100f);

            return clampedChance > UnityEngine.Random.Range(0, 100f);

        }

        public static int ZeroMax(int Max)
        {

            return (Random.Range(0, Max));

        }

    }

    public static class Curve
    {


        //Sourced from Mix and Jam : https://youtu.be/M-3P59GtRW4

        public static Vector3 GetQuadraticCurvePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;
            return (uu * p0) + (2 * u * t * p1) + (tt * p2);
        }

    }


    public static class Checks
    {


        //class for checking layers and stuff
        public static bool CompareLayers(LayerMask mask, GameObject other)
        {

            if ((mask & 1 << other.layer) == 1 << other.layer)
            {

                return true;

            }

            return false;

        }


        public static bool LineOfSight2D(LayerMask blockingMask, GameObject from, GameObject to)
        {

            Vector3 direction = (to.transform.position - from.transform.position).normalized;

            float distance = Vector2.Distance(to.transform.position, from.transform.position);

            RaycastHit2D sightTest = Physics2D.Raycast(from.transform.position, direction, distance, blockingMask);

            if (sightTest.collider == null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

    }



}
