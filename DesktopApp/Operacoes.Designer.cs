using System.Drawing;
using System.Windows.Forms;

namespace DesktopApp
{
    partial class Operacoes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvOperacoes = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cbGroupBy = new System.Windows.Forms.ComboBox();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportCSV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperacoes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvOperacoes
            // 
            this.dgvOperacoes.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvOperacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOperacoes.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvOperacoes.Location = new System.Drawing.Point(12, 56);
            this.dgvOperacoes.Name = "dgvOperacoes";
            this.dgvOperacoes.Size = new System.Drawing.Size(873, 324);
            this.dgvOperacoes.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Agrupar";
            // 
            // cbGroupBy
            // 
            this.cbGroupBy.FormattingEnabled = true;
            this.cbGroupBy.Items.AddRange(new object[] {
            "",
            "Ativo",
            "Tipo de operação",
            "Conta"});
            this.cbGroupBy.Location = new System.Drawing.Point(85, 19);
            this.cbGroupBy.Name = "cbGroupBy";
            this.cbGroupBy.Size = new System.Drawing.Size(156, 21);
            this.cbGroupBy.TabIndex = 2;
            this.cbGroupBy.SelectedIndexChanged += new System.EventHandler(this.cbGroupBy_SelectedIndexChanged);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(397, 8);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(129, 40);
            this.btnExportExcel.TabIndex = 3;
            this.btnExportExcel.Text = "Download Excel";
            this.btnExportExcel.UseVisualStyleBackColor = true;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportarExcel_Click);
            this.btnExportExcel.Enabled = false;
            // 
            // btnExportCSV
            // 
            this.btnExportCSV.Location = new System.Drawing.Point(553, 8);
            this.btnExportCSV.Name = "btnExportCSV";
            this.btnExportCSV.Size = new System.Drawing.Size(129, 40);
            this.btnExportCSV.TabIndex = 4;
            this.btnExportCSV.Text = "Download CSV";
            this.btnExportCSV.UseVisualStyleBackColor = true;
            this.btnExportCSV.Click += new System.EventHandler(this.btnExportCSV_Click);
            this.btnExportCSV.Enabled = false;
            // 
            // Operacoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(896, 465);
            this.Controls.Add(this.btnExportCSV);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.cbGroupBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvOperacoes);
            this.Name = "Operacoes";
            this.Text = "Operaçoes";
            this.Load += new System.EventHandler(this.Operacoes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOperacoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvOperacoes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGroupBy;
        private Button btnExportExcel;
        private Button btnExportCSV;
    }
}

