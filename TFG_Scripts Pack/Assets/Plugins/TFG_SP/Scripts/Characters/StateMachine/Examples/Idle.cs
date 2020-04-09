namespace TFG_SP
{
    public class Idle : BaseState
    {

        System.Type TempState;

        public override void OnActivate()
        {

            TempState = null;

        }

        public override System.Type Perform()
        {

            return TempState;
        }



        public override void OnDeactivate()
        {

        }


        //Idle must be ended externally    
        public void EndIdle()
        {

            TempState = GetNextType();

        }


        System.Type GetNextType()
        {

            return NextState.GetType();

        }



    }
}
