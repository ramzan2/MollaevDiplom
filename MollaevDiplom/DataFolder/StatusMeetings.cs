//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MollaevDiplom.DataFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class StatusMeetings
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StatusMeetings()
        {
            this.Meetings = new HashSet<Meetings>();
        }
    
        public int IdStatusMeetings { get; set; }
        public string NameStatusMeetings { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Meetings> Meetings { get; set; }
    }
}
