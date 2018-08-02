using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BackEnd;
using log4net;

namespace RiistaTunnistusOhjelma {
	/// <summary>
	/// Multi-choose button row control.
	/// </summary>
	public partial class MultiChooseControl : UserControl {
		private static readonly ILog Logger = Logging.GetLogger();

		internal event EventHandler<string> Chosen;
		private readonly IList<EventHandler> clickHandlers
			= new List<EventHandler>();

		// Construct component.
		public MultiChooseControl(IList<string> choices) {
			InitializeComponent();
			for (int i = 0; i < choices.Count(); i++) {
				Button button = InitializeChoiceButton(choices[i], i + 1);
				flowLayoutPanel1.Controls.Add(button);
			}

			Logger.Debug($"{nameof(MultiChooseControl)} initialized.");
		}

		/// <summary>
		/// Initialize multi-choice button.
		/// </summary>
		/// <param name="choice">Choice string.</param>
		/// <param name="order">Order in cointainer.</param>
		/// <returns></returns>
		private Button InitializeChoiceButton(string choice, int order) {
			Button newButton = new Button {
				Location = new Point(3, 3),
				Size = new Size(75 * 2, 23),
				TabIndex = order,
				Text = $"{order} : {choice.ToUpperInvariant()}",
				UseVisualStyleBackColor = true,
				AutoEllipsis = false,
				AutoSizeMode = AutoSizeMode.GrowOnly
			};

			EventHandler clickHandler = InitializeButtonClickHandler(choice);
			newButton.Click += clickHandler;
			clickHandlers.Add(clickHandler);

			return newButton;
		}

		private EventHandler InitializeButtonClickHandler(string choice)
			=> (sender, args) => Chosen?.Invoke(null, choice);

		internal void InvokeClickHandlerByOrder(int order) {
			int index = order - 1;
			if (index < clickHandlers.Count)
				clickHandlers[index].Invoke(null, null);
		}
	}
}
