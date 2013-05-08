// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReferenceNavigationLoop.cs" company="sgmunn">
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
    using MonkeyWrench.CodeDom;

    public sealed class ReferenceNavigationLoop
    {
        private static ReferenceNavigationLoop current;

        private int currentIndex = -1;

        private ReferenceNavigationLoop()
        {
            this.Items = new List<NavigationItem>();
        }

        public static ReferenceNavigationLoop Current
        {
            get
            {
                return current;
            }
        }

        public List<NavigationItem> Items { get; private set; }

        public object SearchTarget { get; private set; }

        public static void Clear()
        {
            current = null;
        }

        public static ReferenceNavigationLoop New(object searchTarget)
        {
            current = new ReferenceNavigationLoop();
            current.SearchTarget = searchTarget;
            return current;
        }

        public static NavigationItem GetNextSearchTarget(object searchTarget)
        {
            var currentLoop = ReferenceNavigationLoop.Current;
            if (currentLoop != null)
            {
                if (CodeDomHelpers.AreEqualSearchTargets(currentLoop.SearchTarget, searchTarget))
                {
                    return currentLoop.Next();
                }
            }

            return NavigationItem.Empty;
        }
        
        public static NavigationItem GetPreviousSearchTarget(object searchTarget)
        {
            var currentLoop = ReferenceNavigationLoop.Current;
            if (currentLoop != null)
            {
                if (CodeDomHelpers.AreEqualSearchTargets(currentLoop.SearchTarget, searchTarget))
                {
                    return currentLoop.Prev();
                }
            }

            return NavigationItem.Empty;
        }

        public NavigationItem Next()
        {
            if (this.Items.Count == 0)
            {
                return NavigationItem.Empty;
            }

            if (this.currentIndex == this.Items.Count - 1)
            {
                this.currentIndex = 0;
            }
            else
            {
                this.currentIndex++;
            }

            return this.Items[this.currentIndex];
        }

        public NavigationItem Prev()
        {
            if (this.Items.Count == 0)
            {
                return NavigationItem.Empty;
            }

            if (this.currentIndex <= 0)
            {
                this.currentIndex = this.Items.Count - 1;
            }
            else
            {
                this.currentIndex--;
            }

            return this.Items[this.currentIndex];
        }

        public void MakeCurrent(NavigationItem item)
        {
            var index = this.Items.IndexOf(item);
            if (index < 0)
            {
                throw new InvalidOperationException("item is not in this navigation loop");
            }

            this.currentIndex = index;
        }
    }
}
