using System;
using static System.Console;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows;

public class Window : Form {
    RichTextBox textBox;
    ToolStrip toolStrip;
    ToolStripButton boldBTN, italicBTN, underlineBTN;  // BTN - button
    ToolStripButton alignLeftBTN, alignCenterBTN, alignRightBTN, bulletListBTN;
    ToolStripSeparator bar;
    Font font;

    ToolStripDropDownButton headingsBTN;
    ToolStripDropDown dropDown;
    ToolStripButton heading1BTN, heading2BTN, heading3BTN;

    public Window() {
        ClientSize = new Size(500, 500);
        StartPosition = FormStartPosition.CenterScreen;

        font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Regular);

        textBox = new RichTextBox();
        textBox.Font = font;
        textBox.Multiline = true;
        textBox.Dock = DockStyle.Fill;
        textBox.SelectionIndent = 30;
        textBox.SelectionRightIndent = 30;
        Controls.Add(textBox);        

        /* ToolStripMenuItem(String, Image, EventHandler)	
        Initializes a new instance of the ToolStripMenuItem class that displays the specified text 
        and image and that does the specified action when the ToolStripMenuItem is clicked. */
        ToolStripMenuItem[] fileItems = {
            new ToolStripMenuItem("Open", null, onOpen),
            new ToolStripMenuItem("Quit", null, onQuit),
            new ToolStripMenuItem("Save", null, onSave)
        };
        
        ToolStripMenuItem[] topItems = {
            new ToolStripMenuItem("File", null, fileItems)
        };
        
        MenuStrip menuStrip = new MenuStrip();
        menuStrip.BackColor = Color.SkyBlue;  

        foreach (var item in topItems)
            menuStrip.Items.Add(item);
        
        this.toolStrip = new ToolStrip();
        toolStrip.GripStyle = ToolStripGripStyle.Hidden;  // hides the "grip" which are three dots you use to move the buttons

        initIcons();
        buttonActions();

        toolStrip.Dock = DockStyle.Top;  // ToolStrip is just a "strip" to which different tools are added e.g. buttons.
        Controls.Add(toolStrip);

        menuStrip.Dock = DockStyle.Top;  // adding it after toolStrip to make the menu on top of buttons
        Controls.Add(menuStrip);         // because whatever is added last goes on top.
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

    // NEEDS TO BE FIXED, DOESN'T WORK PROPERLY. 
    void styleButtonChecked(object sender, EventArgs e) {
        if (boldBTN.Checked) 
            textBox.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Bold);
        else if (italicBTN.Checked)
            textBox.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Italic);
        else if (underlineBTN.Checked)
            textBox.Font = new Font(FontFamily.GenericMonospace, 11, FontStyle.Underline);
        else
            textBox.Font = font;
    }

    void buttonActions() {
        boldBTN.Click += new EventHandler(styleButtonChecked);
        italicBTN.Click += new EventHandler(styleButtonChecked);
        underlineBTN.Click += new EventHandler(styleButtonChecked);

        alignRightBTN.Click += new EventHandler(alignmentButtonChecked);
        alignCenterBTN.Click += new EventHandler(alignmentButtonChecked);
        alignRightBTN.Click += new EventHandler(alignmentButtonChecked);
    }

    ToolStripButton iconButton(string name) {
        ToolStripButton b = new ToolStripButton();
        b.Image = Bitmap.FromFile(Path.Combine("C:/Users/Chyngyz/Documents/text-editor-/icons", name + ".png"));
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

        boldBTN.CheckOnClick = italicBTN.CheckOnClick = underlineBTN.CheckOnClick = true;
        alignLeftBTN.CheckOnClick = alignCenterBTN.CheckOnClick = alignRightBTN.CheckOnClick = true;
        bulletListBTN.CheckOnClick = true;

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
        // Set the drop-down direction.
        this.headingsBTN.DropDownDirection = ToolStripDropDownDirection.Right;
        // Do not show a drop-down arrow.
        this.headingsBTN.ShowDropDownArrow = true;

        // Declare three buttons, set their foreground color and text, 
        // and add the buttons to the drop-down.
        this.heading1BTN = iconButton("HeadingOne_16x");
        this.heading2BTN = iconButton("HeadingTwo_16x");
        this.heading3BTN = iconButton("HeadingThree_16x");

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
        // Save the contents of the RichTextBox into the file.
        textBox.SaveFile(filename, RichTextBoxStreamType.PlainText);
        MessageBox.Show("Saved Successfully", "File address: " + filename, MessageBoxButtons.OK, MessageBoxIcon.Information);
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