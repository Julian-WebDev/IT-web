using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IT_WEB.Models.Combo_Model
{
    public class AssetModel
    {
        public int Id { get; set; }
        public string AssetType { get; set; }
        public string ProductCode { get; set; }
        public string Category { get; set; }
        public string Make { get; set; }
        public string SerialNumber { get; set; }
        public Nullable<int> AssetNumber { get; set; }
        public string Taggable { get; set; }
        public string AllocatedTo { get; set; }
        public string EmailId { get; set; }
        public int Po_No { get; set; }
        public string EolEos { get; set; }
        public string EolEosDate { get; set; }
    }
}