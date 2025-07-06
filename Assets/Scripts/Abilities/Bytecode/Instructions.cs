namespace Prototype.BytecodeTool
{

    public enum Instruction : byte
    {
        NOP = 0,
        LITERAL = 1,
        
        // STATS
        SET_HEALTH = 2,
        GET_HEALTH = 3,
        SET_TRUST = 4,
        GET_TRUST = 5,
        SET_STUBBORNESS = 6,
        GET_STUBBORNESS = 7,
        GET_SIDE = 8,


        // OPERATIONS
        ADD = 20,
        SUBTRACT = 21,
        MULTIPLY = 22,
        DIVIDE = 23,

        // EFFECTS
        PUSH = 30,
        PULL = 31,
    }
}