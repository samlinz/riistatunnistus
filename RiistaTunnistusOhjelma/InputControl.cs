using System;
using System.Windows.Forms;

namespace RiistaTunnistusOhjelma {
	public partial class InputControl : UserControl {
		internal event EventHandler<string> Submit;

		public InputControl() {
			InitializeComponent();
		}

		private void inputBox_KeyPress(object sender, KeyPressEventArgs e) {
			if (e.KeyChar == (char) Keys.Enter) {
				Submit(null, inputBox.Text.Trim());
			}
		}
	}
}
