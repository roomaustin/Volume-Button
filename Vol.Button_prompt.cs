using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VolumeButtonMonitor
{
    public partial class MainForm : Form
    {
        // Import the user32.dll for registering hotkeys
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        // Constants for volume keys
        private const int WM_HOTKEY = 0x0312;
        private const int VK_VOLUME_UP = 0xAF; // Volume Up key code
        private const int VK_VOLUME_DOWN = 0xAE; // Volume Down key code

        public MainForm()
        {
            InitializeComponent();
            RegisterHotKey(this.Handle, 1, 0, VK_VOLUME_UP); // Register volume up key
            RegisterHotKey(this.Handle, 2, 0, VK_VOLUME_DOWN); // Register volume down key
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Check if a hotkey message is received
            if (m.Msg == WM_HOTKEY)
            {
                int id = m.WParam.ToInt32();
                switch (id)
                {
                    case 1: // Volume Up key pressed
                        MessageBox.Show("Volume Up button pressed!");
                        break;
                    case 2: // Volume Down key pressed
                        MessageBox.Show("Volume Down button pressed!");
                        break;
                }
            }
        }

        // Unregister hotkeys when the form is closed
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            UnregisterHotKey(this.Handle, 1); // Unregister volume up key
            UnregisterHotKey(this.Handle, 2); // Unregister volume down key
            base.Dispose(disposing);
        }
    }
}
