//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LTMSV2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class AnalysisGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AnalysisGroup()
        {
            this.AnalysisHeads = new HashSet<AnalysisHead>();
        }
    
        public int AnalysisGroupID { get; set; }
        public string AnalysisGroup1 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnalysisHead> AnalysisHeads { get; set; }
    }
}
