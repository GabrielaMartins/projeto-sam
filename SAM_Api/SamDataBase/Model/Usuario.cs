//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SamDataBase.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Eventos = new HashSet<Evento>();
            this.Pendencias = new HashSet<Pendencia>();
            this.Promocoes = new HashSet<Promocao>();
            this.ResultadoVotacoes = new HashSet<ResultadoVotacao>();
        }
    
        public int id { get; set; }
        public Nullable<int> cargo { get; set; }
        public int pontos { get; set; }
        public string samaccount { get; set; }
        public string nome { get; set; }
        public string descricao { get; set; }
        public string github { get; set; }
        public string facebook { get; set; }
        public string linkedin { get; set; }
        public string perfil { get; set; }
        public System.DateTime dataInicio { get; set; }
        public string foto { get; set; }
        public bool ativo { get; set; }
    
        public virtual Cargo Cargo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evento> Eventos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pendencia> Pendencias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promocao> Promocoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResultadoVotacao> ResultadoVotacoes { get; set; }
    }
}
