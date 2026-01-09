using System.ComponentModel.DataAnnotations;

namespace EFCoreNews.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(50, ErrorMessage = "Title cannot exceed 50 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string Category { get; set; }

        [Required(ErrorMessage = "TenantId is required.")]
        [StringLength(100, ErrorMessage = "TenantId cannot exceed 100 characters.")]
        public string TenantId { get; set; } = null!;

        [Required(ErrorMessage = "UserId is required.")]
        public Guid UserId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "SessionId must be greater than 0.")]
        public int SessionId { get; set; }

        [Required(ErrorMessage = "PartitionKey is required.")]
        [StringLength(150, ErrorMessage = "PartitionKey cannot exceed 150 characters.")]
        public string PartitionKey { get; set; }
    }
}
