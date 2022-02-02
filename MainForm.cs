/*
 * Created by SharpDevelop.
 * User: Kurbatov
 * Date: 24.11.2021
 * Time: 17:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Kursach
{
	
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	
	public partial class MainForm : Form
	{
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			richTextBox2.Clear();
			
			label2.Text="Процент сжатия:";
			
			Format();
		
			string textAll= richTextBox1.Text;
			var TextF = new TextF();
			var ExFile = new List<string>();
			
			for (int i=0;i<textAll.Length;i+=1)
			{
				ExFile.Add(Convert.ToString(textAll[i]));
			}
			TextF.AddFile(ExFile);
			
			if (checkBox1.Checked) {TextF.CryptBeta();}
			else TextF.Crypt();
			
			ExFile=TextF.LookF();
			
			foreach(string F in ExFile)
			{
				richTextBox2.Text+=F;
			}
			
			double temp=0;
			if (richTextBox2.Text !="") temp = (Convert.ToDouble(richTextBox2.Text.Length)-richTextBox1.Text.Length)/richTextBox1.Text.Length*100;
			label1.Text= Convert.ToString(Math.Truncate(temp*-1))+"%";
			
		}
		void Button2Click(object sender, EventArgs e)
		{
			richTextBox2.Clear();
			
			label2.Text="Процент расшиф.:";
			
			string textAll= richTextBox1.Text;
			var TextF = new TextF();
			var ExFile = new List<string>();
			
			for (int i=0;i<textAll.Length;i+=1)
			{
				ExFile.Add(Convert.ToString(textAll[i]));
			}
			TextF.AddFile(ExFile);
			
			if (checkBox1.Checked) TextF.DecryptBeta();
			else TextF.Decrypt();
			
			ExFile=TextF.LookF();
			
			foreach(string F in ExFile)
			{
				richTextBox2.Text+=F;
			}
			richTextBox2.Text=richTextBox2.Text.Replace("^","");
			
			double temp = 0;
			if (richTextBox2.Text !="") temp = (Convert.ToDouble(richTextBox2.Text.Length)-richTextBox1.Text.Length)/richTextBox1.Text.Length*100;
			else MessageBox.Show("Вы не ввели текст!");
			label1.Text= Convert.ToString(Math.Truncate(temp))+"%";
			
		}

		void Format()
		{
			if (checkBox1.Checked && richTextBox1.Text.Contains("^")) 
			{richTextBox1.Text=richTextBox1.Text.Replace("^","");}
		}
		void MainFormResize(object sender, EventArgs e)
		{
			if ((Size.Width-538)%2 == 0)
			{
				panel2.Width=246+(Size.Width-538)/2;
				panel3.Left=264+(Size.Width-538)/2;
				panel3.Width=246+(Size.Width-538)/2;
			}
		}
		void ToolStripMenuItem1Click(object sender, EventArgs e)
		{
			new Form1().ShowDialog();

		}
		void MainFormLoad(object sender, EventArgs e)
		{
			
			
		}
		void СохранитьToolStripMenuItemClick(object sender, EventArgs e)
		{
			var saver = new SaveFileDialog();
			saver.Filter=("Сохранение | *.txt");
			
			if (saver.ShowDialog() == DialogResult.OK)
			using (var sw = new StreamWriter(saver.FileName))
			{
				sw.WriteLine(richTextBox1.Text);
				sw.WriteLine("RICHTEXTBOX2TEXT");
				sw.WriteLine(richTextBox2.Text);
			}
			
			
		}
		void ЗагрузитьToolStripMenuItemClick(object sender, EventArgs e)
		{	
			richTextBox2.Clear();
			richTextBox1.Clear();
			
			var loader = new OpenFileDialog();
			loader.Filter=("Сохранение | *.txt");
			
			
			if (loader.ShowDialog() == DialogResult.OK)
			using(var sr = new StreamReader(loader.FileName))
			{
				string temp="";
				
				string FText=File.ReadAllText(loader.FileName);
            	if (FText.Contains("RICHTEXTBOX2TEXT"))
            	{
					do
					{
						temp = sr.ReadLine();
						if (temp!="RICHTEXTBOX2TEXT")
						richTextBox1.Text+=temp;
					}
					while(temp!="RICHTEXTBOX2TEXT");
					
					while(temp!=null)
					{
						temp=sr.ReadLine();
						richTextBox2.Text+=temp;
					}
            	}
            	else
            	{
            		do
					{
						temp = sr.ReadLine();
						if (temp!="RICHTEXTBOX2TEXT")
						richTextBox1.Text+=temp;
					}
					while(temp!=null);
            	}
			}
		}
		
	}
	
	class TextF
	{
		public List<string> file = new List<string>();
		
		public void AddFile(List<string> chars)
		{
			for (int i = 0;i<chars.Count;i++)
				file.Add(chars[i]);
		}
		
		public void Crypt()
		{
			int CountOfRepetitions = 0;
			int PosFirstRepetition = 0;
			int i = 1;
			string Repetition;
			if (file.Count==0) MessageBox.Show("Вы не ввели текст!");
			while(i < file.Count)
			{
				Repetition = file[i];
				CountOfRepetitions = 0;
				
				if (file[i-1] == file[i])
				{
					PosFirstRepetition=i-1;
					Repetition = file[i];
					
					while (file[i]==Repetition)
					{
						file.RemoveAt(i);
						CountOfRepetitions++;
						if (i==file.Count) break;
					}
					file.Insert(i,Convert.ToString(CountOfRepetitions));
				}
				i++;
			}
		}
		
		public void CryptBeta()
		{
			int CountOfRepetitions = 0;
			int PosFirstRepetition = 0;
			int i = 0;
			string Repetition;
			bool Digit=false;
			
			try
			{
				if (Char.IsDigit(Convert.ToChar(file[0])))
				{file.Insert(0,"^");Digit=true;}
			
				i=1;
				while(i < file.Count)
				{
					if (Char.IsDigit(Convert.ToChar(file[i])) && Digit == false)
					{file.Insert(i,"^");Digit=true;i++;continue;}
				
					if (Char.IsDigit(Convert.ToChar(file[i])) && Digit) {i++;}
					else {Digit=false;i++;}
				}
			
				i=0;
				Digit=false;
			
			
				file.Add("~");
				while(i < file.Count)
				{
					Repetition = file[i];
					CountOfRepetitions = 0; 
				
					if (file[i]=="^")
					{
						while (Char.IsDigit(Convert.ToChar(file[i+1])))
							i++;
						i++;
					}
					if (i==0) i++;
					
				
					if (file[i-1] == file[i])
					{
						PosFirstRepetition=i-1;
						Repetition = file[i];
					
						while (file[i]==Repetition)
						{
							file.RemoveAt(i);
							CountOfRepetitions++;
							if (i==file.Count) break;
						}
						file.Insert(i,Convert.ToString(CountOfRepetitions));
					}
					i++;
				}
				file.RemoveAt(file.Count-1);
			}
			catch{if (file.Count==0) MessageBox.Show("Вы не ввели текст!");}
			try{
			if (file[file.Count-1]=="~") {file.RemoveAt(file.Count-1);}
			}
			catch{}
			}
			
		
		public void Decrypt()
		{
			int multiplyer = 0;
			int i = 0;
			
			
			try
			{
				file.Add("~");
				while(i<file.Count) //s11g2j13df  sg2j13df 
				{
					if (char.IsDigit(Convert.ToChar(file[i])))  
					{
						while (char.IsDigit(Convert.ToChar(file[i])))
						{
							multiplyer=multiplyer*10+Convert.ToInt32(file[i]);
							file.RemoveAt(i);
							if (multiplyer>5000) throw new Exception();
						}
							
						for (int j = 0;j<multiplyer;j++)
							file.Insert(i,file[i-1]);
					}
					multiplyer=0;
					i++;
				}
				file.RemoveAt(file.Count-1);
			}
			catch{MessageBox.Show("Слишком большое количество символов после дешифровки!");file.Clear();file.Add("ERROR");}
		}
		
		public void DecryptBeta() //s^11g2j^13df  
		{
			int multiplyer = 0;
			int i = 0;
			
			try
			{
				file.Add("~");
				while(i<file.Count)
				{
					if (file[i]=="^")
					{
						i++;
						while(char.IsDigit(Convert.ToChar(file[i])))
						{
							i++;
						}
					}
					
					if (char.IsDigit(Convert.ToChar(file[i])))  
					{
						while (char.IsDigit(Convert.ToChar(file[i])))
						{
							multiplyer=multiplyer*10+Convert.ToInt32(file[i]);
							file.RemoveAt(i);
							if (multiplyer>5000) throw new Exception();
						}
							
						for (int j = 0;j<multiplyer;j++)
							file.Insert(i,file[i-1]);
					}
					multiplyer=0;
					i++;
				}
				file.RemoveAt(file.Count-1);
			}
			catch{MessageBox.Show("Слишком большое количество символов после дешифровки!");file.Clear();file.Add("ERROR");}
		}
		
		
		
		public List<string> LookF()
		{
			return file;
		}
		
	}
	
}


	
	
	