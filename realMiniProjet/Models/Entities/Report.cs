//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace realMiniProjet.Models.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public int Id_report { get; set; }
        public string Id_prof { get; set; }
        public int Id_filiere { get; set; }
        public int Id_grp { get; set; }
        public int Id_type { get; set; }
        public int Id_niv { get; set; }
        public string ReportPath { get; set; }
        public string DateUniv { get; set; }
        public System.DateTime DateDepot { get; set; }
        public string Sujet { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Filiere Filiere { get; set; }
        public virtual Groupe Groupe { get; set; }
        public virtual Level Level { get; set; }
        public virtual Type_Reports Type_Reports { get; set; }
    }
}
