namespace Presentation
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGuardarMemoria = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGuardarMemoria
            // 
            this.btnGuardarMemoria.Location = new System.Drawing.Point(12, 12);
            this.btnGuardarMemoria.Name = "btnGuardarMemoria";
            this.btnGuardarMemoria.Size = new System.Drawing.Size(115, 23);
            this.btnGuardarMemoria.TabIndex = 0;
            this.btnGuardarMemoria.Text = "Persistir Memoria";
            this.btnGuardarMemoria.UseVisualStyleBackColor = true;
            this.btnGuardarMemoria.Click += new System.EventHandler(this.btnGuardarMemoria_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnGuardarMemoria);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnGuardarMemoria;
    }
}

