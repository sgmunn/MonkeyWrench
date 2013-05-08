// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Markers.cs" company="sgmunn">
//   (c) sgmunn 2013  
//
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//   documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//   the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
//   to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
//   The above copyright notice and this permission notice shall be included in all copies or substantial portions of 
//   the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
//   THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
//   AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//   CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
//   IN THE SOFTWARE.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MonkeyWrench.Navigation
{
    using System;
    using System.Collections.Generic;
    using Mono.TextEditor;
    
    // TODO: handle document load / unloads so that we can either add back in markers or remove from stack if we want
    //    -- we need to at least remove the marker and add a new one to the document so that removals work after doc close / open

    public static class NavigationMarkers
    {
        private static Stack<NavigationMarker> stack = new Stack<NavigationMarker>();

        public static void Push(string filename, TextSegmentMarker marker)
        {
            stack.Push(new NavigationMarker(filename, marker));
        }

        public static NavigationMarker Peek()
        {
            if (stack.Count > 0)
            {
                return stack.Peek();
            }

            return null;
        }
        
        public static NavigationMarker Pop()
        {
            if (stack.Count > 0)
            {
                return stack.Pop();
            }

            return null;
        }
    }
}
