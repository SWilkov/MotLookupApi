using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotLookupApi.DataLayer.MySQL.DataModels
{
  public class CommentDataModel : BaseDataModel
  {
    [Column("text")]
    [Required]
    public string Text { get; set; }
    [Column("type")]
    [Required]
    public string Type { get; set; }
    [Column("dangerous")]
    public bool Dangerous { get; set; }
    [Column("mot_test_id")]
    [Required]
    public int MotTestId { get; set; }
    public MotTestDataModel MotTest { get; set; }
  }
}
