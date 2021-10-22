using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessInputs : MonoBehaviour
{

    public bool IsLeftKeyDown()
    {
        return Input.GetKeyDown(KeyCode.A);
    }
    public bool IsRightKeyDown()
    {
        return Input.GetKeyDown(KeyCode.D);
    }
    public bool IsJumpKeyDown()
    {
        return Input.GetKeyDown(KeyCode.W);
    }
    public bool IsStartKeyDown()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
    public bool IsRollKeyDown()
    {
        return Input.GetKeyDown(KeyCode.S);
    }

}
