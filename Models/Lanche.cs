using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Lanches.Models;

namespace Lanches.Models
{
    [Table("Lanches")]
    public class Lanche
    {
        [Key]
        public int Id { get; set;}

        [Required(ErrorMessage = "O nome do lanche deve ser informado.")]
        [Display(Name = "Nome do Lanche")]
        [StringLength(80, ErrorMessage = "O {0} deve ter no mínimo {1} e no máximo {2}.")]
        public string Nome { get; set;}

        [Required(ErrorMessage = "A descricao do lanche deve ser informada.")]
        [Display(Name = "Descricao do Lanche")]
        [MinLength(20, ErrorMessage = "Descricao deve ter no mínimo {1} caracteres.")]
        [MaxLength(200, ErrorMessage = "Descricao nao pode exceder {1} caracteres.")]
        public string DescricaoCurta { get; set; }

        [Required(ErrorMessage = "A descricao do lanche deve ser informada.")]
        [Display(Name = "Descricao do Lanche")]
        [MinLength(20, ErrorMessage = "Descricao deve ter no mínimo {1} caracteres.")]
        [MaxLength(200, ErrorMessage = "Descricao nao pode exceder {1} caracteres.")]
        public string DescricaoDetalhada { get; set; }

        [Required(ErrorMessage = "O preco do lanche deve ser informado.")]
        [Display(Name = "Preco")]
        [Column(TypeName ="decimal(10),2")]
        [Range(1, 999.99, ErrorMessage = "O preco deve ser estar entre {1} e {2}.")]
        public decimal Preco { get; set; }

        [Display(Name = "Caminho Imagem Normal")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemUrl { get; set; }

        [Display(Name = "Caminho Imagem Miniatura")]
        [StringLength(200, ErrorMessage = "O {0} deve ter no máximo {1} caracteres")]
        public string ImagemThumbnailUrl { get; set; }

        [Display(Name = "Preferido?")]
        public bool IsLanchePreferido { get; set; }

        [Display(Name = "Estoque")]
        public bool EmEstoque { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}

