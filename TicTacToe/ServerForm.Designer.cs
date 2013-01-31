namespace TicTacToe
{
    partial class ServerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.logListView = new System.Windows.Forms.ListView();
            this.clientColumnHeader = ( ( System.Windows.Forms.ColumnHeader )( new System.Windows.Forms.ColumnHeader() ) );
            this.requestColumnHeader = ( ( System.Windows.Forms.ColumnHeader )( new System.Windows.Forms.ColumnHeader() ) );
            this.responseColumnHeader = ( ( System.Windows.Forms.ColumnHeader )( new System.Windows.Forms.ColumnHeader() ) );
            this.SuspendLayout();
            // 
            // logListView
            // 
            this.logListView.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.clientColumnHeader,
            this.requestColumnHeader,
            this.responseColumnHeader} );
            this.logListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logListView.Location = new System.Drawing.Point( 0, 0 );
            this.logListView.Name = "logListView";
            this.logListView.Size = new System.Drawing.Size( 647, 252 );
            this.logListView.TabIndex = 0;
            this.logListView.UseCompatibleStateImageBehavior = false;
            this.logListView.View = System.Windows.Forms.View.Details;
            // 
            // clientColumnHeader
            // 
            this.clientColumnHeader.Text = "Client";
            this.clientColumnHeader.Width = 93;
            // 
            // requestColumnHeader
            // 
            this.requestColumnHeader.Text = "Request";
            this.requestColumnHeader.Width = 251;
            // 
            // responseColumnHeader
            // 
            this.responseColumnHeader.Text = "Response";
            this.responseColumnHeader.Width = 284;
            // 
            // ServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 647, 252 );
            this.Controls.Add( this.logListView );
            this.Name = "ServerForm";
            this.Text = "Tic Tac Toe Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.ServerForm_FormClosing );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ListView logListView;
        private System.Windows.Forms.ColumnHeader clientColumnHeader;
        private System.Windows.Forms.ColumnHeader requestColumnHeader;
        private System.Windows.Forms.ColumnHeader responseColumnHeader;
    }
}

