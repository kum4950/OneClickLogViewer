﻿using System;
using System.Linq;
using System.Windows;
using ScintillaNET;
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Windows.Input;
using ScintillaNET.WPF;
using System.Windows.Media;
using ScintillaNET_FindReplaceDialog;
using MahApps.Metro.Controls;
using OneClickLogViewer.MVVM.Views;
using ControlzEx.Theming;
using MahApps.Metro.Theming;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

namespace OneClickLogViewer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region enums
        /// <summary>
        /// Enumeration for supported application themes
        /// 
        /// These types must be consistent with the <seealso cref="ThemeResourceUris"/> array.
        /// </summary>
        public enum TypeOfTheme
        {
            ExpressionDark = 0,
            Generic = 1,
            WhistlerBlue = 2
        }
        #endregion enums


        #region ThemingProperties
        /// <summary>
        /// Get the enum datatype of the currently selected theme
        /// </summary>
        public TypeOfTheme CurrentTheme
        {
            get;
            private set;
        }

        /// <summary>
        /// Get the name of the currently selected theme
        /// </summary>
        public string CurrentThemeName
        {
            get
            {
                return this.CurrentTheme.ToString();
            }
        }
        #endregion ThemingProperties


        #region Fields

        //kum
        //private int bookmarkNum = 0;

        private const string NEW_DOCUMENT_TEXT = "Untitled";
        private const int LINE_NUMBERS_MARGIN_WIDTH = 30; // TODO - don't hardcode this


        /// <summary>
        /// the background color of the text area
        /// </summary>
        private int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;

        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        private const int CHECK_MARGIN = 4;
        private int check_num = 0;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = false;

        private int _newDocumentCount;
        public static RoutedCommand NewFileCommand = new RoutedCommand();
        public static RoutedCommand OpenFileCommand = new RoutedCommand();
        public static RoutedCommand SaveFileCommand = new RoutedCommand();
        public static RoutedCommand SaveAllFilesCommand = new RoutedCommand();
        public static RoutedCommand PrintFileCommand = new RoutedCommand();
        public static RoutedCommand UndoCommand = new RoutedCommand();
        public static RoutedCommand RedoCommand = new RoutedCommand();
        public static RoutedCommand CutCommand = new RoutedCommand();
        public static RoutedCommand CopyCommand = new RoutedCommand();
        public static RoutedCommand PasteCommand = new RoutedCommand();
        public static RoutedCommand SelectAllCommand = new RoutedCommand();
        public static RoutedCommand IncrementalSearchCommand = new RoutedCommand();
        public static RoutedCommand FindCommand = new RoutedCommand();
        public static RoutedCommand ReplaceCommand = new RoutedCommand();
        public static RoutedCommand GotoCommand = new RoutedCommand();
        public static RoutedCommand ViewTheme = new RoutedCommand();


        private const string ProductName = "OneClickLogViewer";

        private FindReplace MyFindReplace;

        #endregion Fields

        #region Constructors

        public MainWindow()
        {
            InitializeComponent();
            // TODO Why this has to be here, I have no idea.
            // All I know is that it doesn't work properly
            // if put in the xaml file.
            lineNumbersMenuItem.IsChecked = true;

            MyFindReplace = new FindReplace();
            // Tie in FindReplace event
            MyFindReplace.KeyPressed += MyFindReplace_KeyPressed;

            this.CurrentTheme = TypeOfTheme.Generic;

            
        }

        #endregion Constructors

        #region Menus

        // These sections are in the same order
        // they appear in the actual menu.

        #region File

        private void newMenuItem_Click(object sender, RoutedEventArgs e)
        {
            NewDocument();
        }

        private void openMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void saveMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Save();
        }

        private void saveAsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.SaveAs();
        }

        private void saveAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            foreach (DocumentForm doc in Documents)
            {
                doc.Save();
            }
        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion File

        #region Edit

        #region Undo/Redo

        private void undoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Undo();
        }

        private void redoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Redo();
        }

        #endregion Undo/Redo

        #region Cut/Copy/Paste

        private void cutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Cut();
        }

        private void copyMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Copy();
        }

        private void pasteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Paste();
        }

        #endregion Cut/Copy/Paste

        private void selectLineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
            {
                Line line = ActiveDocument.Scintilla.Lines[ActiveDocument.Scintilla.CurrentLine];
                ActiveDocument.Scintilla.SetSelection(line.Position + line.Length, line.Position);
            }
        }

        private void selectAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.SelectAll();
        }

        private void clearSelectionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.SetEmptySelection(0);
        }

        #region Find and Replace

        private void incrementalSearchMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActiveDocument.FindReplace.ShowIncrementalSearch();
        }

        private void findMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActiveDocument.FindReplace.ShowFind();
        }

        private void replaceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActiveDocument.FindReplace.ShowReplace();
        }

        private void findInFilesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Coming someday...
            // TODO - Implement findInFiles feature
            MessageBox.Show("Future!", ProductName, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void replaceInFilesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //	Coming someday...
            // TODO - Implement replaceInFiles feature
            MessageBox.Show("Future!", ProductName, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion Find and Replace

        private void gotoMenuItem_Click(object sender, RoutedEventArgs e)
        {
            GoTo MyGoTo = new GoTo(ActiveDocument.Scintilla.Scintilla);
            MyGoTo.ShowGoToDialog();
        }

        #region Bookmarks

        private void toggleBookmarkMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Line currentLine = ActiveDocument.Scintilla.Lines[ActiveDocument.Scintilla.CurrentLine];
            const uint mask = (1 << BOOKMARK_MARKER);
            uint markers = currentLine.MarkerGet();
            if ((markers & mask) > 0)
            {
                currentLine.MarkerDelete(BOOKMARK_MARKER);
            }
            else
            {
                currentLine.MarkerAdd(BOOKMARK_MARKER);
            }
        }

        private void previousBookmarkMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //	 I've got to redo this whole FindNextMarker/FindPreviousMarker Scheme
            int lineNumber = ActiveDocument.Scintilla.Lines[ActiveDocument.Scintilla.CurrentLine - 1].MarkerPrevious(1 << BOOKMARK_MARKER);
            if (lineNumber != -1)
                ActiveDocument.Scintilla.Lines[lineNumber].Goto();
        }

        private void nextBookmarkMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //	 I've got to redo this whole FindNextMarker/FindPreviousMarker Scheme
            int lineNumber = ActiveDocument.Scintilla.Lines[ActiveDocument.Scintilla.CurrentLine + 1].MarkerNext(1 << BOOKMARK_MARKER);
            if (lineNumber != -1)
                ActiveDocument.Scintilla.Lines[lineNumber].Goto();
        }

        private void clearBookmarksMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActiveDocument.Scintilla.MarkerDeleteAll(BOOKMARK_MARKER);
        }

        #endregion Bookmarks

        #region Advanced

        private void makeUpperCaseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActiveDocument.Scintilla.ExecuteCmd(Command.Uppercase);
        }

        private void makeLowerCaseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            ActiveDocument.Scintilla.ExecuteCmd(Command.Lowercase);
        }

        #endregion Advanced

        #endregion Edit

        #region View

        private void toolbarMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the tool bar
            toolStrip.Visibility = Toggle(toolStrip.Visibility);
            toolbarMenuItem.IsChecked = toolStrip.Visibility == Visibility.Visible;
        }

        private void statusBarMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the visibility of the status strip
            statusStrip.Visibility = Toggle(statusStrip.Visibility);
            statusBarMenuItem.IsChecked = statusStrip.Visibility == Visibility.Visible;
        }

        #region Control Character Visibility

        private void whitespaceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the whitespace mode for all open files
            whitespaceMenuItem.IsChecked = !whitespaceMenuItem.IsChecked;
            foreach (DocumentForm doc in Documents)
            {
                if (whitespaceMenuItem.IsChecked)
                    doc.Scintilla.ViewWhitespace = WhitespaceMode.VisibleAlways;
                else
                    doc.Scintilla.ViewWhitespace = WhitespaceMode.Invisible;
            }
        }

        private void wordWrapMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Toggle word wrap for all open files
            wordWrapMenuItem.IsChecked = !wordWrapMenuItem.IsChecked;
            foreach (DocumentForm doc in Documents)
            {
                if (wordWrapMenuItem.IsChecked)
                    doc.Scintilla.WrapMode = WrapMode.Word;
                else
                    doc.Scintilla.WrapMode = WrapMode.None;
            }
        }

        private void endOfLineMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Toggle EOL visibility for all open files
            endOfLineMenuItem.IsChecked = !endOfLineMenuItem.IsChecked;
            foreach (DocumentForm doc in Documents)
            {
                doc.Scintilla.ViewEol = endOfLineMenuItem.IsChecked;
            }
        }

        #endregion Control Character Visibility

        #region Zoom

        private int _zoomLevel;

        private void UpdateAllScintillaZoom()
        {
            // Update zoom level for all files
            // TODO - DocumentsSource is null. This is probably supposed to zoom all windows, not just the document style windows.
            //foreach (DocumentForm doc in dockPanel.DocumentsSource)
            //    doc.Scintilla.Zoom = _zoomLevel;

            // TODO - Ideally remove this once the zoom for all windows is working.
            foreach (DocumentForm doc in documentsRoot.Children)
                doc.Scintilla.Zoom = _zoomLevel;
        }

        private void zoomInMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Increase the zoom for all open files
            _zoomLevel++;
            UpdateAllScintillaZoom();
        }

        private void zoomOutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _zoomLevel--;
            UpdateAllScintillaZoom();
        }

        private void resetZoomMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _zoomLevel = 0;
            UpdateAllScintillaZoom();
        }

        #endregion Zoom

        private void lineNumbersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the line numbers margin for all documents
            lineNumbersMenuItem.IsChecked = !lineNumbersMenuItem.IsChecked;
            foreach (DocumentForm docForm in Documents)
            {
                if (lineNumbersMenuItem.IsChecked)
                    docForm.Scintilla.Margins[NUMBER_MARGIN].Width = LINE_NUMBERS_MARGIN_WIDTH;
                else
                    docForm.Scintilla.Margins[NUMBER_MARGIN].Width = 0;
            }
        }

        private void CheckNumbersMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Toggle the line numbers margin for all documents
            lineNumbersMenuItem.IsChecked = !lineNumbersMenuItem.IsChecked;
            foreach (DocumentForm docForm in Documents)
            {
                if (lineNumbersMenuItem.IsChecked)
                    docForm.Scintilla.Margins[NUMBER_MARGIN].Width = LINE_NUMBERS_MARGIN_WIDTH;
                else
                    docForm.Scintilla.Margins[NUMBER_MARGIN].Width = 0;
            }
        }


        #region Folding

        private void foldLevelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Lines[ActiveDocument.Scintilla.CurrentLine].FoldLine(FoldAction.Contract);
        }

        private void unfoldLevelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.Lines[ActiveDocument.Scintilla.CurrentLine].FoldLine(FoldAction.Expand);
        }

        private void foldAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.FoldAll(FoldAction.Contract);
        }

        private void unfoldAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Scintilla.FoldAll(FoldAction.Expand);
        }

        #endregion Folding

        #endregion View

        #region Window

        private void bookmarkWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // These currently are hidden.
            // TODO - Implement bookmark window feature
            MessageBox.Show("Future!", ProductName, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void findResultsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // TODO - Implement find results window feature
            MessageBox.Show("Future!", ProductName, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void closeWindowMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveDocument != null)
                ActiveDocument.Close();
        }

        private void closeAllMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // TODO - Implement close all windows feature
            MessageBox.Show("Future!", ProductName, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion Window

        #region Settings

        #endregion
        private void configuration_click(object sender, RoutedEventArgs e)
        {
            ConfigView configView = new ConfigView();
            configView.ShowDialog();
        }

        private void changeDarkTheme(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ClearThemes();
            var theme = ThemeManager.Current.AddLibraryTheme(new LibraryTheme(
    new Uri("pack://application:,,,/OneClickLogViewer;component/CustomAccents/Dark.Accent.xaml"),
    MahAppsLibraryThemeProvider.DefaultInstance));
            ThemeManager.Current.ChangeTheme(Application.Current, theme);
            
            Application.Current.MainWindow.Activate();

            this.TitleForeground = new SolidColorBrush() { Color = Color.FromRgb(255, 255, 255) };
            this.dockPanel.Theme = new AvalonDock.Themes.Vs2013DarkTheme();
        }

        private void changeLightTheme(object sender, RoutedEventArgs e)
        {
            ThemeManager.Current.ClearThemes();
            var theme = ThemeManager.Current.AddLibraryTheme(new LibraryTheme(
    new Uri("pack://application:,,,/OneClickLogViewer;component/CustomAccents/Light.Accent.xaml"),
    MahAppsLibraryThemeProvider.DefaultInstance));
            ThemeManager.Current.ChangeTheme(Application.Current, theme);
            
            Application.Current.MainWindow.Activate();

            this.TitleForeground = new SolidColorBrush() { Color = Color.FromRgb(0, 0, 0) };
            this.WindowButtonCommands.Foreground = new SolidColorBrush() { Color = Color.FromRgb(0, 0, 0) };

            this.dockPanel.Theme = new AvalonDock.Themes.Vs2013LightTheme();

        

    }


        #region Help

        private void aboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        #endregion Help

        #endregion Menus

        #region Properties

        public DocumentForm ActiveDocument
        {
            get { return documentsRoot.Children.FirstOrDefault(c => c.Content == dockPanel.ActiveContent) as DocumentForm; }
        }

        public IEnumerable<DocumentForm> Documents
        {
            get { return documentsRoot.Children.Cast<DocumentForm>(); }
        }

        #endregion Properties

        #region Methods


        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            // Update the main form _text to show the current document
            if (ActiveDocument != null)
            {
                this.Title = String.Format(CultureInfo.CurrentCulture, "{0} - {1}", ActiveDocument.Title, Program.Title);
                MyFindReplace.Scintilla = ActiveDocument.Scintilla.Scintilla;
                ActiveDocument.FindReplace = MyFindReplace;
            }
            else
                this.Title = Program.Title;
        }


        //kum
        private void InitBookmarkMargin(ScintillaWPF ScintillaNet)
        {
            //TextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));

            var margin = ScintillaNet.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);

            SetMarker(ScintillaNet);

        }

        private void SetMarker(ScintillaWPF ScintillaNet)
        {
            var marker = ScintillaNet.Markers[BOOKMARK_MARKER];
            marker.SetAlpha(50);
            //marker.SetBackColor(Color.FromArgb(50,0,0,0);

            /*
            var marker = ScintillaNet.Markers[bookmarkNum];
            marker.Symbol = MarkerSymbol.RgbaImage;

            FormattedText text = new FormattedText(bookmarkNum.ToString(), new CultureInfo("en-us"), FlowDirection.LeftToRight,
                new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Normal, new FontStretch()), 16, Brushes.Red, VisualTreeHelper.GetDpi(this).PixelsPerDip);

            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawText(text, new Point(0, 0));
            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap(20, 20, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            MemoryStream stream = new MemoryStream();
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(stream);

            System.Drawing.Bitmap mybitmap = new System.Drawing.Bitmap(stream);


            using (var bitmap = new System.Drawing.Bitmap(mybitmap))
            {
                marker.DefineRgbaImage(bitmap);
            }
            */
        }

        public static RenderTargetBitmap GetBitmapFromControl(Grid view)
        {
            Size size = new Size(view.ActualWidth, view.ActualHeight);
            if (size.IsEmpty)
                return null;

            size = new Size(20, 20);

            RenderTargetBitmap result = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96, 96, PixelFormats.Pbgra32);

            DrawingVisual drawingvisual = new DrawingVisual();
            using (DrawingContext context = drawingvisual.RenderOpen())
            {
                context.DrawRectangle(new VisualBrush(view), null, new Rect(new Point(), size));
                context.Close();
            }

            result.Render(drawingvisual);

            return result;
        }



        private void InitCodeFolding(ScintillaWPF ScintillaNet)
        {
            ScintillaNet.SetFoldMarginColor(true, IntToMediaColor(BACK_COLOR));
            ScintillaNet.SetFoldMarginHighlightColor(true, IntToMediaColor(BACK_COLOR));

            // Enable code folding
            ScintillaNet.SetProperty("fold", "1");
            ScintillaNet.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            ScintillaNet.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            ScintillaNet.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            ScintillaNet.Margins[FOLDING_MARGIN].Sensitive = true;
            ScintillaNet.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                ScintillaNet.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                ScintillaNet.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            ScintillaNet.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            ScintillaNet.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            ScintillaNet.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            ScintillaNet.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            ScintillaNet.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            ScintillaNet.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            ScintillaNet.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            ScintillaNet.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
        }

        private void InitColors(ScintillaWPF ScintillaNet)
        {
            ScintillaNet.CaretForeColor = Colors.White;
            ScintillaNet.SetSelectionBackColor(true, IntToMediaColor(0x114D9C));

            //FindReplace.Indicator.ForeColor = System.Drawing.Color.DarkOrange;
        }

        private void InitNumberMargin(ScintillaWPF ScintillaNet)
        {
            ScintillaNet.Styles[ScintillaNET.Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            ScintillaNet.Styles[ScintillaNET.Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            ScintillaNet.Styles[ScintillaNET.Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            ScintillaNet.Styles[ScintillaNET.Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = ScintillaNet.Margins[NUMBER_MARGIN];
            nums.Width = LINE_NUMBERS_MARGIN_WIDTH;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            ScintillaNet.MarginClick += TextArea_MarginClick;
        }

        private void InitSyntaxColoring(ScintillaWPF ScintillaNet)
        {
            // Configure the default style
            ScintillaNet.StyleResetDefault();
            ScintillaNet.Styles[ScintillaNET.Style.Default].Font = "Consolas";
            ScintillaNet.Styles[ScintillaNET.Style.Default].Size = 10;
            ScintillaNet.Styles[ScintillaNET.Style.Default].BackColor = IntToColor(0x212121);
            ScintillaNet.Styles[ScintillaNET.Style.Default].ForeColor = IntToColor(0xFFFFFF);
            ScintillaNet.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            ScintillaNet.Styles[ScintillaNET.Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            ScintillaNet.Lexer = Lexer.Cpp;

            ScintillaNet.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            ScintillaNet.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");
        }

        /// <summary>
        /// Converts a Win32 colour to a Drawing.Color
        /// </summary>
        /// <param name="rgb">A Win32 color.</param>
        /// <returns>A System.Drawing color.</returns>
        public static System.Drawing.Color IntToColor(int rgb)
        {
            return System.Drawing.Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        /// <summary>
        /// Converts a Win32 colour to a Media Color
        /// </summary>
        /// <param name="rgb">A Win32 color.</param>
        /// <returns>A System.Media color.</returns>
        public static Color IntToMediaColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        private void MyFindReplace_KeyPressed(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            ScintillaNet_KeyDown(sender, e);
        }

        private DocumentForm NewDocument()
        {
            DocumentForm doc = new DocumentForm();
            SetScintillaToCurrentOptions(doc);
            doc.Title = String.Format(CultureInfo.CurrentCulture, "{0}{1}", NEW_DOCUMENT_TEXT, ++_newDocumentCount);
            documentsRoot.Children.Add(doc);
            //doc.DockAsDocument();
            doc.IsActive = true;

            return doc;
        }

        private void OpenFile()
        {
            bool? res = openFileDialog.ShowDialog();
            if (res == null || !(bool)res)
                return;

            foreach (string filePath in openFileDialog.FileNames)
            {
                // Ensure this file isn't already open
                bool isOpen = false;
                foreach (DocumentForm documentForm in Documents)
                {
                    if (filePath.Equals(documentForm.FilePath, StringComparison.OrdinalIgnoreCase))
                    {
                        documentForm.IsActive = true;
                        isOpen = true;
                        break;
                    }
                }

                // Open the files
                if (!isOpen)
                    OpenFile(filePath);
            }
        }

        private DocumentForm OpenFile(string filePath)
        {
            DocumentForm doc = new DocumentForm();
            SetScintillaToCurrentOptions(doc);
            doc.Scintilla.Text = File.ReadAllText(filePath);
            //doc.Scintilla.UndoRedo.EmptyUndoBuffer();
            //doc.Scintilla.Modified = false;
            doc.Title = Path.GetFileName(filePath);
            doc.FilePath = filePath;
            documentsRoot.Children.Add(doc);
            //doc.DockAsDocument();
            doc.IsActive = true;
            //incrementalSearcher.Scintilla = doc.Scintilla;

            return doc;
        }

        /*
        public unsafe static byte[] GetBytes(string text, Encoding encoding, bool zeroTerminated)
        {
            if (string.IsNullOrEmpty(text))
            {
                if (!zeroTerminated)
                {
                    return new byte[0];
                }

                return new byte[1];
            }

            int byteCount = encoding.GetByteCount(text);
            byte[] array = new byte[byteCount + (zeroTerminated ? 1 : 0)];
            fixed (byte* bytes = array)
            {
                fixed (char* chars = text)
                {
                    encoding.GetBytes(chars, text.Length, bytes, byteCount);
                }
            }

            if (zeroTerminated)
            {
                array[array.Length - 1] = 0;
            }

            return array;
        }
        */


        private void SetScintillaToCurrentOptions(DocumentForm doc)
        {
            ScintillaWPF ScintillaNet = doc.Scintilla;
            ScintillaNet.KeyDown += ScintillaNet_KeyDown;

            // INITIAL VIEW CONFIG
            ScintillaNet.WrapMode = WrapMode.None;
            ScintillaNet.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors(ScintillaNet);
            InitSyntaxColoring(ScintillaNet);

            // NUMBER MARGIN
            InitNumberMargin(ScintillaNet);



            ScintillaNet.Styles[ScintillaNET.Style.IndentGuide].ForeColor = IntToColor(0xFF0000);
            ScintillaNet.Styles[ScintillaNET.Style.IndentGuide].BackColor = IntToColor(0xD5D5D5);
            ScintillaNet.Styles[ScintillaNET.Style.IndentGuide].Bold = true;
            ScintillaNet.Styles[ScintillaNET.Style.IndentGuide].Size = 12;
            ScintillaNet.Styles[ScintillaNET.Style.IndentGuide].Italic = true;


            var text = ScintillaNet.Margins[CHECK_MARGIN];
            text.Type = MarginType.Text;
            text.BackColor = IntToColor(0x212121);
            text.Width = 12;
            text.Sensitive = true;
            text.Mask = 0;

            /*
            var bytes = Helpers.GetBytes("ssss", ScintillaNet.Encoding, zeroTerminated: true);
            fixed (byte* bp = bytes)
                scintilla.DirectMessage(NativeMethods.SCI_MARGINSETTEXT, new IntPtr(Index), new IntPtr(bp));

            */
            // BOOKMARK MARGIN
            InitBookmarkMargin(ScintillaNet);

            // CODE FOLDING MARGIN
            InitCodeFolding(ScintillaNet);

            // DRAG DROP
            // TODO - Enable InitDragDropFile
            //InitDragDropFile();

            // INIT HOTKEYS
            // TODO - Enable InitHotkeys
            //InitHotkeys(ScintillaNet);

            // Turn on line numbers?
            if (lineNumbersMenuItem.IsChecked)
                doc.Scintilla.Margins[NUMBER_MARGIN].Width = LINE_NUMBERS_MARGIN_WIDTH;
            else
                doc.Scintilla.Margins[NUMBER_MARGIN].Width = 0;

            // Turn on white space?
            if (whitespaceMenuItem.IsChecked)
                doc.Scintilla.ViewWhitespace = WhitespaceMode.VisibleAlways;
            else
                doc.Scintilla.ViewWhitespace = WhitespaceMode.Invisible;

            // Turn on word wrap?
            if (wordWrapMenuItem.IsChecked)
                doc.Scintilla.WrapMode = WrapMode.Word;
            else
                doc.Scintilla.WrapMode = WrapMode.None;

            // Show EOL?
            doc.Scintilla.ViewEol = endOfLineMenuItem.IsChecked;

            // Set the zoom
            doc.Scintilla.Zoom = _zoomLevel;
        }

        private void ScintillaNet_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == System.Windows.Forms.Keys.F)
            {
                MyFindReplace.ShowFind();
                e.SuppressKeyPress = true;
            }
            else if (e.Shift && e.KeyCode == System.Windows.Forms.Keys.F3)
            {
                MyFindReplace.Window.FindPrevious();
                e.SuppressKeyPress = true;
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.F3)
            {
                MyFindReplace.Window.FindNext();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == System.Windows.Forms.Keys.H)
            {
                MyFindReplace.ShowReplace();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == System.Windows.Forms.Keys.I)
            {
                MyFindReplace.ShowIncrementalSearch();
                e.SuppressKeyPress = true;
            }
            else if (e.Control && e.KeyCode == System.Windows.Forms.Keys.G)
            {
                GoTo MyGoTo = new GoTo((Scintilla)sender);
                MyGoTo.ShowGoToDialog();
                e.SuppressKeyPress = true;
            }
        }


        //kum
        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            ScintillaNET.WPF.ScintillaWPF TextArea = ActiveDocument.Scintilla;

            Console.WriteLine("margin click");

           

            if (e.Margin == BOOKMARK_MARGIN)
            {
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);

                //int mask = (1 << bookmarkNum);

                
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                    //line.MarkerDelete(bookmarkNum);
                    //ActiveDocument.scintilla.DirectMessage(2044, new IntPtr(line.Index), new IntPtr(bookmarkNum));
                    //bookmarkNum--;
                    //지우면 다시 마커 숫자 세야할듯?
                }
                else
                {
                    // Add bookmark

                    //SetMarker(ActiveDocument.scintilla);

                    line.MarkerAdd(BOOKMARK_MARKER);
                    //line.MarkerAdd(bookmarkNum);

                    //ActiveDocument.scintilla.DirectMessage(2043,new IntPtr(line.Index),new IntPtr(bookmarkNum));
                    //bookmarkNum++;

                    
                }
            }

            if(e.Margin == CHECK_MARGIN)
            {
                var line = TextArea.Lines[TextArea.LineFromPosition(e.Position)];

                if (line.MarginText == string.Empty)
                {
                    Console.WriteLine(line.MarginStyle);
                    line.MarginStyle = ScintillaNET.Style.IndentGuide;
                    line.MarginText = check_num.ToString();

                    check_num++;

                    if (check_num >= 10)
                    {
                        TextArea.Margins[CHECK_MARGIN].Width = 24;
                    }
                }
                else
                {
                    line.MarginText = string.Empty;
                    check_num--;
                    // 지우면 숫자 꼬임 수정해야함.
                }

            }

        }

        private static Visibility Toggle(Visibility v)
        {
            if (v == Visibility.Visible)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // DEFAULT FILE
            //OpenFile("../../OneClickLogViewer/MainWindow.xaml.cs");
            //OpenFile("../../OneClickLogViewer/DocumentForm.xaml.cs");

            this.mytimepicker.PickerVisibility = TimePartVisibility.Hour | TimePartVisibility.Minute | TimePartVisibility.Second;


           

            Popup pp = this.mytimepicker.Template.FindName("PART_Popup", mytimepicker) as Popup;

            FrameworkElement childElement = pp.FindName("PART_ClockPartSelectorsHolder") as FrameworkElement;


            FrameworkElement comboBox = pp.FindName("PART_AmPmSwitcher") as FrameworkElement;


            if (childElement != null)
            {
                // 자식 요소를 찾았을 때 필요한 작업 수행
                // ...
                
                Grid mygrid = (Grid)childElement;
                ColumnDefinitionCollection columnDefinitions = mygrid.ColumnDefinitions;

                Console.WriteLine(columnDefinitions.Count);

                columnDefinitions.RemoveAt(columnDefinitions.Count - 1);

                Console.WriteLine(columnDefinitions.Count);



                comboBox.Visibility = Visibility.Hidden;



            }



            Image myImage = new Image();

            FormattedText text = new FormattedText("ABC", new CultureInfo("en-us"), FlowDirection.LeftToRight,
                new Typeface(this.FontFamily, FontStyles.Normal, FontWeights.Normal, new FontStretch()), 16, Brushes.Black, VisualTreeHelper.GetDpi(this).PixelsPerDip);


            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawText(text, new Point(0, 0));
            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            myImage.Source = bmp;

            this.mystackpanel.Children.Add(myImage);
            mystackpanel.Background = Brushes.Red;
            mystackpanel.Height = 100;
        }

        #endregion Methods


    }
}
