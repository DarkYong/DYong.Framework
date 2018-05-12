

namespace DYong.Code
{
    /// <summary>
    /// 树状表格VModel
    /// </summary>
    public class TreeGridModel
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 父级节点ID
        /// </summary>
        public string parentId { get; set; }
        /// <summary>
        /// 显示文本
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// 是否中间节点
        /// </summary>
        public bool isLeaf { get; set; }
        /// <summary>
        /// 是否展开
        /// </summary>
        public bool expanded { get; set; }
        /// <summary>
        /// 是否已加载
        /// </summary>
        public bool loaded { get; set; }
        /// <summary>
        /// 节点内容
        /// </summary>
        public string entityJson { get; set; }
    }
}
