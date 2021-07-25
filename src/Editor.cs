using System;
using static System.Console;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;

public class Window : Form {
    RichTextBox textBox;
    ToolStrip toolStrip;
    MenuStrip menuStrip;
    ToolStripButton boldBTN, italicBTN, underlineBTN;  // BTN - button
    ToolStripButton alignLeftBTN, alignCenterBTN, alignRightBTN;
    ToolStripButton colorDialogBTN, bulletListBTN;
    ToolStripSeparator bar;
    Font font; 

    public Window() {
        ClientSize = new Size(500, 500);
        StartPosition = FormStartPosition.CenterScreen;

        initFonts();
        textBox = new RichTextBox();
        textboxFeatures();
        Controls.Add(textBox);         

        initMenuStrip();
        
        this.toolStrip = new ToolStrip();
        toolStrip.GripStyle = ToolStripGripStyle.Hidden;  // hides the "grip" which are three dots you use to move the buttons
        toolStrip.BackColor = Color.LightGreen;  //MediumPurple
        
        initIcons();
        buttonActions();

        toolStrip.Dock = DockStyle.Top;  
        Controls.Add(toolStrip);

        menuStrip.Dock = DockStyle.Top;  // adding it after toolStrip to make the menu on top of buttons
        Controls.Add(menuStrip);         // because whatever is added last goes on top.
    }

    void initFonts() {
        font = new Font("Arial", 11, FontStyle.Regular);
    }

    void textboxFeatures() {
        textBox.Font = font;
        textBox.Multiline = true;
        textBox.Dock = DockStyle.Fill;
        textBox.ShowSelectionMargin = true;
        textBox.SelectionRightIndent = 7;
        textBox.BackColor = Color.OldLace;
        textBox.AcceptsTab = true;
        textBox.ShortcutsEnabled = true;  
    }

    void initMenuStrip() {
        /* ToolStripMenuItem(String, Image, EventHandler)	
        Initializes a new instance of the ToolStripMenuItem class that displays the specified text 
        and image and that does the specified action when the ToolStripMenuItem is clicked */
        ToolStripMenuItem[] fileItems = {
            new ToolStripMenuItem("Open", null, onOpen),
            new ToolStripMenuItem("Quit", null, onQuit),
            new ToolStripMenuItem("Save", null, onSave),
        };

        ToolStripMenuItem[] editItems = {
            new ToolStripMenuItem("Undo", null, onUndo),
            new ToolStripMenuItem("Redo", null, onRedo),
            new ToolStripMenuItem("Select All", null, onSelectAll),
            new ToolStripMenuItem("Paste", null, onPaste),
            new ToolStripMenuItem("Cut", null, onCut),
        };

        ToolStripMenuItem[] fontItems = {
            new ToolStripMenuItem("Choose Font", null, onChooseFont),
        };
        

        ToolStripMenuItem[] topItems = {
            new ToolStripMenuItem("File", null, fileItems),
            new ToolStripMenuItem("Edit", null, editItems),
            new ToolStripMenuItem("Font", null, fontItems)
        };
        
        menuStrip = new MenuStrip();
        menuStrip.BackColor = Color.BurlyWood;  

        foreach (var item in topItems)
            menuStrip.Items.Add(item);
    }

