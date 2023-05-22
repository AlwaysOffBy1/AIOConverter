using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace AIOConverter
{
    public partial class MainForm : Form
    {
        public enum Conversion{hex2ascii, ascii2hex, name2UNI, UNI2name}
        public MainForm()
        {
            InitializeComponent();
        }
        private void tbInput_TextChanged(object sender, EventArgs e)
        {
            tbOutput.Text = "";
            List<string> input = new List<string>(Regex.Split(tbInput.Text, "\n|,")).ToList();
            foreach(string s in input)
            {
                if (this.rbHex.Checked) tbOutput.AppendText(hex2ascii(s));
                if (this.rbASCII.Checked) tbOutput.AppendText(ascii2hex(s));
                if (this.rbAuto.Checked) tbOutput.AppendText(AutoConvert(s, DetermineProperConversion()));
                tbOutput.AppendText(Environment.NewLine);
            }
        }

        //Converters
        public Conversion DetermineProperConversion()
        {
            //if regex = AAAB then 
            return Conversion.hex2ascii;
        }
        public string AutoConvert(string input, Conversion c)
        {
            switch (c)
            {
                case Conversion.hex2ascii:
                    return hex2ascii(input);
                case Conversion.ascii2hex:
                    return ascii2hex(input);
                case Conversion.name2UNI:
                    return name2UNI(input);
                case Conversion.UNI2name:
                    return UNI2name(input);
            }
            return "";
        }
        public string hex2ascii(string input)
        {
            string ascii = "";
            try
            {
                for (int i = 0; i < input.Length - 1; i += 2)
                {
                    string s = input.Substring(i, 2);
                    char c = (char)Convert.ToInt32(s, 16);
                    ascii += c;
                }
            }catch(Exception ex) { Console.WriteLine(ex.Message); }
            return ascii;
        }
        public string ascii2hex(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
                sb.AppendFormat("{0:X2}", (int)c);
            return sb.ToString().Trim();
        }

        public string name2UNI(string input)
        {
            return input;
        }
        public string UNI2name(string input)
        {
            return input;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            
        }
    }
}