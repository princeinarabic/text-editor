// // // using System;
// // // using System.Collections.Generic;
// // // using System.ComponentModel;
// // // using System.Data;
// // // using System.Drawing;
// // // using System.Text;
// // // using System.Windows.Forms;


// // // // public class Form1 : Form {
// // // //     private ToolStripContainer toolStripContainer1;
// // // //     private ToolStrip toolStrip1;

// // // //     public Form1() {
// // // //         InitializeComponent();
// // // //     }
    
// // // //     private void InitializeComponent() {
// // // //         toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
// // // //         toolStrip1 = new System.Windows.Forms.ToolStrip();
// // // //         // Add items to the ToolStrip.
// // // //         toolStrip1.Items.Add("One");
// // // //         toolStrip1.Items.Add("Two");
// // // //         toolStrip1.Items.Add("Three");
// // // //         // Add the ToolStrip to the top panel of the ToolStripContainer.
// // // //         toolStripContainer1.TopToolStripPanel.Controls.Add(toolStrip1);
// // // //         // Add the ToolStripContainer to the form.
// // // //         Controls.Add(toolStripContainer1);
// // // //     }

// // // //     class Editor {
// // // //     [STAThread]
// // // //     static void Main() {
// // // //         Form1 w = new Form1();
// // // //         Application.Run(w);
// // // //     }
// // // // }
// // // // }



// // // // This code example demonstrates how to use ToolStripPanel
// // // // controls with a multiple document interface (MDI).
// // // public class Form1 : Form
// // // {
// // //     public Form1()
// // //     {
// // //         // Make the Form an MDI parent.
// // //         this.IsMdiContainer = true;

// // //         // Create ToolStripPanel controls.
// // //         ToolStripPanel tspTop = new ToolStripPanel();
// // //         ToolStripPanel tspBottom = new ToolStripPanel();
// // //         ToolStripPanel tspLeft = new ToolStripPanel();
// // //         ToolStripPanel tspRight = new ToolStripPanel();

// // //         // Dock the ToolStripPanel controls to the edges of the form.
// // //         tspTop.Dock = DockStyle.Top;
// // //         tspBottom.Dock = DockStyle.Bottom;
// // //         tspLeft.Dock = DockStyle.Left;
// // //         tspRight.Dock = DockStyle.Right;

// // //         // Create ToolStrip controls to move among the 
// // //         // ToolStripPanel controls.

// // //         // Create the "Top" ToolStrip control and add
// // //         // to the corresponding ToolStripPanel.
// // //         ToolStrip tsTop = new ToolStrip();
// // //         tsTop.Items.Add("Top");
// // //         tspTop.Join(tsTop);

// // //         // Create the "Bottom" ToolStrip control and add
// // //         // to the corresponding ToolStripPanel.
// // //         ToolStrip tsBottom = new ToolStrip();
// // //         tsBottom.Items.Add("Bottom");
// // //         tspBottom.Join(tsBottom);

// // //         // Create the "Right" ToolStrip control and add
// // //         // to the corresponding ToolStripPanel.
// // //         ToolStrip tsRight = new ToolStrip();
// // //         tsRight.Items.Add("Right");
// // //         tspRight.Join(tsRight);

// // //         // Create the "Left" ToolStrip control and add
// // //         // to the corresponding ToolStripPanel.
// // //         ToolStrip tsLeft = new ToolStrip();
// // //         tsLeft.Items.Add("Left");
// // //         tspLeft.Join(tsLeft);

// // //         // Create a MenuStrip control with a new window.
// // //         MenuStrip ms = new MenuStrip();
// // //         ToolStripMenuItem windowMenu = new ToolStripMenuItem("Window");
// // //         ToolStripMenuItem windowNewMenu = new ToolStripMenuItem("New", null, new EventHandler(windowNewMenu_Click));
// // //         windowMenu.DropDownItems.Add(windowNewMenu);
// // //         ((ToolStripDropDownMenu)(windowMenu.DropDown)).ShowImageMargin = false;
// // //         ((ToolStripDropDownMenu)(windowMenu.DropDown)).ShowCheckMargin = true;

// // //         // Assign the ToolStripMenuItem that displays 
// // //         // the list of child forms.
// // //         ms.MdiWindowListItem = windowMenu;

// // //         // Add the window ToolStripMenuItem to the MenuStrip.
// // //         ms.Items.Add(windowMenu);

// // //         // Dock the MenuStrip to the top of the form.
// // //         ms.Dock = DockStyle.Top;

// // //         // The Form.MainMenuStrip property determines the merge target.
// // //         this.MainMenuStrip = ms;

