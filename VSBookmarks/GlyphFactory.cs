﻿using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace VSBookmarks {

  internal class GlyphFactory: IGlyphFactory {

    public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag) {
      if(tag == null || !(tag is Tag)) {
        return null;
      }

      var digit = new TextBlock();
      digit.Text = (tag as Tag).Number.ToString();
      digit.FontFamily = new FontFamily("Verdana");
      digit.FontSize = 12;
      digit.FontWeight = FontWeights.ExtraBold;
      digit.HorizontalAlignment = HorizontalAlignment.Center;
      digit.VerticalAlignment = VerticalAlignment.Center;
      digit.Width = _GlyphSize;
      digit.Height = _GlyphSize;

      return digit;
    }

    const double _GlyphSize = 14.0;
  }

  [Export(typeof(IGlyphFactoryProvider))]
  [Name("VSBookmarks")]
  [Order(After = "VsTextMarker")]
  [ContentType("code")]
  [TagType(typeof(Tag))]
  internal sealed class GlyphFactoryProvider: IGlyphFactoryProvider {

    public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin) {
      return new GlyphFactory();
    }

  }

}