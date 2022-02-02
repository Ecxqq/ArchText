/*
 * Created by SharpDevelop.
 * User: Kurbatov
 * Date: 02.12.2021
 * Time: 20:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kursach
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class Form1 : Form
	{
		public Form1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void ComboBox1SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (comboBox1.SelectedIndex)
			{
				case 0:
					richTextBox1.Text="Так как у алгоритма RLE Есть ограничение из-за которого" +
						" алгоритм может шифровать числа подобным другим символам образом" +
						", поэтому, если необходимо сохранить числовые значения неизменными" +
						", можно использовать алгоритм, не меняющий числа в тексте, также если имеется" +
						" зашифрованный текст \"с сохранением чисел\" (имеет специальные знаки ^), " +
						"тогда следует выбрать \"режим сохранения чисел\" при дешифровке, в свою " +
						"очередь данный режим имеет некоторые особенности, такие как: " +
						"невозможность использования символа ^ в тексте, а также появление" +
						" дополнительных символов в зашифрованной последовательности." 						;
					break;
				case 1:
					richTextBox1.Text="Кнопка сжатие создает текст который был обработан" +
						" алгоритмом RLE, а при нажатии \"флажка\", его усовершенствованной версии.";
					break;
				case 2:
					richTextBox1.Text="Кнопка расшифровка возвращает текст который был обработан" +
						" алгоритмом RLE, а при нажатии \"флажка\", его усовершенствованной версией.";
					break;
					
			}
		}
	}
}
