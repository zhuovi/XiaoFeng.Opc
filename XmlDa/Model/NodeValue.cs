using System;
using System.Collections.Generic;
using System.Text;

/****************************************************************
*  Copyright © (2026) www.eelf.cn All Rights Reserved.          *
*  Author : jacky                                               *
*  QQ : 7092734                                                 *
*  Email : jacky@eelf.cn                                        *
*  Site : www.eelf.cn                                           *
*  Create Time : 2026-01-11 22:43:20                            *
*  Version : v 1.0.0                                            *
*  CLR Version : 4.0.30319.42000                                *
*****************************************************************/
namespace XiaoFeng.OPC.XmlDa.Model
{
    /// <summary>
    /// 节点值
    /// </summary>
    public class NodeValue : BrowseElement
    {
        #region 构造器
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        public NodeValue()
        {

        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="element">浏览节点</param>
        public NodeValue(BrowseElement element)
        {
            if (element == null) return;
            this.HasChildren = element.HasChildren;
            this.Name = element.Name;
            this.IsItem = element.IsItem;
            this.ItemName = element.ItemName;
            this.ItemPath = element.ItemPath;
            this.Properties = element.Properties;
            if(this.Properties!=null && this.Properties.Count > 0)
            {
                var itemProperty = this.Properties.Find(a => a.Name.EqualsIgnoreCase("Description"));
                if (itemProperty != null)
                {
                    this.Description = itemProperty.Value;
                }
                itemProperty = this.Properties.Find(a => a.Name.EqualsIgnoreCase("DataType"));
                if (itemProperty != null)
                {
                    this.DataType = itemProperty.Value.Substring(4).ToEnum<DataType>();
                }
                itemProperty = this.Properties.Find(a => a.Name.EqualsIgnoreCase("Quality"));
                if (itemProperty != null)
                {
                    this.Quality = new OPCQuality(itemProperty.Value.ToEnum<QualityBits>());
                }
                itemProperty = this.Properties.Find(a => a.Name.EqualsIgnoreCase("AccessRights"));
                if (itemProperty != null)
                {
                    this.AccessRights = itemProperty.Value.ToEnum<AccessRights>();
                }
                itemProperty = this.Properties.Find(a => a.Name.EqualsIgnoreCase("Timestamp"));
                if (itemProperty != null)
                {
                    this.Timestamp = itemProperty.Value.ToDateTime();
                }
                itemProperty = this.Properties.Find(a => a.Name.EqualsIgnoreCase("ScanRate"));
                if (itemProperty != null)
                {
                    this.ScanRate = itemProperty.Value.ToInt32();
                }
            }
        }
        /// <summary>
        /// 初始化一个新实例
        /// </summary>
        /// <param name="item">项</param>
        public NodeValue(ItemValue item)
        {
            if (item == null) return;
            this.ItemName = item.ItemName;
            if (ItemName.IsNotNullOrEmpty())
            {
                if (this.ItemName.IndexOf(".") == -1)
                    this.Name = this.ItemName;
                else this.Name = this.ItemName.Substring(this.ItemName.LastIndexOf(".")+1);
            }
            this.ItemPath = item.ItemPath;
            this.Quality = item.Quality;
            this.Timestamp = item.Timestamp;
            this.Value = item.Value;
            switch (item.ValueTypeQualifier)
            {
                case "VT_BSTR":
                    if (item.Value.ToString().IsMatch(@"^(year|month|day|hour|minute|second)$"))
                        this.DataType = DataType.duration;
                    else
                        this.DataType = DataType.@string;
                    break;
                case "VT_BOOL":
                    this.DataType = DataType.boolean;
                    break;
                case "VT_R4":
                    this.DataType = DataType.@float;
                    break;
                case "VT_R8":
                    this.DataType = DataType.@double;
                    break;
                case "VT_CY":
                    this.DataType = DataType.@decimal;
                    break;
                case "VT_I8":
                    this.DataType = DataType.@long;
                    break;
                case "VT_I4":
                    this.DataType = DataType.@int;
                    break;
                case "VT_I2":
                    this.DataType = DataType.@short;
                    break;
                case "VT_I1":
                    this.DataType = DataType.@byte;
                    break;
                case "VT_UI8":
                    this.DataType = DataType.unsignedLong;
                    break;
                case "VT_UI4":
                    this.DataType = DataType.unsignedInt;
                    break;
                case "VT_UI2":
                    this.DataType = DataType.unsignedShort;
                    break;
                case "VT_UI1":
                    this.DataType = DataType.unsignedByte;
                    break;
                case "VT_ARRAY":
                    this.DataType = DataType.base64Binary;
                    break;
                case "VT_DATE":
                    if (item.Value.ToString().IsDate())
                        this.DataType = DataType.date;
                    else if (item.Value.ToString().IsTime())
                        this.DataType = DataType.time;
                    else
                        this.DataType = DataType.dateTime;
                    break;
                case "VT_VARIANT":
                    this.DataType = DataType.AnyType;
                    break;
                default:
                    this.DataType = DataType.QName;
                    break;
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 子节点集
        /// </summary>
        public List<NodeValue> ChildNodes { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public DataType DataType { get; set; }
        /// <summary>
        /// OPC质量
        /// </summary>
        public OPCQuality Quality { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 项目权限
        /// </summary>
        public AccessRights AccessRights { get; set; }
        /// <summary>
        /// 扫描速率
        /// </summary>
        public int ScanRate { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }
        #endregion

        #region 方法

        #endregion
    }
}