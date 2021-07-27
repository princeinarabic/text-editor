using System;
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

    public Window() {
        ClientSize = new Size(500, 500);
        StartPosition = FormStartPosition.CenterScreen;
        this.FormClosing += Form_Closing;

        textBox = new RichTextBox();
        textBox.SelectionChanged += BTN_SelectionChanged;
        textboxFeatures();
        Controls.Add(textBox);         

        initMenuStrip();
        
        this.toolStrip = new ToolStrip();
        toolStrip.GripStyle = ToolStripGripStyle.Hidden;  
        toolStrip.BackColor = Color.LightGreen;  
        
        initIcons();
        buttonActions();

        toolStrip.Dock = DockStyle.Top;  
        Controls.Add(toolStrip);

        menuStrip.Dock = DockStyle.Top;  // adding it after toolStrip to make the menu on top of buttons
        Controls.Add(menuStrip);         // because whatever is added last goes on top.

    }  

    void BTN_SelectionChanged(object sender, EventArgs e) {
        if (textBox.SelectionFont.Bold == true)
            boldBTN.Checked = true;
        else 
            boldBTN.Checked = false;

        if (textBox.SelectionFont.Italic == true)
            italicBTN.Checked = true;
        else 
            italicBTN.Checked = false;

        if (textBox.SelectionFont.Underline == true)
            underlineBTN.Checked = true;
        else 
            underlineBTN.Checked = false;
    }

    Font initFont() {  
        Font font = new Font("Arial", 11, FontStyle.Regular);
        return font;
    }

    void textboxFeatures() {
        textBox.Font = initFont();
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
            new ToolStripMenuItem("Open...", null, onOpen),
            new ToolStripMenuItem("Save", null, onSave),
            new ToolStripMenuItem("Save As...", null, onSave),
            new ToolStripMenuItem("Quit", null, onQuit),
        };

        ToolStripMenuItem[] editItems = {
            new ToolStripMenuItem("Undo", null, onUndo),
            new ToolStripMenuItem("Redo", null, onRedo),
            new ToolStripMenuItem("Select All", null, onSelectAll),
            new ToolStripMenuItem("Cut", null, onCut),
            new ToolStripMenuItem("Paste", null, onPaste),
        };

        ToolStripMenuItem[] fontItems = {
            new ToolStripMenuItem("Choose Font...", null, onChooseFont),
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

    void boldBTN_Click(object sender, EventArgs e) {
        Font newFont, oldFont;  
        oldFont = textBox.SelectionFont;  
        if (oldFont.Bold)  
            newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Bold);  
        else  
            newFont = new Font(oldFont, oldFont.Style | FontStyle.Bold);  
        
        textBox.SelectionFont = newFont;  
        textBox.Focus();  

        if (textBox.SelectionLength > 0 || textBox.SelectionFont.Bold == false) 
            boldBTN.Checked = false; 
    }

    void italicBTN_Click(object sender, EventArgs e) {
        Font newFont, oldFont;  
        oldFont = textBox.SelectionFont;  
        if (oldFont.Italic)  
            newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Italic);  
        else  
            newFont = new Font(oldFont, oldFont.Style | FontStyle.Italic);  
        
        textBox.SelectionFont = newFont;        
        textBox.Focus();  

        if (textBox.SelectionLength > 0 || textBox.SelectionFont.Italic == false) 
            italicBTN.Checked = false; 
    }

    void underlineBTN_Click(object sender, EventArgs e) {
        Font newFont, oldFont;  
        oldFont = textBox.SelectionFont;  
        if (oldFont.Underline)  
            newFont = new Font(oldFont, oldFont.Style & ~FontStyle.Underline);  
        else  
            newFont = new Font(oldFont, oldFont.Style | FontStyle.Underline);  
        
        textBox.SelectionFont = newFont;  
        textBox.Focus();  

        if (textBox.SelectionLength > 0 || textBox.SelectionFont.Underline == false) 
            underlineBTN.Checked = false; 
    }

    void bulletButtonChecked(object sender, EventArgs e) {
        if (bulletListBTN.Checked)
            textBox.SelectionBullet = true;
    }

    void alignLeft(object sender, EventArgs e) {
        alignRightBTN.Checked = alignCenterBTN.Checked = false;
        textBox.SelectionAlignment = HorizontalAlignment.Left;
    }

    void alignCenter(object sender, EventArgs e) {
        alignRightBTN.Checked = alignLeftBTN.Checked = false;
        textBox.SelectionAlignment = HorizontalAlignment.Center;
    }

    void alignRight(object sender, EventArgs e) {
        alignLeftBTN.Checked = alignCenterBTN.Checked = false;
        textBox.SelectionAlignment = HorizontalAlignment.Right;
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

        alignLeftBTN.Click += alignLeft;
        alignCenterBTN.Click += alignCenter;
        alignRightBTN.Click += alignRight;

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
        ToolStripSeparator bar = new ToolStripSeparator();
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

    string currentFile = "";
    void onSave(object sender, EventArgs e) {
        if (currentFile == "") {  
            onSaveAs(this, e);  
            return;  
        }  
        else {
            string strExt;  
            strExt = System.IO.Path.GetExtension(currentFile);  
            strExt = strExt.ToUpper();  

            StreamWriter textWriter = new StreamWriter(currentFile);  
            textWriter.Write(textBox.Text);  
            textWriter.Close();  
            textWriter = null;   
            textBox.Modified = false;  
        }
        this.Text = "Editor: " + currentFile.ToString();
    }

    void onSaveAs(object sender, EventArgs e) {  
        SaveFileDialog saveFile = new SaveFileDialog();
        saveFile.Title = "Save File";  
        saveFile.DefaultExt = "txt";  // rich text format  
        saveFile.Filter = "Text Files|*.txt|HTML Files|*.htm|All Files|*.*";  
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
        }  

        currentFile = saveFile.FileName;  
        textBox.Modified = false;  
        this.Text = "Editor: " + currentFile.ToString();
        MessageBox.Show("Saved Successfully", "File address: " + 
                        currentFile, MessageBoxButtons.OK, MessageBoxIcon.Information);
    } 

    void onChooseFont(object sender, EventArgs e) {
        FontDialog fd = new FontDialog();

        fd.Font = textBox.SelectionFont;
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
            answer = MessageBox.Show("The document has not been saved, continue without saving?", 
                                    "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  
            if (answer == DialogResult.No)  
                onSaveAs(this, e);
            else  
                Application.Exit();  
        }
        else  
            Application.Exit();    
    }

    void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
        if (textBox.Modified) {    
            if (MessageBox.Show("The document has not been saved, continue without saving?", 
                "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) 
            {
                e.Cancel = true;
                onSaveAs(this, e);
            }
        }
    }
}

class Editor {
    [STAThread]
    static void Main() {
        Window w = new Window();
        Application.Run(w);
    }
}