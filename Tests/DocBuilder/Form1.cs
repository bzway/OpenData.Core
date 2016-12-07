﻿using Bzway.Common.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DocBuilder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = this.textBox1.Text.Trim();
            var parser = ParserManager.Get(input);
            this.newList.Add(new NewsModel()
            {
                Author = parser.Author,
                Content = parser.Content,
                ContentSourceUrl = parser.Url,
                Url = string.Empty,
                CreatedBy = string.Empty,
                CreatedOn = DateTime.UtcNow,
                UpdatedBy = string.Empty,
                UpdatedOn = DateTime.UtcNow,
                Digest = parser.Digest,
                Id = Guid.NewGuid().ToString("N"),
                IsReleased = false,
                LastUpdateTime = DateTime.UtcNow,
                MaterialID = string.Empty,
                MediaId = string.Empty,
                OfficialAccount = string.Empty,
                ShowCoverPicture = false,
                SortBy = 0,
                ThumbMediaId = parser.ThumbMedia,
                Title = parser.Title
            });
        }
        List<NewsModel> newList=new List<NewsModel>();
        private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var path = this.openFileDialog1.FileName;
                var list = JsonConvert.DeserializeObject<List<NewsModel>>(File.ReadAllText(path));
                list.AddRange(this.newList);
                File.WriteAllText(path, JsonConvert.SerializeObject(list));
            }
        }
    }

    public class NewsModel
    {
        public virtual string CreatedBy { get; set; }


        public virtual DateTime CreatedOn { get; set; }

        public virtual string Id { get; set; }

        public virtual string UpdatedBy { get; set; }

        public virtual DateTime UpdatedOn { get; set; }
        /// <summary>
        /// 所属公众号
        /// </summary>
        public string OfficialAccount { get; set; }
        public string MaterialID { get; set; }
        /// <summary>
        /// 微信唯一标识
        /// </summary>
        public string MediaId { get; set; }

        public int SortBy { get; set; }
        /// <summary>
        /// 图文消息的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 图文消息的封面图片素材id（必须是永久mediaID）
        /// </summary>
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 是否显示封面，0为false，即不显示，1为true，即显示
        /// </summary>
        public bool ShowCoverPicture { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 图文消息的摘要，仅有单图文消息才有摘要，多图文此处为空
        /// </summary>
        public string Digest { get; set; }
        /// <summary>
        /// 图文消息的具体内容，支持HTML标签，必须少于2万字符，小于1M，且此处会去除JS
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 图文页的URL，或者，当获取的列表是图片素材列表时，该字段是图片的URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图文消息的原文地址，即点击“阅读原文”后的URL
        /// </summary>
        public string ContentSourceUrl { get; set; }
        /// <summary>
        /// 这篇图文消息素材的最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        public bool IsReleased { get; set; }

    }
}
