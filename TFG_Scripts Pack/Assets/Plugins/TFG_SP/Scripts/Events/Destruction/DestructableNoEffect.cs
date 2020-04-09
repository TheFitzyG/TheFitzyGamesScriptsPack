namespace TFG_SP
{
    public class DestructableNoEffect : DestructableBaseClass
    {
        public override void DestroyMe(float time)
        {

            Destroy(gameObject, time);

        }


    }
}
