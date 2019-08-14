using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;

namespace EDManagerApp
{
    public partial class MainForm : Form
    {
        private ToolStripButton toolStripButton_Add;
        private ToolStripButton toolStripButton_Edit;
        private ToolStripButton toolStripButton_Delete;
        private ToolStripButton toolStripButton_Import;
        private ToolStripDropDownButton dropDownButton;
        private ToolStripDropDown dropDown;
        private ToolStripButton toolStripButton_StorageOpen;
        private ToolStripButton toolStripButton_StorageSave;
        private ToolStrip toolStrip;
        /// <summary>
        /// Диалог импорта документа
        /// </summary>
        private OpenFileDialog importFileDialog;
        private DataGridView edDataGridView;
        private BindingSource edBindingSource;
        /// <summary>
        /// Файл xsd-схемы
        /// </summary>
        private string xsdSchemePath = "";
        /// <summary>
        /// Внутреннее хранилище документов
        /// </summary>
        private EDStorage edStorage;
        /// <summary>
        /// Менеджер привязки данных с формой
        /// </summary>
        private BindingManagerBase BindingManager
        {
            get => this.BindingContext[this.edStorage.EdDataSet];
        }
        /// <summary>
        /// Текущий выбранный элемент в источнике данных
        /// </summary>
        private ED542 CurrentInBindingManager
        {
            get => (this.BindingManager.Current as ED542);
        }

