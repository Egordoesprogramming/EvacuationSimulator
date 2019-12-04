using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace MouseSet
{
    public class MouseSetPosition : MonoBehaviour
    {
        //https://answers.unity.com/questions/330661/setting-the-mouse-position-to-specific-coordinates.html
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
    }
}
