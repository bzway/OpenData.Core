﻿using OpenData.Data.Core;

namespace OpenData.Framework.Core.Entity
{

    public class PageView : BaseEntity
    {
        public BlockType Type { get; set; }
        public string Name { get; set; }

        public string EntityName { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public int OrderBy { get; set; }
    }

    public enum BlockType
    {
        StaticHtml,
        CreateView,
        DeleteView,
        UpdateView,
        QueryView,
    }


}