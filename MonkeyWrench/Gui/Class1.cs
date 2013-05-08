// --------------------------------------------------------------------------------------------------------------------
// <copyright file=".cs" company="sgmunn">
//   (c) sgmunn 2013  
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
using System;
using Mono.TextEditor;
using System.Diagnostics;

namespace MonkeyWrench.Gui
{
    // TODO: fix up drawing of markers

    public class NavigationMarkerTextSegmentMarker : TextSegmentMarker
    {
        public NavigationMarkerTextSegmentMarker (Cairo.Color color, TextSegment textSegment) : base (textSegment)
        {
            this.Color = color;
        }

        public NavigationMarkerTextSegmentMarker (string colorName, TextSegment textSegment) : base (textSegment)
        {
            this.ColorName = colorName;
        }

        public string ColorName { get; set; }

        public Cairo.Color Color { get; set; }

        public override void Draw (TextEditor editor, Cairo.Context cr, Pango.Layout layout, bool selected, int startOffset, int endOffset, double y, double startXPos, double endXPos)
        {
            int markerStart = Segment.Offset;
            int markerEnd = Segment.EndOffset;
            if (markerEnd < startOffset || markerStart > endOffset) 
                return; 

//            if (editor.IsSomethingSelected) 
//            {
//                var range = editor.SelectionRange;
//
//                if (range.Contains(markerStart)) 
//                {
//                    int end = System.Math.Min (markerEnd, range.EndOffset);
//                    this.InternalDraw (markerStart, end, editor, cr, layout, true, startOffset, endOffset, y, startXPos, endXPos);
//                    this.InternalDraw (range.EndOffset, markerEnd, editor, cr, layout, false, startOffset, endOffset, y, startXPos, endXPos);
//                    return;
//                }
//
//                if (range.Contains(markerEnd)) 
//                {
//                    this.InternalDraw (markerStart, range.Offset, editor, cr, layout, false, startOffset, endOffset, y, startXPos, endXPos);
//                    this.InternalDraw (range.Offset, markerEnd, editor, cr, layout, true, startOffset, endOffset, y, startXPos, endXPos);
//                    return;
//                }
//
//                if (markerStart <= range.Offset && range.EndOffset <= markerEnd) 
//                {
//                    this.InternalDraw (markerStart, range.Offset, editor, cr, layout, false, startOffset, endOffset, y, startXPos, endXPos);
//                    this.InternalDraw (range.Offset, range.EndOffset, editor, cr, layout, true, startOffset, endOffset, y, startXPos, endXPos);
//                    this.InternalDraw (range.EndOffset, markerEnd, editor, cr, layout, false, startOffset, endOffset, y, startXPos, endXPos);
//                    return;
//                }
//
//            }

            this.InternalDraw (markerStart, markerEnd, editor, cr, layout, false, startOffset, endOffset, y, startXPos, endXPos);
        }

        private void InternalDraw (int markerStart, int markerEnd, TextEditor editor, Cairo.Context cr, Pango.Layout layout, bool selected, int startOffset, int endOffset, double y, double startXPos, double endXPos)
        {
            // we get called twice, the second time has a different set of params because we are the same offset
            // on the next line as well


            //Debug.WriteLine("draw marker {0}, {1}", startXPos, y);

            if (markerStart >= markerEnd)
                return;

            double @from;
            double to;

            if (markerStart < startOffset && endOffset < markerEnd) 
            {
                @from = startXPos;
                to = endXPos;
            } else 
            { 
                int start = startOffset < markerStart ? markerStart : startOffset;
                int end = endOffset < markerEnd ? endOffset : markerEnd;
                int /*lineNr,*/ x_pos;

                x_pos = layout.IndexToPos (start - startOffset).X;
                @from = startXPos + (int)(x_pos / Pango.Scale.PangoScale);

                x_pos = layout.IndexToPos (end - startOffset).X;

                to = startXPos + (int)(x_pos / Pango.Scale.PangoScale);
            }

            var charWidth = editor.TextViewMargin.CharWidth;

            @from = System.Math.Max (@from, editor.TextViewMargin.XOffset) - charWidth / 2;
            to = System.Math.Max (to, editor.TextViewMargin.XOffset) - charWidth / 2;

            if (@from >= to) 
            {
                //return;

                // BUG: fake it for now, but doesn't draw correctly
//                to += editor.TextViewMargin.CharWidth;
            }
            to = @from + editor.TextViewMargin.CharWidth;

            double height = editor.LineHeight / 2;
            if (selected) 
            {
                cr.Color = editor.ColorStyle.SelectedText.Foreground;
            } 
            else 
            {
                cr.Color = ColorName == null ? Color : editor.ColorStyle.GetChunkStyle (ColorName).Foreground;
            }

            cr.LineWidth = 1;
            cr.MoveTo (@from, y + editor.LineHeight - 1.5);
            cr.LineTo (to, y + editor.LineHeight - 1.5);

            cr.LineTo (@from + (@to - @from) / 2, y + editor.LineHeight - 5);

            cr.LineTo (@from, y + editor.LineHeight - 1.5);

            cr.Fill();
        }
    }
}

