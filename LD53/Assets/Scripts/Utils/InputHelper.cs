using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InputHelper
{
    public static bool GetAnyKey(List<KeyCode> keys)
    {
        return keys.Any(key => Input.GetKey(key));
    }
    public static bool GetAnyKeyDown(List<KeyCode> keys)
    {
        return keys.Any(key => Input.GetKeyDown(key));
    }
}
