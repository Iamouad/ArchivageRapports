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
    
    public partial class Groupe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Groupe()
        {
            this.Reports = new HashSet<Report>();
            this.Students_Groupes = new HashSet<Students_Groupes>();
        }
    
        public int Id { get; set; }
        public string Id_prof { get; set; }
        public int Id_fil { get; set; }
        public int Id_niv { get; set; }
        public Nullable<System.DateTime> Delais { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Filiere Filiere { get; set; }
        public virtual Level Level { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Report> Reports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Students_Groupes> Students_Groupes { get; set; }
    }
}
