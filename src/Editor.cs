using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class Window : Form {
    RichTextBox textBox;
    ToolStrip toolStrip;
    MenuStrip menuStrip;
    ToolStripButton boldBTN, italicBTN, underlineBTN;  // BTN - button
    ToolStripButton alignLeftBTN, alignCenterBTN, alignRightBTN;
    ToolStripButton colorDialogBTN, bulletListBTN;
    SaveFileDialog saveFile;  // Can't make it a local var b/z it's also being used in the savingFile_nonRTF() method.
    string currentFile = "";

    public Window() {
        formFeatures();

        textBox = new RichTextBox();
        textBox.SelectionChanged += BTN_SelectionChanged;
        textboxFeatures();
        Controls.Add(textBox);         

        initMenuStrip();
        
        this.toolStrip = new ToolStrip();
        toolStrip.GripStyle = ToolStripGripStyle.Hidden;  
        toolStrip.BackColor = Color.LightGreen;  
        
        initIcons();
        buttons_CheckOnClick();
        attachButtons();
        buttonActions();
        textBox.TextChanged += Text_Changed;

        toolStrip.Dock = DockStyle.Top;  
        Controls.Add(toolStrip);

        menuStrip.Dock = DockStyle.Top;  // adding it after toolStrip to make the menu on top of buttons
        Controls.Add(menuStrip);         // because whatever is added last goes on top.
    }  

    void formFeatures() {
        Text = "Editor";
        ClientSize = new Size(500, 500);
        StartPosition = FormStartPosition.CenterScreen;
        this.FormClosing += Form_Closing;   
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
            new ToolStripMenuItem("Save As...", null, onSaveAs),
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




    /*   This section takes care of buttons   */  

    void Text_Changed(object sender, EventArgs e) {
        bulletListBTN.Checked = textBox.SelectionBullet;
    }

    void BTN_SelectionChanged(object sender, EventArgs e) {
        boldBTN.Checked = textBox.SelectionFont.Bold;
        italicBTN.Checked = textBox.SelectionFont.Italic;
        underlineBTN.Checked = textBox.SelectionFont.Underline;
        bulletListBTN.Checked = textBox.SelectionBullet;

        alignLeftBTN.Checked = (textBox.SelectionAlignment == HorizontalAlignment.Left);
        alignRightBTN.Checked = (textBox.SelectionAlignment == HorizontalAlignment.Right);
        alignCenterBTN.Checked = (textBox.SelectionAlignment == HorizontalAlignment.Center); 

        colorDialogBTN.Checked = (textBox.SelectionColor != Color.Black); 
    }

    void handleStyleClick(ToolStripButton styleBTN, FontStyle style) {
        Font newFont, oldFont;  
        oldFont = textBox.SelectionFont;  
        if ((oldFont.Style & style) != 0)            
            newFont = new Font(oldFont, oldFont.Style & ~style);  
        else  
            newFont = new Font(oldFont, oldFont.Style | style);  
        
        textBox.SelectionFont = newFont;  
        textBox.Focus();  
        styleBTN.Checked = ((newFont.Style & style) != 0);
    }


    void boldBTN_Click(object sender, EventArgs e) => 
        handleStyleClick(boldBTN, FontStyle.Bold);

    void italicBTN_Click(object sender, EventArgs e) =>
        handleStyleClick(italicBTN, FontStyle.Italic);

    void underlineBTN_Click(object sender, EventArgs e) =>
        handleStyleClick(underlineBTN, FontStyle.Underline);

    void bulletListButtonChecked(object sender, EventArgs e) =>
        textBox.SelectionBullet = bulletListBTN.Checked;

    void alignLeft(object sender, EventArgs e) {
        alignRightBTN.Checked = alignCenterBTN.Checked = false;
        textBox.SelectionAlignment = HorizontalAlignment.Left;
        alignLeftBTN.Checked = true;
    }

    void alignCenter(object sender, EventArgs e) {
        alignRightBTN.Checked = alignLeftBTN.Checked = false;
        textBox.SelectionAlignment = HorizontalAlignment.Center;
        alignCenterBTN.Checked = true;
    }

    void alignRight(object sender, EventArgs e) {
        alignLeftBTN.Checked = alignCenterBTN.Checked = false;
        textBox.SelectionAlignment = HorizontalAlignment.Right;
        alignRightBTN.Checked = true;
    }

    void colorDialogBTN_Click(object sender, EventArgs e) {
        colorDialogBTN.Checked = true;
        ColorDialog cd = new ColorDialog();
        cd.Color = textBox.ForeColor;  
        if (cd.ShowDialog() == DialogResult.OK)  
            textBox.SelectionColor = cd.Color; 

        if (textBox.SelectionColor == Color.Black)
            colorDialogBTN.Checked = false;    
    } 

    void buttonActions() {
        boldBTN.Click += boldBTN_Click;
        italicBTN.Click += italicBTN_Click;
        underlineBTN.Click += underlineBTN_Click;

        alignLeftBTN.Click += alignLeft;
        alignCenterBTN.Click += alignCenter;
        alignRightBTN.Click += alignRight;

        bulletListBTN.Click += bulletListButtonChecked;
        colorDialogBTN.Click += colorDialogBTN_Click;
    }

    ToolStripButton iconButton(string name) {
        ToolStripButton b = new ToolStripButton();
        b.Image = Bitmap.FromFile(
                    Path.Combine("../src/icons", name + ".png"));
        return b;
    }

    void buttons_CheckOnClick() {
        boldBTN.CheckOnClick = italicBTN.CheckOnClick = underlineBTN.CheckOnClick = true;
        bulletListBTN.CheckOnClick = true;
    }

    void addSeparator() {
        ToolStripSeparator bar = new ToolStripSeparator();
        toolStrip.Items.Add(bar);
    }

    void attachButtons() {
        toolStrip.Items.Add(boldBTN); toolStrip.Items.Add(italicBTN); toolStrip.Items.Add(underlineBTN);
        addSeparator();
        toolStrip.Items.Add(alignLeftBTN); toolStrip.Items.Add(alignCenterBTN); toolStrip.Items.Add(alignRightBTN);
        addSeparator();
        toolStrip.Items.Add(bulletListBTN); 
        addSeparator();
        toolStrip.Items.Add(colorDialogBTN);
    }

    void initIcons() {
        this.boldBTN = iconButton("Bold_16x");  
        this.italicBTN = iconButton("Italic_16x");
        this.underlineBTN = iconButton("Underline_16x");

        this.alignLeftBTN = iconButton("AlignLeft_16x");
        this.alignCenterBTN = iconButton("AlignCenter_16x");
        this.alignRightBTN = iconButton("AlignRight_16x");

        this.bulletListBTN = iconButton("BulletList_16x");
        this.colorDialogBTN = iconButton("ColorDialog_16x");
    }




    /*   "File" menu commands section   */
    void onOpen(object sender, EventArgs e) {
        OpenFileDialog fd = new OpenFileDialog();
        string text = "";

        fd.Title = "Open file";
        fd.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|All Files|*.*";
        fd.FilterIndex = 1;
        fd.ShowDialog();
        if (fd.FileName == "")
            return;

        string strExt = Path.GetExtension(fd.FileName);  
        strExt = strExt.ToUpper();  

        if (strExt == ".RTF")  
            textBox.LoadFile(fd.FileName, RichTextBoxStreamType.RichText);  
        else {                
            using (StreamReader sr = new StreamReader(fd.FileName)) {
                while (sr.ReadLine() is string s)
                    text += s + "\n";
            }
            textBox.Text = text;
        }  

        currentFile = fd.FileName;  
        textBox.Modified = false;  
        this.Text = "Editor: " + currentFile.ToString();  
    }        

    void savingFile(string file) {
        string strExt = Path.GetExtension(file);  
        if (strExt == ".rtf") 
            textBox.SaveFile(file, RichTextBoxStreamType.RichText);  
        else {
            StreamWriter textWriter = new StreamWriter(file);  
            textWriter.Write(textBox.Text);  
            textWriter.Close();
        }
    }

    void onSave(object sender, EventArgs e) {
        if (currentFile == "") {  
            onSaveAs(this, e);  
            return;  
        }  
        savingFile(currentFile);
        textBox.Modified = false;  // there is a reason why I put it here and not in the savingFile() method
                                   // putting in savingFile() will prevent the form from closing even after saving the file
    }

    void onSaveAs(object sender, EventArgs e) {  
        saveFile = new SaveFileDialog();
        saveFile.Title = "Save File";  
        saveFile.DefaultExt = "rtf";  
        saveFile.Filter = "Rich Text Files|*.rtf|Text Files|*.txt|All Files|*.*";  
        saveFile.FilterIndex = 1;  
        saveFile.ShowDialog();  

        if (saveFile.FileName == "")
            return;

        savingFile(saveFile.FileName);
        textBox.Modified = false;  // there is a reason why I put it here and not in the savingFile() method

        currentFile = saveFile.FileName;  
        this.Text = "Editor: " + currentFile.ToString();

        MessageBox.Show("Saved Successfully", "File address: " + 
                        currentFile, MessageBoxButtons.OK, MessageBoxIcon.Information);
    } 

    void onQuit(object sender, EventArgs e) {
        if (textBox.Modified) {  
            DialogResult answer;  
            answer = MessageBox.Show("The document has not been saved, continue without saving?", 
                                    "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question);  
            if (answer == DialogResult.No)  
                return;
            else  
                Application.Exit();  
        }
        else  
            Application.Exit();    
    }




    /*   The section of "Edit" menu commands   */

    void onSelectAll(object sender, EventArgs e) => textBox.SelectAll();  

    void onUndo(object sender, EventArgs e) {
        if (textBox.CanUndo)   
            textBox.Undo();  
    }

    /*  Use the Redo method to reapply the last undo operation to the control. 
        The CanRedo method enables you to determine whether the last operation 
        the user has undone can be reapplied to the control.  */
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



    
    /* The section of a "Font" menu command */
    void onChooseFont(object sender, EventArgs e) {
        FontDialog fd = new FontDialog();

        fd.Font = textBox.SelectionFont;
        fd.ShowApply = true;  

        if (fd.ShowDialog() == DialogResult.OK)  
            textBox.SelectionFont = fd.Font;  
    }



    void Form_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
        if (textBox.Modified) {    
            if (MessageBox.Show("The document has not been saved, continue without saving?", 
                "Unsaved Document", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) 
            {
                e.Cancel = true;
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