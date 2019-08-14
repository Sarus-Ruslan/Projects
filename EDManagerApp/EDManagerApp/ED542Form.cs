using System.Windows.Forms;

namespace EDManagerApp
{
    /// <summary>
    /// Форма редактирования документа
    /// </summary>
    public partial class ED542Form : Form
    {
        private BindingSource bindingSource;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ed542"></param>
        public ED542Form(ED542 ed542)
        {
            bindingSource = new BindingSource();
            bindingSource.DataSource = ed542;
            InitializeComponent();
            BindingFields();
        }

        /// <summary>
        /// Привязка данных к визуальным компонентам
        /// </summary>
        private void BindingFields()
        {
            Binding b = numericUpDown1.DataBindings.Add("Text", bindingSource, "EDNo", true);
            b.BindingComplete += new BindingCompleteEventHandler(OnBindingComplete);
            b = dateTimePicker1.DataBindings.Add("Text", bindingSource, "EDDate", true);
            b.BindingComplete += new BindingCompleteEventHandler(OnBindingComplete);
            b = textBox1.DataBindings.Add("Text", bindingSource, "EDAuthor", true);
            b.BindingComplete += new BindingCompleteEventHandler(OnBindingComplete);
            b = comboBox1.DataBindings.Add("Text", bindingSource, "RepeatReceptInqCode", true, DataSourceUpdateMode.OnValidation, null, "d");
            b.BindingComplete += new BindingCompleteEventHandler(OnBindingComplete);
            b = textBox2.DataBindings.Add("Text", bindingSource, "EDTypeNo", true);
            b.BindingComplete += new BindingCompleteEventHandler(OnBindingComplete);
            b = textBox3.DataBindings.Add("Text", bindingSource, "ARMNo", true);
            b.BindingComplete += new BindingCompleteEventHandler(OnBindingComplete);
        }

        /// <summary>
        /// Обработка ошибок ввода данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnBindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState != BindingCompleteState.Success)
            {
                Utils.InfoMessageBox("Ошибка ввода данных:\r\n " + e.ErrorText);
            }
        }
    }
}