// // //         // Add the ToolStripPanels to the form in reverse order.
// // //         this.Controls.Add(tspRight);
// // //         this.Controls.Add(tspLeft);
// // //         this.Controls.Add(tspBottom);
// // //         this.Controls.Add(tspTop);

// // //         // Add the MenuStrip last.
// // //         // This is important for correct placement in the z-order.
// // //         this.Controls.Add(ms);
// // //     }

// // //     // This event handler is invoked when 
// // //     // the "New" ToolStripMenuItem is clicked.
// // //     // It creates a new Form and sets its MdiParent 
// // //     // property to the main form.
// // //     void windowNewMenu_Click(object sender, EventArgs e)
// // //     {
// // //         Form f = new Form();
// // //         f.MdiParent = this;
// // //         f.Text = "Form - " + this.MdiChildren.Length.ToString();
// // //         f.Show();
// // //     }
// // // }

// // // class Editor {
// // //     [STAThread]
// // //     static void Main() {
// // //         Form1 w = new Form1();
// // //         Application.Run(w);
// // //     }
// // // }

// // using System;
// // using System.Collections.Generic;
// // using System.ComponentModel;
// // using System.Data;
// // using System.Drawing;
// // using System.Windows.Forms;

// // namespace WindowsApplication11
// // {
// //     public class Form1 : Form
// //     {
// //         private ToolStripButton toolStripButton1;
// //         private ToolStripButton toolStripButton2;
// //         private ToolStrip toolStrip1;
    
// //         public Form1()
// //         {
// //             InitializeComponent();
// //         }
// //         [STAThread]
// //         static void Main()
// //         {
// //             Application.EnableVisualStyles();
// //             Application.Run(new Form1());
// //         }

// //         private void InitializeComponent()
// //         {
// //             this.toolStrip1 = new System.Windows.Forms.ToolStrip();
// //             this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
// //             this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
// //             this.toolStrip1.SuspendLayout();
// //             this.SuspendLayout();
// //             // 
// //             // toolStrip1
// //             // 
// //             this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
// //             this.toolStripButton1,
// //             this.toolStripButton2});
// //             this.toolStrip1.Location = new System.Drawing.Point(0, 0);
// //             this.toolStrip1.Name = "toolStrip1";
// //             this.toolStrip1.TabIndex = 0;
// //             this.toolStrip1.Text = "toolStrip1";
// //             // 
// //             // toolStripButton1
// //             //
// //             this.toolStripButton1.Image = Bitmap.FromFile("c:\\NewItem.bmp");
// //             this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
// //             this.toolStripButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
// //             this.toolStripButton1.Name = "toolStripButton1";
// //             this.toolStripButton1.Text = "&New";
// //             this.toolStripButton1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
// //             this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
// //             // 
// //             // toolStripButton2
// //             // 
// //             this.toolStripButton2.Image = Bitmap.FromFile("c:\\OpenItem.bmp");
// //             this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
// //             this.toolStripButton2.Name = "toolStripButton2";
// //             this.toolStripButton2.Text = "&Open";
// //             this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
// //             // 
// //             // Form1
// //             // 
// //             this.ClientSize = new System.Drawing.Size(292, 273);
// //             this.Controls.Add(this.toolStrip1);
// //             this.Name = "Form1";
// //             this.toolStrip1.ResumeLayout(false);
// //             this.ResumeLayout(false);
// //             this.PerformLayout();
// //         }

// //         private void toolStripButton1_Click(object sender, EventArgs e)
// //         {
// //             MessageBox.Show("You have mail.");
// //         }

// //         private void toolStripButton2_Click(object sender, EventArgs e)
// //         {
// //             // Add the response to the Click event here.
// //         }
// //     }
// // }

// using System;
// using System.Collections.Generic;
// using System.ComponentModel;
// using System.Data;
// using System.Drawing;
// using System.Text;
// using System.Windows.Forms;

// public class Form1 : Form
// {
//     private ToolStripContainer tsc;
//     private RichTextBox rtb;

//     public Form1()
//     {
//         InitializeComponent();
//     }    
// [STAThread]
// static void Main()
//     {
//         Application.EnableVisualStyles();
//         Application.Run(new Form1());
//     }

//     private void InitializeComponent()
//     {
//         this.tsc = new System.Windows.Forms.ToolStripContainer();
//         this.rtb = new System.Windows.Forms.RichTextBox();
//         this.tsc.ContentPanel.Controls.Add(this.rtb);
//         this.Controls.Add(this.tsc);
//     }
// }