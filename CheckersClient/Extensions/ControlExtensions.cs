using System;
using System.Windows.Forms;

namespace CheckersClient.Extensions
{
    internal static class ControlExtensions
    {
        internal static void InvokeIfRequired(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
    }
}