    FontStyle newBoldFont;  // build error when put within ToggleFontStyle()
    void boldBTN_Click(object sender, EventArgs e) {
        if (textBox.SelectionFont != null) {
            Font currentFont = textBox.SelectionFont;

            if (textBox.SelectionFont.Bold == true) 
                newBoldFont = FontStyle.Regular;
            else    
                newBoldFont = FontStyle.Bold;

            textBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newBoldFont);
        }
    }

    FontStyle newItalicFont;
    void italicBTN_Click(object sender, EventArgs e) {
        if (textBox.SelectionFont != null) {
            Font currentFont = textBox.SelectionFont;

            if (textBox.SelectionFont.Italic == true) 
                newItalicFont = FontStyle.Regular;
            else    
                newItalicFont = FontStyle.Italic;

            textBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newItalicFont);
        }
    }

    FontStyle newUnderlineFont;
    void underlineBTN_Click(object sender, EventArgs e) {
        if (textBox.SelectionFont != null) {
            Font currentFont = textBox.SelectionFont;

            if (textBox.SelectionFont.Underline == true) 
                newUnderlineFont = FontStyle.Regular;
            else    
                newUnderlineFont = FontStyle.Underline;

            textBox.SelectionFont = new Font(currentFont.FontFamily, currentFont.Size, newUnderlineFont);
        }
    }

    void bulletButtonChecked(object sender, EventArgs e) {
        if (bulletListBTN.Checked)
            textBox.SelectionBullet = true;
    }

    void alignmentButtonChecked(object sender, EventArgs e) {
        if (alignLeftBTN.Checked)
            textBox.SelectionAlignment = HorizontalAlignment.Left;
        else if (alignCenterBTN.Checked)    
            textBox.SelectionAlignment = HorizontalAlignment.Center;
        else if (alignRightBTN.Checked)
            textBox.SelectionAlignment = HorizontalAlignment.Right;
        else 
            textBox.SelectionAlignment = HorizontalAlignment.Left;
    }

    void colorDialogBTN_Click(object sender, EventArgs e) {
        ColorDialog cd = new ColorDialog();
        cd.Color = textBox.ForeColor;  
        if (cd.ShowDialog() == DialogResult.OK)  
            textBox.SelectionColor = cd.Color; 
    }

    void buttonActions() {
        boldBTN.Click += boldBTN_Click;
        italicBTN.Click += italicBTN_Click;
        underlineBTN.Click += underlineBTN_Click;

        alignRightBTN.Click += alignmentButtonChecked;
        alignCenterBTN.Click += alignmentButtonChecked;
        alignRightBTN.Click += alignmentButtonChecked;

        bulletListBTN.Click += bulletButtonChecked;
        colorDialogBTN.Click += colorDialogBTN_Click;
    }

    ToolStripButton iconButton(string name) {
        ToolStripButton b = new ToolStripButton();
        b.Image = Bitmap.FromFile(
                    Path.Combine("../src/icons", name + ".png"));
        return b;
    }

    void addSeparator() {
        bar = new ToolStripSeparator();
        toolStrip.Items.Add(bar);
    }

    void initIcons() {
        this.boldBTN = iconButton("Bold_16x");  // iconButton method is self written
        this.italicBTN = iconButton("Italic_16x");
        this.underlineBTN = iconButton("Underline_16x");

        this.alignLeftBTN = iconButton("AlignLeft_16x");
        this.alignCenterBTN = iconButton("AlignCenter_16x");
        this.alignRightBTN = iconButton("AlignRight_16x");

        this.bulletListBTN = iconButton("BulletList_16x");
        this.colorDialogBTN = iconButton("ColorDialog_16x");

        boldBTN.CheckOnClick = italicBTN.CheckOnClick = underlineBTN.CheckOnClick = true;
        alignLeftBTN.CheckOnClick = alignCenterBTN.CheckOnClick = alignRightBTN.CheckOnClick = true;
        bulletListBTN.CheckOnClick = colorDialogBTN.CheckOnClick = true;

        toolStrip.Items.Add(boldBTN); toolStrip.Items.Add(italicBTN); toolStrip.Items.Add(underlineBTN);
        addSeparator();
        toolStrip.Items.Add(alignLeftBTN); toolStrip.Items.Add(alignCenterBTN); toolStrip.Items.Add(alignRightBTN);
        addSeparator();
        toolStrip.Items.Add(bulletListBTN); 
        addSeparator();
        toolStrip.Items.Add(colorDialogBTN);
    }

    void loadFile(string filename) {
        string text = "";                                   

        using (StreamReader sr = new StreamReader(filename))
            while (sr.ReadLine() is string s)
                text += s + "\n";           

        textBox.Text = text;                    
    }                                                       
                                         
    /* The OpenFileDialog component allows users to browse  the folders of their computer or any computer on the 
    network and select one or more files to open. The dialog box returns the path and name of the file the 
    user selected in the dialog box. The FileName property  can be set prior to showing the dialog box */   
    void onOpen(object sender, EventArgs e) {       
        using (var dialog = new OpenFileDialog()) { 
            if (dialog.ShowDialog() == DialogResult.OK)
                loadFile(dialog.FileName);
        }
    }                 

    void onSave(object sender, EventArgs e) {  
        SaveFileDialog saveFile = new SaveFileDialog();
        saveFile.Title = "Save File";  
        saveFile.DefaultExt = "rtf";  // rich text format  
        saveFile.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|HTML Files|*.htm|All Files|*.*";  
        saveFile.FilterIndex = 1;  
        saveFile.ShowDialog();  

        if (saveFile.FileName == "")
            return;

        string strExt;  
        strExt = System.IO.Path.GetExtension(saveFile.FileName);  
        strExt = strExt.ToUpper();  
        if (strExt == ".RTF") 
            textBox.SaveFile(saveFile.FileName, RichTextBoxStreamType.RichText);   
        else {  
            StreamWriter textWriter;  
            textWriter = new StreamWriter(saveFile.FileName);  
            textWriter.Write(textBox.Text);  
            textWriter.Close();  
            textWriter = null;  
            textBox.SelectionStart = 0;  
            textBox.SelectionLength = 0;  
        }  
        string currentFile = saveFile.FileName;  
        textBox.Modified = false;  
        this.Text = "Editor: " + currentFile.ToString();
        MessageBox.Show("Saved Successfully", "File address: " + 
                        currentFile, MessageBoxButtons.OK, MessageBoxIcon.Information);
    } 

    void onChooseFont(object sender, EventArgs e) {
        FontDialog fd = new FontDialog();
        if ( textBox.SelectionFont != null) 
            fd.Font = textBox.SelectionFont;
        else 
            fd.Font = null;  
        fd.ShowApply = true;  

        if (fd.ShowDialog() == DialogResult.OK)  
            textBox.SelectionFont = fd.Font;  
    }

    void onSelectAll(object sender, EventArgs e) => textBox.SelectAll();  

    void onUndo(object sender, EventArgs e) {
        if (textBox.CanUndo)   
            textBox.Undo();  
    }

    void onRedo(object sender, EventArgs e) {
        if (textBox.CanRedo) 
            textBox.Redo();
    }

    void onPaste(object sender, EventArgs e) {
        if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))  
            textBox.Paste();  
    }

    void onCut(object sender, EventArgs e) {
        if (textBox.SelectionLength > 0)  
            textBox.Cut();
    }

    void onQuit(object sender, EventArgs e) {
        if (textBox.Modified) {  
            DialogResult answer;  
            answer = MessageBox.Show("The document has not been saved, would you like to continue without saving?", 
                                    "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  
            if (answer == DialogResult.No)  
                return;  
            else  
                Application.Exit();  
        }
        else  
            Application.Exit();    
    }
}

 class Editor {
    [STAThread]
    static void Main() {
        Window w = new Window();
        Application.Run(w);
    }
}