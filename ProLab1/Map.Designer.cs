namespace ProLab1
{
    partial class Map
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Map));
            this.which_chests = new System.Windows.Forms.Label();
            this.game_timer = new System.Windows.Forms.Timer(this.components);
            this.ch_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.w_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.t_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.r_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.m_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.c_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.b_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.wasp_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // which_chests
            // 
            this.which_chests.AutoSize = true;
            this.which_chests.BackColor = System.Drawing.Color.Transparent;
            this.which_chests.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.which_chests.Location = new System.Drawing.Point(0, -1);
            this.which_chests.Name = "which_chests";
            this.which_chests.Size = new System.Drawing.Size(117, 16);
            this.which_chests.TabIndex = 0;
            this.which_chests.Text = "Chests collected: ";
            // 
            // game_timer
            // 
            this.game_timer.Enabled = true;
            this.game_timer.Interval = 1000;
            this.game_timer.Tick += new System.EventHandler(this.game_timer_tick);
            // 
            // ch_imagelist
            // 
            this.ch_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ch_imagelist.ImageStream")));
            this.ch_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.ch_imagelist.Images.SetKeyName(0, "miner.png");
            // 
            // w_imagelist
            // 
            this.w_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("w_imagelist.ImageStream")));
            this.w_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.w_imagelist.Images.SetKeyName(0, "wall1.jpg");
            this.w_imagelist.Images.SetKeyName(1, "wall2.jpg");
            // 
            // t_imagelist
            // 
            this.t_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("t_imagelist.ImageStream")));
            this.t_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.t_imagelist.Images.SetKeyName(0, "summertree.png");
            this.t_imagelist.Images.SetKeyName(1, "summertrees.png");
            this.t_imagelist.Images.SetKeyName(2, "wintertree.png");
            this.t_imagelist.Images.SetKeyName(3, "wintertrees.png");
            // 
            // r_imagelist
            // 
            this.r_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("r_imagelist.ImageStream")));
            this.r_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.r_imagelist.Images.SetKeyName(0, "rock.png");
            this.r_imagelist.Images.SetKeyName(1, "rocks.png");
            // 
            // m_imagelist
            // 
            this.m_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("m_imagelist.ImageStream")));
            this.m_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.m_imagelist.Images.SetKeyName(0, "summermountain.png");
            this.m_imagelist.Images.SetKeyName(1, "wintermountain.png");
            // 
            // c_imagelist
            // 
            this.c_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("c_imagelist.ImageStream")));
            this.c_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.c_imagelist.Images.SetKeyName(0, "copperchest.png");
            this.c_imagelist.Images.SetKeyName(1, "silverchest.png");
            this.c_imagelist.Images.SetKeyName(2, "goldchest.png");
            this.c_imagelist.Images.SetKeyName(3, "emeraldchest.png");
            // 
            // b_imagelist
            // 
            this.b_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("b_imagelist.ImageStream")));
            this.b_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.b_imagelist.Images.SetKeyName(0, "bird.png");
            // 
            // wasp_imagelist
            // 
            this.wasp_imagelist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("wasp_imagelist.ImageStream")));
            this.wasp_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            this.wasp_imagelist.Images.SetKeyName(0, "wasp.png");
            // 
            // Map
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Honeydew;
            this.ClientSize = new System.Drawing.Size(984, 961);
            this.Controls.Add(this.which_chests);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Map";
            this.Text = "Treasure Hunter Game";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label which_chests;
        private System.Windows.Forms.Timer game_timer;
        private System.Windows.Forms.ImageList ch_imagelist;
        private System.Windows.Forms.ImageList w_imagelist;
        private System.Windows.Forms.ImageList t_imagelist;
        private System.Windows.Forms.ImageList r_imagelist;
        private System.Windows.Forms.ImageList m_imagelist;
        private System.Windows.Forms.ImageList c_imagelist;
        private System.Windows.Forms.ImageList b_imagelist;
        private System.Windows.Forms.ImageList wasp_imagelist;
    }
}