        /// <summary>
        /// Основная форма приложения
        /// </summary>
        public MainForm()
        {
            Logger.WriteLine("Запуск программы");

            edStorage = new EDStorage();

            InitializeComponent();
            this.edDataGridView.AutoGenerateColumns = true;
            this.edBindingSource.DataSource = this.edStorage.EdDataSet;

            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        /// <summary>
        /// Завершение программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnApplicationExit(object sender, EventArgs e)
        {
            Logger.WriteLine("Завершение программы");
        }

        /// <summary>
        /// Инициализация визуальных компонент
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.edBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.dropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.dropDown = new System.Windows.Forms.ToolStripDropDown();
            this.toolStripButton_StorageOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_StorageSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Add = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Edit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Delete = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton_Import = new System.Windows.Forms.ToolStripButton();
            this.importFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.edDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.edBindingSource)).BeginInit();
            this.toolStrip.SuspendLayout();
            this.dropDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edDataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dropDownButton,
            this.toolStripButton_Add,
            this.toolStripButton_Edit,
            this.toolStripButton_Delete,
            this.toolStripButton_Import});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(696, 25);
            this.toolStrip.TabIndex = 0;
            this.toolStrip.Text = "toolStrip";
            // 
            // dropDownButton
            // 
            this.dropDownButton.DropDown = this.dropDown;
            this.dropDownButton.DropDownDirection = System.Windows.Forms.ToolStripDropDownDirection.BelowRight;
            this.dropDownButton.Image = global::EDManagerApp.Properties.Resources.storage;
            this.dropDownButton.Name = "dropDownButton";
            this.dropDownButton.ShowDropDownArrow = false;
            this.dropDownButton.Size = new System.Drawing.Size(101, 22);
            this.dropDownButton.Text = "Хранилище...";
            // 
            // dropDown
            // 
            this.dropDown.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_StorageOpen,
            this.toolStripButton_StorageSave});
            this.dropDown.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.dropDown.Name = "dropDown";
            this.dropDown.Size = new System.Drawing.Size(97, 50);
            // 
            // toolStripButton_StorageOpen
            // 
            this.toolStripButton_StorageOpen.AutoToolTip = false;
            this.toolStripButton_StorageOpen.Image = global::EDManagerApp.Properties.Resources.storage_load;
            this.toolStripButton_StorageOpen.Name = "toolStripButton_StorageOpen";
            this.toolStripButton_StorageOpen.Size = new System.Drawing.Size(90, 20);
            this.toolStripButton_StorageOpen.Text = "&Загрузить...";
            this.toolStripButton_StorageOpen.ToolTipText = "Загрузить документы из файла-хранилища...";
            this.toolStripButton_StorageOpen.Click += new System.EventHandler(this.OnToolStripButton_StorageOpen_Click);
            // 
            // toolStripButton_StorageSave
            // 
            this.toolStripButton_StorageSave.AutoToolTip = false;
            this.toolStripButton_StorageSave.Image = global::EDManagerApp.Properties.Resources.storage_save;
            this.toolStripButton_StorageSave.Name = "toolStripButton_StorageSave";
            this.toolStripButton_StorageSave.Size = new System.Drawing.Size(95, 20);
            this.toolStripButton_StorageSave.Text = "&Сохранить...";
            this.toolStripButton_StorageSave.ToolTipText = "Сохранить документы в файл-хранилище...";
            this.toolStripButton_StorageSave.Click += new System.EventHandler(this.OnToolStripButton_StorageSave_Click);
            // 
            // toolStripButton_Add
            // 
            this.toolStripButton_Add.AutoToolTip = false;
            this.toolStripButton_Add.Image = global::EDManagerApp.Properties.Resources.document_add;
            this.toolStripButton_Add.Name = "toolStripButton_Add";
            this.toolStripButton_Add.Size = new System.Drawing.Size(70, 22);
            this.toolStripButton_Add.Text = "&Создать";
            this.toolStripButton_Add.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripButton_Add.ToolTipText = "Создать новый документ";
            this.toolStripButton_Add.Click += new System.EventHandler(this.OnButtonAdd_Click);
            // 
            // toolStripButton_Edit
            // 
            this.toolStripButton_Edit.AutoToolTip = false;
            this.toolStripButton_Edit.Image = global::EDManagerApp.Properties.Resources.document_edit;
            this.toolStripButton_Edit.Name = "toolStripButton_Edit";
            this.toolStripButton_Edit.Size = new System.Drawing.Size(107, 22);
            this.toolStripButton_Edit.Text = "&Редактировать";
            this.toolStripButton_Edit.ToolTipText = "Редактировать выбранный документ";
            this.toolStripButton_Edit.Click += new System.EventHandler(this.OnButtonEdit_Click);
            // 
            // toolStripButton_Delete
            // 
            this.toolStripButton_Delete.AutoToolTip = false;
            this.toolStripButton_Delete.Image = global::EDManagerApp.Properties.Resources.document_remove;
            this.toolStripButton_Delete.Name = "toolStripButton_Delete";
            this.toolStripButton_Delete.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton_Delete.Text = "&Удалить";
            this.toolStripButton_Delete.ToolTipText = "Удалить выбранный документ";
            this.toolStripButton_Delete.Click += new System.EventHandler(this.OnButtonDelete_Click);
            // 
            // toolStripButton_Import
            // 
            this.toolStripButton_Import.AutoToolTip = false;
            this.toolStripButton_Import.Image = global::EDManagerApp.Properties.Resources.document_import;
            this.toolStripButton_Import.Name = "toolStripButton_Import";
            this.toolStripButton_Import.Size = new System.Drawing.Size(115, 22);
            this.toolStripButton_Import.Text = "&Импортировать";
            this.toolStripButton_Import.ToolTipText = "Импортировать документ из файла";
            this.toolStripButton_Import.Click += new System.EventHandler(this.OnToolStripButton_Import_Click);
            // 
            // importFileDialog
            // 
            this.importFileDialog.Filter = "Text files(*.xml)|*.xml|All files(*.*)|*.*";
            this.importFileDialog.Title = "Импорт документа";
            // 
            // edDataGridView
            // 
            this.edDataGridView.AllowUserToAddRows = false;
            this.edDataGridView.AllowUserToDeleteRows = false;
            this.edDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edDataGridView.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.edDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.edDataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.edDataGridView.DataSource = this.edBindingSource;
            this.edDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.edDataGridView.Location = new System.Drawing.Point(0, 28);
            this.edDataGridView.MultiSelect = false;
            this.edDataGridView.Name = "edDataGridView";
            this.edDataGridView.ReadOnly = true;
            this.edDataGridView.RowTemplate.DefaultCellStyle.NullValue = null;
            this.edDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.edDataGridView.Size = new System.Drawing.Size(696, 364);
            this.edDataGridView.TabIndex = 1;
            this.edDataGridView.SelectionChanged += new System.EventHandler(this.OnDataGridView_SelectionChanged);
            this.edDataGridView.DoubleClick += new System.EventHandler(this.OnButtonEdit_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 70);
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.NewToolStripMenuItem.Text = "Создать";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.OnButtonAdd_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.EditToolStripMenuItem.Text = "Редактировать";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.OnButtonEdit_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.DeleteToolStripMenuItem.Text = "Удалить";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.OnButtonDelete_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 392);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.edDataGridView);
            this.Name = "MainForm";
            this.Text = "Менеджер документов ED542";
            ((System.ComponentModel.ISupportInitialize)(this.edBindingSource)).EndInit();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.dropDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.edDataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

         }

        /// <summary>
        /// Обработка выбора пользователем элемента в таблице
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (edDataGridView.SelectedCells.Count > 0)
            {
                int selectedrowindex = edDataGridView.SelectedCells[0].RowIndex;
                //синхронизируем визуальный выбранный элемент и текущий элемент внутреннего хранилища
                this.BindingManager.Position = selectedrowindex;
            }
        }

        /// <summary>
        /// Импорт документа из файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnToolStripButton_Import_Click(object sender, EventArgs e)
        {
            if (importFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Logger.WriteLine($"Импорт документа из файла: {importFileDialog.FileName} ...");
            string fileText;
            try
            {
                // читаем файл в строку
                fileText = File.ReadAllText(importFileDialog.FileName);
            }
            catch (Exception ex)
            {
                Logger.WriteLine($"Ошибка чтения из файл: {importFileDialog.FileName}\r\n" + ex.Message);
                Utils.ErrorMessageBox($"Ошибка чтения из файл: {importFileDialog.FileName}\r\n" + ex.Message);
                return;
            }

            var xmlValid = new XmlValidator();

            if (!File.Exists(this.xsdSchemePath))
            {
                // если файл xsd-схемы не задан, то выбираем его через диалоговое окно
                var openXsdFile = new OpenFileDialog();
                openXsdFile.Title = "Выбор xsd-файла для валидации";
                openXsdFile.Filter = "Text files(*.xsd)|*.xsd|All files(*.*)|*.*";

                if (openXsdFile.ShowDialog() == DialogResult.Cancel)
                {
                    Logger.WriteLine($"Отмена импорта документа из файла. Не выбран файл xsd-схемы.");
                    return;
                }
                this.xsdSchemePath = openXsdFile.FileName;
                Logger.WriteLine($"Выбран файл xsd-схемы: {this.xsdSchemePath}");
            }

            if (xmlValid.ValidateXmlDoc(fileText, this.xsdSchemePath))
            {
                Logger.WriteLine("Файл валидный.");
                try
                {
                    var newED542 = new ED542();
                    newED542.FromXmlStr(fileText);
                    AddNewED(newED542);
                    SetCurrentGridRow();
                    Logger.WriteLine($"Загружен документ: {newED542.ToXmlStr()}");
                }
                catch (Exception ex)
                {
                    Logger.WriteLine("Ошибка создания документа ED542 из xml-файла:\r\n" + ex.Message);
                    Utils.ErrorMessageBox("Ошибка создания документа ED542 из xml-файла:\r\n" + ex.Message);
                }
            }
            else
            {
                Logger.WriteLine("Ошибка валидации файла:\n\r" + xmlValid.ValidError);
                Utils.ErrorMessageBox("Ошибка валидации файла:\n\r" + xmlValid.ValidError);
            }
        }

        /// <summary>
        /// Удаление текущего документа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonDelete_Click(object sender, EventArgs e)
        {
            if (BindingManager.Count > 0)
            {
                string message = $"Вы действительно хотите удалить документ {(CurrentInBindingManager.EDNo?.ToString()??"")}?";
                var result = MessageBox.Show(message, "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Logger.WriteLine($"Удален документ: {CurrentInBindingManager.ToXmlStr()}");
                    DeleteCurrentED();
                    SetCurrentGridRow();
                }
            }
        }

        /// <summary>
        /// Создание нового документа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonAdd_Click(object sender, EventArgs e)
        {
            var newED542 = new ED542();
            var addRowToDataForm = new ED542Form(newED542);
            var trayAgain = false;
            do
            {
                trayAgain = false;
                if (addRowToDataForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        newED542.Validate();
                        AddNewED(newED542);
                        SetCurrentGridRow();
                        Logger.WriteLine($"Создан новый документ: {newED542.ToXmlStr()}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        trayAgain = true;
                    }
                }
            } while (trayAgain);
        }

        /// <summary>
        /// Редактирование текущего документа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnButtonEdit_Click(object sender, EventArgs e)
        {
            if (BindingManager.Count > 0)
            {
                // копия текущего документа
                var eD542Old = new ED542(CurrentInBindingManager);
                var addRowToDataForm = new ED542Form(CurrentInBindingManager);
                var trayAgain = false;
                do
                {
                    trayAgain = false;
                    if (addRowToDataForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            CurrentInBindingManager.Validate();
                            Logger.WriteLine($"Изменен документ: {CurrentInBindingManager.ToXmlStr()}");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            trayAgain = true;
                        }
                    }
                    else
                    {
                        // редактирование отменено, возвращаем старые значения в документ
                        CurrentInBindingManager.CopyFrom(eD542Old);
                    }
                } while (trayAgain);
            }
        }

        /// <summary>
        /// Загрузка документов из хранилища
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnToolStripButton_StorageOpen_Click(object sender, EventArgs e)
        {
            List<ED542> newStorageData = edStorage.Load();
            foreach(var ed in newStorageData)
            {
                AddNewED(ed);
            }
            this.SetCurrentGridRow();
        }

        /// <summary>
        /// Сохранение документов в хранилище
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnToolStripButton_StorageSave_Click(object sender, EventArgs e)
        {
            try
            {
                edStorage.Save();
            }
            catch (Exception ex)
            {
                Logger.WriteLine("Ошибка сохранения хранилища сообщений:\r\n" + ex.Message);
                Utils.ErrorMessageBox("Ошибка сохранения хранилища сообщений:\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Добавление нового документа во внутреннее хранилище
        /// </summary>
        /// <param name="newED542">Новый документ</param>
        private void AddNewED(ED542 newED542)
        {
            this.edBindingSource.Add(newED542);
            this.BindingManager.Position = this.BindingManager.Count - 1;
        }

        /// <summary>
        /// Удаление текущего элемента из внутреннего хранилища
        /// </summary>
        private void DeleteCurrentED()
        {
            this.edBindingSource.RemoveCurrent();
            this.BindingManager.Position = this.BindingManager.Count - 1;
        }
        /// <summary>
        /// Выделение строки в визуальном списке
        /// </summary>
        private void SetCurrentGridRow()
        {
            if (this.BindingManager.Count > 0 && this.BindingManager.Position >= 0)
            {
                this.edDataGridView.Rows[this.BindingManager.Position].Selected = true;
                this.edDataGridView.CurrentCell = this.edDataGridView.Rows[this.BindingManager.Position].Cells[0];
            }
        }

    }
}
