using System;

namespace Variables
{
    [Serializable]
    public class IntReference : Reference<int>
    {
        public IntVariable variableReference;
        
        public override Variable<int> Variable => variableReference;
    }
}