using System;
using static System.Console;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

class Window : Form {
    RichTextBox textBox;
    internal SaveFileDialog SaveFileDialog;

    public Window() {
        ClientSize = new Size(500, 500);
        StartPosition = FormStartPosition.CenterScreen;

        Font font = new Font(FontFamily.GenericMonospace, 13);

        textBox = new RichTextBox();
        textBox.Font = font;
        textBox.Multiline = true;
        textBox.Dock = DockStyle.Fill;
        Controls.Add(textBox);        

        /* ToolStripMenuItem(String, Image, EventHandler)	

        Initializes a new instance of the ToolStripMenuItem class 
        that displays the specified text and image and that does the 
        specified action when the ToolStripMenuItem is clicked. */

        // this is an array
        ToolStripMenuItem[] fileItems = {
            new ToolStripMenuItem("Open file", null, onOpen),
            new ToolStripMenuItem("Quit", null, onQuit),
            new ToolStripMenuItem("Save file", null, onSave)
        };
        
        ToolStripMenuItem[] topItems = {
            new ToolStripMenuItem("File", null, fileItems)
        };
        
        MenuStrip strip = new MenuStrip();
        strip.BackColor = Color.SkyBlue;  

        foreach (var item in topItems)
            strip.Items.Add(item);
        
        strip.Dock = DockStyle.Top;
        Controls.Add(strip);
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
        MessageBox.Show("save successfully", "Address File : " + filename, MessageBoxButtons.OK, MessageBoxIcon.Information);

    }
    

    /* The OpenFileDialog component allows users to browse 
    the folders of their computer or any computer on the 
    network and select one or more files to open. The 
    dialog box returns the path and name of the file the 
    user selected in the dialog box. The FileName property 
    can be set prior to showing the dialog box */   
    void onOpen(object sender, EventArgs e) {       
        using (var dialog = new OpenFileDialog()) { 
            if (dialog.ShowDialog() == DialogResult.OK)
                loadFile(dialog.FileName);
        }
    }

    void onSave(object sender, EventArgs e) {
        using (var saveFile = new SaveFileDialog()) {
            // Initialize the SaveFileDialog to specify the RTF extension for the file.
            saveFile.DefaultExt = "*.doc";
            saveFile.Filter = "DOC Files|*.doc";
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
