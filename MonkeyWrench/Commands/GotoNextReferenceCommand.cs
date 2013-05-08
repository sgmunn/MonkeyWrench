// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GotoNextReferenceCommand.cs" company="sgmunn">
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

namespace MonkeyWrench.Commands
{
    using System;
    using MonkeyWrench.Navigation;
    using Mono.TextEditor;
    using MonoDevelop.Core;
    using MonoDevelop.Components.Commands;
    using MonoDevelop.Ide;

    public sealed class GotoNextReferenceCommand : CommandHandler
    {
        /*
         * find the thing we're on,
         * do a search (handle cancellation)
         * start looping thru the results
         * 
         * if the thing we are on is different to the current loop, then
         *     drop a marker
         *     create a new loop and make it the current
         * 
         * if we navigate back to where we were, we will make a new loop and search again from there.
         */
        protected override void Run()
        {
            var document = IdeApp.Workbench.ActiveDocument;
            if (document == null || document.FileName == FilePath.Null)
            {
                return;
            }

            NavigationTools.GotoToNextReferenceAtCaret(document);
        }

        protected override void Update(CommandInfo info)
        {
            var doc = IdeApp.Workbench.ActiveDocument;
            info.Enabled = doc != null && doc.GetContent<ITextEditorDataProvider>() != null;
        }
    }
}
