using System;
using static System.Console;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;

public class Window : Form {
    RichTextBox textBox;
    ToolStrip toolStrip;
    ToolStripButton boldBTN, italicBTN, underlineBTN;  // BTN - button
    ToolStripButton alignLeftBTN, alignCenterBTN, alignRightBTN, bulletListBTN;
    ToolStripSeparator bar;

    ToolStripDropDownButton headingsBTN;
    ToolStripDropDown dropDown;
    ToolStripButton heading1BTN, heading2BTN, heading3BTN;
    Font font, heading1Font, heading2Font, heading3Font;

    public Window() {
        ClientSize = new Size(500, 500);
        StartPosition = FormStartPosition.CenterScreen;

        initFonts();

        textBox = new RichTextBox();
        textBox.Font = font;
        textBox.Multiline = true;
        textBox.Dock = DockStyle.Fill;
        textBox.ShowSelectionMargin = true;
        textBox.SelectionRightIndent = 7;
        textBox.BackColor = Color.OldLace;  
        Controls.Add(textBox);         

        /* ToolStripMenuItem(String, Image, EventHandler)	
        Initializes a new instance of the ToolStripMenuItem class that displays the specified text 
        and image and that does the specified action when the ToolStripMenuItem is clicked */
        ToolStripMenuItem[] fileItems = {
            new ToolStripMenuItem("Open", null, onOpen),
            new ToolStripMenuItem("Quit", null, onQuit),
            new ToolStripMenuItem("Save", null, onSave)
        };
                
        ToolStripMenuItem[] topItems = {
            new ToolStripMenuItem("File", null, fileItems),
        };
        
        MenuStrip menuStrip = new MenuStrip();
        menuStrip.BackColor = Color.BurlyWood;  //CadetBlue

        foreach (var item in topItems)
            menuStrip.Items.Add(item);
        
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
        heading1Font = new Font("Arial", 20, FontStyle.Bold);
        heading2Font = new Font("Arial", 18, FontStyle.Bold);
        heading3Font = new Font("Arial", 16, FontStyle.Bold);
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

    void buttonActions() {
        boldBTN.Click += boldBTN_Click;
        italicBTN.Click += italicBTN_Click;
        underlineBTN.Click += underlineBTN_Click;

        alignRightBTN.Click += alignmentButtonChecked;
        alignCenterBTN.Click += alignmentButtonChecked;
        alignRightBTN.Click += alignmentButtonChecked;

        bulletListBTN.Click += bulletButtonChecked;
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

        this.heading1BTN = iconButton("HeadingOne_16x");
        this.heading2BTN = iconButton("HeadingTwo_16x");
        this.heading3BTN = iconButton("HeadingThree_16x");

        boldBTN.CheckOnClick = italicBTN.CheckOnClick = underlineBTN.CheckOnClick = true;
        alignLeftBTN.CheckOnClick = alignCenterBTN.CheckOnClick = alignRightBTN.CheckOnClick = true;
        bulletListBTN.CheckOnClick = true;
        heading1BTN.CheckOnClick = heading2BTN.CheckOnClick = heading3BTN.CheckOnClick = true;

        toolStrip.Items.Add(boldBTN); toolStrip.Items.Add(italicBTN); toolStrip.Items.Add(underlineBTN);
        addSeparator();
        toolStrip.Items.Add(alignLeftBTN); toolStrip.Items.Add(alignCenterBTN); toolStrip.Items.Add(alignRightBTN);
        addSeparator();
        toolStrip.Items.Add(bulletListBTN); 
        addSeparator();

        // This part forms the drop down button which contains headings buttons.
        this.headingsBTN = new ToolStripDropDownButton();
        this.dropDown = new ToolStripDropDown();
        this.headingsBTN.Text = "Headings";

        // Set the drop-down on the ToolStripDropDownButton.
        this.headingsBTN.DropDown = dropDown;
        this.headingsBTN.DropDownDirection = ToolStripDropDownDirection.Right;
        this.headingsBTN.ShowDropDownArrow = true;

        dropDown.Items.AddRange(new ToolStripItem[] 
            { heading1BTN, heading2BTN, heading3BTN });
        toolStrip.Items.Add(headingsBTN);
    }
                                                
    void loadFile(string filename) {
        string text = "";                                   

        using (StreamReader sr = new StreamReader(filename))
            while (sr.ReadLine() is string s)
                text += s + "\n";           

        textBox.Text = text;                    
    }                                                       

    void savingFile(string filename) {
        textBox.SaveFile(filename, RichTextBoxStreamType.PlainText);
        MessageBox.Show("Saved Successfully", "File address: " + 
                        filename, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        using (var saveFile = new SaveFileDialog()) {
            // Initialize the SaveFileDialog to specify the RTF extension for the file.
            saveFile.DefaultExt = "*.txt";
            saveFile.Filter = "TXT Files|*.txt";
            if (saveFile.ShowDialog() == DialogResult.OK && saveFile.FileName.Length > 0) {
                savingFile(saveFile.FileName);
            }
        }
    }

    void onQuit(object sender, EventArgs e) {
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