using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextCalculator
{
    public partial class Form1 : Form
    {
        private delegate void InputStringChanged(string newText);

        event InputStringChanged UserStringChanged;
        event Action UserChangedFields;

        private bool _userStringValid = false;
        private bool _oldSubstringValid = false;
        private bool _newSubstringValid = false;

        private Calculator calculator;

        private TextBox txtInput;
        private TextBox txtOldSubstring;
        private TextBox txtNewSubstring;
        private NumericUpDown numIndex;
        private Label lblResult;

        private Button btnReplace;
        private Button btnDelete;
        private Button btnGetChar;
        private Button btnLength;
        private Button btnCountVowels;
        private Button btnCountConsonants;
        private Button btnCountSentences;
        private Button btnCountWords;

        public Form1()
        {
            InitializeComponent();
            calculator = new Calculator();
            calculator.OperationCompleted += Calculator_OperationCompleted;
        }
    }
}
