namespace Main
{
    partial class NumberOfActors
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NumberOfActorsView = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Název = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Rok = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Premiéra = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Počet_Herců = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(54, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(540, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Zobrazit počet obsazených herců";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(49, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Počet obsazených herců";
            // 
            // NumberOfActorsView
            // 
            this.NumberOfActorsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.Název,
            this.Rok,
            this.Premiéra,
            this.Počet_Herců});
            this.NumberOfActorsView.FullRowSelect = true;
            this.NumberOfActorsView.GridLines = true;
            this.NumberOfActorsView.Location = new System.Drawing.Point(54, 136);
            this.NumberOfActorsView.Name = "NumberOfActorsView";
            this.NumberOfActorsView.Size = new System.Drawing.Size(540, 169);
            this.NumberOfActorsView.TabIndex = 4;
            this.NumberOfActorsView.UseCompatibleStateImageBehavior = false;
            this.NumberOfActorsView.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 44;
            // 
            // Název
            // 
            this.Název.Text = "Název";
            this.Název.Width = 187;
            // 
            // Rok
            // 
            this.Rok.Text = "Rok";
            this.Rok.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Rok.Width = 70;
            // 
            // Premiéra
            // 
            this.Premiéra.Text = "Premiéra";
            this.Premiéra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Premiéra.Width = 152;
            // 
            // Počet_Herců
            // 
            this.Počet_Herců.Text = "Počet_Herců";
            this.Počet_Herců.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Počet_Herců.Width = 83;
            // 
            // NumberOfActors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 327);
            this.Controls.Add(this.NumberOfActorsView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Name = "NumberOfActors";
            this.Text = "Počet obsazených herců";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView NumberOfActorsView;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader Rok;
        private System.Windows.Forms.ColumnHeader Název;
        private System.Windows.Forms.ColumnHeader Počet_Herců;
        private System.Windows.Forms.ColumnHeader Premiéra;
    }
}